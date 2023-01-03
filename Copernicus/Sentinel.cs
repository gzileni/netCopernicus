using System;
namespace Copernicus
{
	public class Sentinel
	{
        readonly Dictionary<string, string> headers = new() { { "Authorization", "Basic czVwZ3Vlc3Q6czVwZ3Vlc3Q=`" } };

        public Sentinel(string product, List<float> coordinates, Nullable<int> days)
        {
            this.product = $"producttype:{product}";
            this.footprint = $"footprint:\"intersects({coordinates[0]}, {coordinates[1]})\"";

            if (days is null)
            {
                this.range = "ingestiondate:[NOW-3DAYS TO NOW]";
            } else
            {
                this.range = $"ingestiondate:[NOW-{days}DAYS TO NOW]";
            };

            this.url = $"https://s5phub.copernicus.eu/dhus/search?q={this.footprint} AND {this.range} AND {this.product}&rows=100&start=0&format=json";
        }

        public string product { get; private set; }
        public string footprint { get; private set; }
        public string range { get; private set; }
        public string url { get; private set; }

        public void download()
        {
            throw new NotImplementedException();
        }
    }
}

