using System;
namespace Copernicus
{
	public class FormalDehyde: Sentinel
	{
        public FormalDehyde(List<float> coordinates) : base("HCHO", coordinates)
        {
        }

        public FormalDehyde(List<float> coordinates, int days) : base("HCHO", coordinates, days)
        {
        }
    }
}

