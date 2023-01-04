using System;
namespace Copernicus
{
	public class AerosolIndex: Sentinel
	{
        public AerosolIndex(List<float> coordinates) : base("AER_I", coordinates)
        {
        }

        public AerosolIndex(List<float> coordinates, int days) : base("AER_I", coordinates, days)
        {
        }
    }
}

