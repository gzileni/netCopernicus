using System;
namespace Copernicus
{
    public class Nitrogendioxide : Sentinel
    {
        public Nitrogendioxide(List<float> coordinates) : base("NO2", coordinates)
        {
        }

        public Nitrogendioxide(List<float> coordinates, int days) : base("NO2", coordinates, days)
        {
        }
    }
}

