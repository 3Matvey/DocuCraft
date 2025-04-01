using System.Text.Json;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Formats
{
    public class JsonFormatHandler : IFormatHandler
    {
        public string Serialize(Document doc)
        {
            // Включаем тип документа, чтобы при десериализации можно было создать нужный объект
            var obj = new
            {
                DocumentType = doc.GetType().Name,
                doc.Title,
                doc.Content
            };
            return JsonSerializer.Serialize(obj);
        }

        public Result<Document> Deserialize(string data)
        {
            try
            {
                using (JsonDocument jsonDoc = JsonDocument.Parse(data))
                {
                    var root = jsonDoc.RootElement;
                    if (!root.TryGetProperty("DocumentType", out var typeElement))
                        return Error.Failure("JsonLoadError", "Отсутствует свойство DocumentType.");

                    string docType = typeElement.GetString() ?? "";
                    string title = root.GetProperty("Title").GetString() ?? "";
                    string content = root.GetProperty("Content").GetString() ?? "";

                    // Используем фабрику для создания документа нужного типа
                    Document doc = Domain.Factories.DocumentFactory.CreateDocument(docType, title);
                    doc.Content = content;
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
