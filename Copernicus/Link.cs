using System;
namespace Copernicus
{
    public class Link
    {
        public string? rel { get; set; }
        public string? href { get; set; }
        public Link()
        {
        }

        public bool IsDataSet()
        {
            return this.rel == null;
        }
    }
}

