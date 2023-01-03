using Copernicus;

List<float> coordinates = new();
coordinates.Add((float)45.6459);
coordinates.Add((float)7.1634);

Sentinel s5 = new("L2__CO____", coordinates, 3);
Console.WriteLine(s5.url);