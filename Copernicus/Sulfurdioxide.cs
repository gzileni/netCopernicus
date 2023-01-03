using System;
namespace Copernicus
{
	public class Sulfurdioxide: Sentinel
	{
		public Sulfurdioxide(List<float> coordinates): base("SO2", coordinates)
		{
        }

        public Sulfurdioxide(List<float> coordinates, int days) : base("SO2", coordinates, days)
        {
        }
    }
}

