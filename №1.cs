using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

[Serializable]
public class TextFile
{
    public string FilePath { get; set; }
    public string Content { get; set; }

    public void SaveAsBinary(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            BinarySerializer serializer = new BinarySerializer();
            serializer.Serialize(fileStream, this);
        }
    }

    public static TextFile LoadFromBinary(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            BinarySerializer serializer = new BinarySerializer();
            return (TextFile)serializer.Deserialize(fileStream);
        }
    }

    public void SaveAsXml(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
            serializer.Serialize(fileStream, this);
        }
    }

    public static TextFile LoadFromXml(string filePath)
    {
        using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(TextFile));
            return (TextFile)serializer.Deserialize(fileStream);
        }
    }
}

public class BinarySerializer
{
    public void Serialize(Stream stream, object obj)
    {
        using (BinaryWriter writer = new BinaryWriter(stream))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
        }
    }

    public object Deserialize(Stream stream)
    {
        using (BinaryReader reader = new BinaryReader(stream))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return formatter.Deserialize(stream);
        }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        // Создание экземпляра класса TextFile
        TextFile file = new TextFile
        {
            FilePath = "example.txt",
            Content = "Пример содержимого файла"
        };

        // Сохранение файла в бинарном формате
        file.SaveAsBinary("example.bin");

        // Загрузка файла из бинарного формата
        TextFile loadedFile = TextFile.LoadFromBinary("example.bin");

        // Вывод содержимого загруженного файла
        Console.WriteLine(loadedFile.Content);

        // Сохранение файла в XML формате
        file.SaveAsXml("example.xml");

        // Загрузка файла из XML формата
        loadedFile = TextFile.LoadFromXml("example.xml");

        // Вывод содержимого загруженного файла
        Console.WriteLine(loadedFile.Content);
    }
}
