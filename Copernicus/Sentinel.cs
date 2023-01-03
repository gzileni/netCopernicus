using System;
using Copernicus;

namespace Copernicus
{
    public class Sentinel
    {
        public List<string> datasets { get; private set; }
        public Product Pollution { get; private set; }
        public string ingestiondate = "ingestiondate:[NOW-3DAYS TO NOW]";
        public int row = 100;
        public int start = 0;

        private string footprint { get; set; }
        private Dictionary<string, string> headers = new Dictionary<string, string>();
        private Dictionary<string, Product> products = new()
        {
            { "SO2", new Product { name="L2__SO2___", key="sulfurdioxide", description="Sulfur Dioxide (SO2)" }},
            { "NO2", new Product { name = "L2__NO2___", key = "nitrogendioxide", description = "Nitrogen Dioxide (NO2)" }},
            { "HCHO", new Product { name = "L2__HCHO__", key = "formaldehyde", description = "Formaldehyde (HCHO)" }},
            { "CO", new Product { name = "L2__CO____", key = "carbonmonoxide", description = "Carbon Monoxide (CO)" }},
            { "AER_AI", new Product { name = "L2__AER_AI", key = "aerosolI", description = "UV Aerosol Index" }},
            { "AER_LH", new Product { name = "L2__AER_LH", key = "aerosolH", description = "UV Aerosol Height" }}
        };
        
        public Sentinel(string product, List<float> coordinates)
        {
            this.Pollution = this.products[product];
            this.footprint = $"footprint:\"intersects({coordinates[0]}, {coordinates[1]})\"";
            this.datasets = new();
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

