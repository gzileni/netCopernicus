using System;
namespace Copernicus
{
	public interface Product
	{
		public string name { get; set; }
		public string key { get; set; }
		public string description { get; set; }
		public string range { get; set; }
        public void download();
	}
}

