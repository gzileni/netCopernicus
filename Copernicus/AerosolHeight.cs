using System;
namespace Copernicus
{
	public class AerosolHeight: Sentinel
	{
        public AerosolHeight(List<float> coordinates) : base("AER_LH", coordinates)
        {
        }

        public AerosolHeight(List<float> coordinates, int days) : base("AER_LH", coordinates, days)
        {
        }
    }
}

