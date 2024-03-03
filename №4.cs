using System;
using System.Collections.Generic;
using System.IO;

public class TextFileIndexer
{
    public Dictionary<string, List<string>> IndexFiles(string directoryPath, string[] keywords)
    {
        Dictionary<string, List<string>> index = new Dictionary<string, List<string>>();

        foreach (string filePath in Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories))
        {
            string fileContent = File.ReadAllText(filePath);

            foreach (string keyword in keywords)
            {
                if (fileContent.Contains(keyword))
                {
                    if (!index.ContainsKey(keyword))
                    {
                        index[keyword] = new List<string>();
                    }

                    index[keyword].Add(filePath);
                }
            }
        }

        return index;
    }
}
class Program
{
    static void Main()
    {
        TextFileIndexer indexer = new TextFileIndexer();
        string directoryPath = "C:/yourDirectoryPath";
        string[] keywords = { "keyword1", "keyword2", "keyword3" };

        Dictionary<string, List<string>> index = indexer.IndexFiles(directoryPath, keywords);

        foreach (var kvp in index)
        {
            Console.WriteLine($"Keyword: {kvp.Key}");
            Console.WriteLine("Files:");
            foreach (var file in kvp.Value)
            {
                Console.WriteLine(file);
            }
            Console.WriteLine();
        }
    }
}
