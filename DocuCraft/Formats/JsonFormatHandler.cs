using System.Text.Json;
using DocuCraft.Models;
using DocuCraft.Factories;
using DocuCraft.ResultPattern;

namespace DocuCraft.Formats
{
    public class JsonFormatHandler : IFormatHandler
    {
        public Result Save(Document doc, string fileName)
        {
            try
            {
                // Включаем тип документа в JSON для последующей десериализации
                var obj = new
                {
                    DocumentType = doc.GetType().Name,
                    doc.Title,
                    doc.Content
                };
                string json = JsonSerializer.Serialize(obj);
                File.WriteAllText(fileName, json);
                Console.WriteLine($"Документ сохранён как {fileName}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("JsonSaveError", ex.Message);
            }
        }

        public Result<Document> Load(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return Error.NotFound("FileNotFound", $"Файл {fileName} не найден.");

                string json = File.ReadAllText(fileName);
                using (JsonDocument jsonDoc = JsonDocument.Parse(json))
                {
                    var root = jsonDoc.RootElement;
                    if (!root.TryGetProperty("DocumentType", out JsonElement typeElement))
                        return Error.Failure("JsonLoadError", "Отсутствует свойство DocumentType в JSON.");

                    string docType = typeElement.GetString() ?? "";
                    string title = root.GetProperty("Title").GetString() ?? "";
                    string content = root.GetProperty("Content").GetString() ?? "";

                    // Создаём документ нужного типа через фабрику
                    Document doc = DocumentFactory.CreateDocumentByType(docType, title);
                    doc.Content = content;
                    Console.WriteLine($"Документ {fileName} успешно загружен (JSON).");
                    return doc;
                }
            }
            catch (Exception ex)
            {
                return Error.Failure("JsonLoadError", ex.Message);
            }
        }
    }
}
