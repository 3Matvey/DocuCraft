using System.Xml.Linq;
using DocuCraft.Models;
using DocuCraft.Factories;
using DocuCraft.ResultPattern;

namespace DocuCraft.Formats
{
    public class XmlFormatHandler : IFormatHandler
    {
        public Result Save(Document doc, string fileName)
        {
            try
            {
                var xml = new XElement("Document",
                            new XElement("DocumentType", doc.GetType().Name),
                            new XElement("Title", doc.Title),
                            new XElement("Content", doc.Content));
                xml.Save(fileName);
                Console.WriteLine($"Документ сохранён как {fileName}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("XmlSaveError", ex.Message);
            }
        }

        public Result<Document> Load(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return Error.NotFound("FileNotFound", $"Файл {fileName} не найден.");

                XElement xml = XElement.Load(fileName);
                string docType = xml.Element("DocumentType")?.Value ?? "";
                string title = xml.Element("Title")?.Value ?? "";
                string content = xml.Element("Content")?.Value ?? "";

                // Создаем документ нужного типа через фабрику
                Document doc = DocumentFactory.CreateDocumentByType(docType, title);
                doc.Content = content;
                Console.WriteLine($"Документ {fileName} успешно загружен (XML).");
                return doc;
            }
            catch (Exception ex)
            {
                return Error.Failure("XmlLoadError", ex.Message);
            }
        }
    }
}
