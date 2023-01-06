using System;
namespace Copernicus
{
    public class Entry
    {
        public string? title { get; set; }
        public string? id { get; set; }
        public string? summary { get; set; }
        public List<Link>? link { get; set; }
        public List<Date>? date { get; set; }
        public Entry()
        {
        }
    }
}

