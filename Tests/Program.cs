// See https://aka.ms/new-console-template for more information
using System.Security.Cryptography;
using Tests;

TestsRunner.RunTestsAndCollectStats();


Console.WriteLine($"\n\n\n\t\t\t\t--- STATISTICS ---");
Dictionary<string, List<(int d, double avgN, double bStar, double avgMem)>>? stats = TestsRunner.CombineStats();
if (stats == null) Console.WriteLine("Error");
else
{
    foreach (var algorithm in stats)
    {
        Console.WriteLine($"\n\t\t--- Average Nodes Generated, b*, Average Memory Usage for {algorithm.Key} ---");
        foreach (var (d, avgN, bStar, avgMem) in algorithm.Value)
        {
            Console.WriteLine($"\t\t\t\tDepth: {d}, N: {avgN:F1}, b*: {bStar:F2}, mem:{avgMem:F1}");
        }
    }
}
