using System;
using Copernicus;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;
using System.Text.Json;
using System.Collections.Generic;
using System.Collections;

namespace Copernicus
{
    public class Sentinel
    {
        public Dataset Data = new();
        public Product Pollution { get; private set; } = new();
        public string ingestiondate = "ingestiondate:[NOW-3DAYS TO NOW]";
        public int row = 100;
        public int start = 0;
        
        private string? footprint { get; set; }
        private Dictionary<string, string> headers = new Dictionary<string, string>()
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

        public Sentinel(string product, List<float> coordinates): this(product)
        {
            this.Pollution = this.products[product];
            this.footprint = $"footprint:\"intersects({coordinates[0]}, {coordinates[1]})\"";
            this.Data.path = $"{Directory.GetCurrentDirectory()}/{this.Pollution.key}";
            Directory.CreateDirectory(this.Data.path);
        }

        public Sentinel(string product, List<float> coordinates, int days) : this(product, coordinates)
        {
            this.ingestiondate = $"ingestiondate:[NOW-{days}DAYS TO NOW]";
        }

        public string urlDataset()
        {
            return $"https://s5phub.copernicus.eu/dhus/search?q={this.footprint} AND {this.ingestiondate} AND producttype:{this.Pollution.name}&rows={this.row}&start={this.start}&format=json";
        }

        public void download()
        {
            throw new NotImplementedException();
        }
    }
}

