using System;
namespace Copernicus
{
	public class AerosolIndex: Sentinel
	{
        public AerosolIndex(List<float> coordinates) : base("AER_AI", coordinates)
        {
        }

        public AerosolIndex(List<float> coordinates, int days) : base("AER_AI", coordinates, days)
        {
        }
    }
}

