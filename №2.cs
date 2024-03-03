using System;
using System.Collections.Generic;
using System.IO;

public class TextFileSearcher
{
    public IEnumerable<string> Search(string directoryPath, string[] keywords)
    {
        List<string> matchingFiles = new List<string>();

        foreach (string filePath in Directory.GetFiles(directoryPath, "Свой файл.txt", SearchOption.AllDirectories))
        {
            string fileContent = File.ReadAllText(filePath);

            bool containsAllKeywords = true;
            foreach (string keyword in keywords)
            {
                if (!fileContent.Contains(keyword))
                {
                    containsAllKeywords = false;
                    break;
                }
            }

            if (containsAllKeywords)
            {
                matchingFiles.Add(filePath);
            }
        }

        return matchingFiles;
    }
}
class Program
{
    static void Main(string[] args)
    {
        TextFileSearcher searcher = new TextFileSearcher();
        string directoryPath = @"C:\ExampleDirectory";
        string[] keywords = { "keyword1", "keyword2", "keyword3" };

        IEnumerable<string> matchingFiles = searcher.Search(directoryPath, keywords);

        foreach (string file in matchingFiles)
        {
            Console.WriteLine($"Found matching file: {file}");
        }
    }
}
