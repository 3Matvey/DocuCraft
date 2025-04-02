using System.Text.Json;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Factories;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Formats
{
    public class JsonFormatHandler : IFormatHandler
    {
        public string Serialize(Document doc)
        {
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
                using var jsonDoc = JsonDocument.Parse(data);
                var root = jsonDoc.RootElement;

                if (!root.TryGetProperty("DocumentType", out var typeElement))
                    return Error.Failure("JsonDeserializeError", "Отсутствует свойство DocumentType.");

                string docType = typeElement.GetString() ?? "";
                string title = root.GetProperty("Title").GetString() ?? "";
                string content = root.GetProperty("Content").GetString() ?? "";

                Document doc = DocumentFactory.CreateDocument(docType, title);
                doc.InsertText(content);
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("JsonDeserializeError", ex.Message);
            }
        }
    }
}
