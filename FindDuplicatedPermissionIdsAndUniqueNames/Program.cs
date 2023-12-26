Console.WriteLine("Enter destination file full path:");
var destinationFilePath = Console.ReadLine().ToString();

List<string> GetDirectories(string path)
{

    var directories1 = new List<string>();

    directories1.Add(path);

    var newDiectory = new List<string>();
    newDiectory.AddRange(directories1);

    var tempDirectory = new List<string>();

    while (true)
    {
        foreach (var item in newDiectory)
        {
            tempDirectory.AddRange(Directory.GetDirectories(item));
        }

        if (tempDirectory.Count == 0) break;

        newDiectory.Clear();
        newDiectory.AddRange(tempDirectory);
        directories1.AddRange(tempDirectory);
        tempDirectory.Clear();
    }

    return directories1;
}

var allNames = new List<string>();

var destinationFile = File.ReadAllLines(destinationFilePath).ToList();

var idsInDestinationFile = new List<string>();
var namesInDestinationFile = new List<string>();

foreach (var line in destinationFile)
{
    if (line.Replace(" ", "").Trim().StartsWith("Id="))
    {
        var id = line.Replace(" ", "").Trim().Split("=")[1].Replace(",", "");
        idsInDestinationFile.Add(id);
    }

    if (line.Contains("UniqueName"))
    {
        var name = line.Replace(" ", "").Trim().Split("=")[1].Replace(",", "");
        namesInDestinationFile.Add(name);
    }
}

var duplicatedIds = new List<string>();
var duplicatedNames = new List<string>();

foreach (var id in idsInDestinationFile.GroupBy(id => id).Where(g => g.Count() > 1))
{
    duplicatedIds.Add(id.Key);
}

foreach (var name in namesInDestinationFile.GroupBy(name => name).Where(g => g.Count() > 1))
{
    duplicatedNames.Add(name.Key);
}

Console.WriteLine("Duplicated ids:");
foreach (var id in duplicatedIds)
{
    Console.WriteLine(id);
}
Console.WriteLine("------------------------------------");

Console.WriteLine("Duplicated names:");
foreach (var name in duplicatedNames)
{
    Console.WriteLine(name);
}

Console.WriteLine("------------------------------------");
Console.WriteLine("Done");