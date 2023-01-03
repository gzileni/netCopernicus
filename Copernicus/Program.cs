using Copernicus;

List<float> coordinates = new();
coordinates.Add((float)45.6459);
coordinates.Add((float)7.1634);

AerosolHeight aH_1 = new(coordinates);
AerosolHeight aH_2 = new(coordinates, 7);
AerosolIndex aI_1 = new(coordinates);
Carbonmonoxide co = new(coordinates);
FormalDehyde fo = new(coordinates);
Nitrogendioxide ni = new(coordinates);
Sulfurdioxide su = new(coordinates);

Console.WriteLine($"\n{aH_1.Pollution.description}\n{aH_1.urlDataset()}");
Console.WriteLine($"\n{aH_2.Pollution.description}\n{aH_2.urlDataset()}");
Console.WriteLine($"\n{aI_1.Pollution.description}\n{aI_1.urlDataset()}");
Console.WriteLine($"\n{co.Pollution.description}\n{co.urlDataset()}");
Console.WriteLine($"\n{fo.Pollution.description}\n{fo.urlDataset()}");
Console.WriteLine($"\n{ni.Pollution.description}\n{ni.urlDataset()}");
Console.WriteLine($"\n{su.Pollution.description}\n{su.urlDataset()}");