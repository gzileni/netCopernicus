using System;
namespace Copernicus
{
	public class Carbonmonoxide: Sentinel
	{
        public Carbonmonoxide(List<float> coordinates) : base("CO", coordinates)
        {
        }

        public Carbonmonoxide(List<float> coordinates, int days) : base("CO", coordinates, days)
        {
        }
    }
}

