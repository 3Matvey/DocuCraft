using System.Text.Json;
using System.Xml.Linq;

namespace DocuCraft.Models
{
    public class RichTextDocument(string title) : Document(title)
    {
        public override void Save(string format)
        {
            switch (format.ToLower())
            {
                case "txt":
                    File.WriteAllText($"{Title}.rtf.txt", Content);
                    Console.WriteLine($"Документ сохранён как {Title}.rtf.txt");
                    break;
                case "json":
                    var json = JsonSerializer.Serialize(this);
                    File.WriteAllText($"{Title}.rtf.json", json);
                    Console.WriteLine($"Документ сохранён как {Title}.rtf.json");
                    break;
                case "xml":
                    var xml = new XElement("RichTextDocument",
                                    new XElement("Title", Title),
                                    new XElement("Content", Content));
                    xml.Save($"{Title}.rtf.xml");
                    Console.WriteLine($"Документ сохранён как {Title}.rtf.xml");
                    break;
                default:
                    Console.WriteLine($"Формат {format} не поддерживается для RichTextDocument.");
                    break;
            }
        }

        public override void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                if (filePath.EndsWith(".json"))
                {
                    string json = File.ReadAllText(filePath);
                    var loadedDoc = JsonSerializer.Deserialize<RichTextDocument>(json);
                    if (loadedDoc != null)
                    {
                        Title = loadedDoc.Title;
                        Content = loadedDoc.Content;
                        Console.WriteLine($"Документ {filePath} успешно загружен (JSON).");
                    }
                }
                else if (filePath.EndsWith(".xml"))
                {
                    var xml = XElement.Load(filePath);
                    Title = xml.Element("Title")?.Value!;
                    Content = xml.Element("Content")?.Value!;
                    Console.WriteLine($"Документ {filePath} успешно загружен (XML).");
                }
                else // txt
                {
                    Content = File.ReadAllText(filePath);
                    Console.WriteLine($"Документ {filePath} успешно загружен (TXT).");
                }
            }
            else
            {
                Console.WriteLine($"Файл {filePath} не найден.");
            }
        }
    }
}
