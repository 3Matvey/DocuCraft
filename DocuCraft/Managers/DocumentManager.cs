using DocuCraft.Models;
using DocuCraft.Factories;
using DocuCraft.ResultPattern;

namespace DocuCraft.Managers
{
    public class DocumentManager
    {
        private readonly Dictionary<string, Document> _documents = [];

        // Создание нового документа с возвратом Result<Document>
        public Result<Document> CreateDocument(string type, string title)
        {
            if (_documents.ContainsKey(title))
                return Error.Failure("DOC001", "Документ с таким названием уже существует");

            try
            {
                Document doc = DocumentFactory.CreateDocument(type, title);
                _documents.Add(title, doc);
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC002", ex.Message);
            }
        }

        // Открытие документа из файловой системы с возвратом Result<Document>
        public Result<Document> OpenDocument(string type, string title, string filePath)
        {
            try
            {
                Document doc = DocumentFactory.CreateDocument(type, title);
                doc.Load(filePath);
                _documents[title] = doc;
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC003", ex.Message);
            }
        }

        // Получение документа для редактирования с возвратом Result<Document>
        public Result<Document> GetDocument(string title)
            => _documents.TryGetValue(title, out Document? doc)
                ? Result<Document>.Success(doc)
                : Error.NotFound("DOC004", "Документ не найден");

        // Удаление документа с возвратом Result
        public Result DeleteDocument(string title)
            => _documents.Remove(title)
                ? Result.Success()
                : Error.NotFound("DOC005", "Документ не найден");

        // Вывод списка открытых документов
        public IEnumerable<Document> ListDocuments()
            => _documents.Values;
    }
}
