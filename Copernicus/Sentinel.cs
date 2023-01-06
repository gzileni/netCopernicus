using System;
using Copernicus;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using SDS = Microsoft.Research.Science.Data;
using Microsoft.Research.Science.Data.Imperative;
using Microsoft.VisualBasic;

namespace Copernicus
{
    public class Sentinel
    {
        public Product Pollution { get; private set; } = new();
        public string ingestiondate = "ingestiondate:[NOW-3DAYS TO NOW]";
        public int row = 100;
        public int start = 0;
        public string path { get; private set; } = "";
        public Dataset? datasets { get; private set; }

        private string? footprint { get; set; }
        public Dictionary<string, string> headers = new Dictionary<string, string>()
        {
            { "Authorization", "Basic czVwZ3Vlc3Q6czVwZ3Vlc3Q=" }
        };
        private Dictionary<string, Product> products = new();

        public Sentinel(string product)
        {
            string productsConf = $"../../../products.json";
            string jsonString = File.ReadAllText(productsConf);
            List<Product> products = JsonSerializer.Deserialize<List<Product>>(jsonString)!;

            foreach (Product p in products)
            {
                this.products.Add(p.index, p);
            }
        }

        public Sentinel(string product, List<float> coordinates) : this(product)
        {
            this.Pollution = this.products[product];
            this.footprint = $"footprint:\"intersects({coordinates[0]}, {coordinates[1]})\"";
            this.path = $"{Directory.GetCurrentDirectory()}/{this.Pollution.key}";
            Directory.CreateDirectory(this.path);
        }

        public Sentinel(string product, List<float> coordinates, int days) : this(product, coordinates)
        {
            this.ingestiondate = $"ingestiondate:[NOW-{days}DAYS TO NOW]";
        }

        public string urlDataset()
        {
            return $"https://s5phub.copernicus.eu/dhus/search?q={this.footprint} AND {this.ingestiondate} AND producttype:{this.Pollution.name}&rows={this.row}&start={this.start}&format=json";
        }

        public async Task getDatasets()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("Authorization", this.headers["Authorization"]);
            await using Stream stream = await client.GetStreamAsync(this.urlDataset());
            this.datasets = await JsonSerializer.DeserializeAsync<Dataset>(stream);
            await downloadDatasets();
        }

        public void openDataset(string path)
        {
            try
            {
                string uri = $"file:///{path}";
                var dataset = SDS.DataSet.Open(uri);
                Console.WriteLine(dataset);
            } catch (Exception e)
            {
                Console.WriteLine("\n{0} Exception caught.", e);
            }
            
        }
        
        private async Task downloadDatasets()
        {
            if (this.datasets != null && this.datasets.feed != null && this.datasets.feed.entry != null)
            {
                for (int i = 0; i < this.datasets.feed.entry.Count; i++)
                {
                    Entry e = this.datasets.feed.entry[i];
                    if (e != null && e.link != null)
                    {
                        foreach (Link l in e.link)
                        {
                            if (l.href != null && e.title != null && l.IsDataSet())
                            {
                                Console.Write($"\n");
                                string path = $"{this.path}/{e.title}.nc";
                                using (HttpClientWithProgress download = new(l.href, path, this.headers["Authorization"]))
                                {
                                    download.ProgressChanged += (totalFileSize, totalBytesDownloaded, progressPercentage) => {
                                        Console.Write($"\r{e.title} -> {progressPercentage}% ({totalBytesDownloaded}/{totalFileSize})");
                                    };

                                    await download.StartDownload();
                                }
                                Console.Write($"\r{e.title} -> Ok");
                                // open dataset NetCDF from path
                                this.openDataset(path);
                            }
                        }
                    }
                }
            }
        }
    }
}

