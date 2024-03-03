using System;
using System.Collections.Generic;
using System.IO;

public class TextFileEditor
{
    private string filePath;
    private Stack<TextFileMemento> history;
    private string currentContent;

    public TextFileEditor(string filePath)
    {
        this.filePath = filePath;
        this.history = new Stack<TextFileMemento>();
        this.currentContent = File.ReadAllText(filePath);
        SaveHistory();
    }

    public string GetCurrentContent()
    {
        return currentContent;
    }

    public void SetCurrentContent(string newContent)
    {
        currentContent = newContent;
        SaveHistory(); // Сохраняем изменение в истории
    }

    public void Undo()
    {
        if (history.Count > 1)
        {
            history.Pop(); // Удаляем текущее состояние из истории
            TextFileMemento previousState = history.Peek();
            currentContent = previousState.Content;
            File.WriteAllText(filePath, currentContent);
        }
    }

    private void SaveHistory()
    {
        TextFileMemento memento = new TextFileMemento(currentContent);
        history.Push(memento);
    }
}

public class TextFileMemento
{
    public string Content { get; private set; }

    public TextFileMemento(string content)
    {
        Content = content;
    }
}
class Program
{
    static void Main(string[] args)
    {
        TextFileEditor editor = new TextFileEditor("example.txt");

        Console.WriteLine("Current Content:");
        Console.WriteLine(editor.GetCurrentContent());

        // Make some changes
        editor.SetCurrentContent("New content added!");

        Console.WriteLine("After making changes:");
        Console.WriteLine(editor.GetCurrentContent());

        // Undo changes
        editor.Undo();

        Console.WriteLine("After undoing changes:");
        Console.WriteLine(editor.GetCurrentContent());
    }
}
