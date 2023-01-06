using Copernicus;

List<float> coordinates = new();
coordinates.Add((float)45.6459);
coordinates.Add((float)7.1634);

// CopernicusDB db = new();
// bool task = db.Connect();
// Console.WriteLine("Connected: " + task);

AerosolHeight aH_1 = new(coordinates);
AerosolIndex aI_1 = new(coordinates);
Carbonmonoxide co = new(coordinates);
FormalDehyde fo = new(coordinates);
Nitrogendioxide ni = new(coordinates);
Sulfurdioxide su = new(coordinates);

// await aH_1.getDatasets();
// await aI_1.getDatasets();
await co.getDatasets();
// await fo.getDatasets();
// await ni.getDatasets();
// await su.getDatasets();




