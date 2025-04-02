using DocuCraft.Application.Storage;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Factories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocuCraft.Application.Managers
{
    public class DocumentManager
    {
        private readonly IStorageStrategy _storageStrategy;
        private readonly Dictionary<string, Document> _documents = new();

        public DocumentManager(IStorageStrategy storageStrategy)
        {
            _storageStrategy = storageStrategy;
        }

        // Метод для создания нового документа
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

        // Метод для редактирования документа
        public Result EditDocument(string title, string newText, int position)
        {
            var result = GetDocument(title);
            if (!result.IsSuccess)
                return result.Error;

            Document doc = result.Value;

            try
            {
                doc.InsertText(newText, position); // Метод для вставки текста
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC003", ex.Message);
            }
        }

        // Открытие документа
        public async Task<Result<Document>> OpenDocumentAsync(string type, string title, string filePath)
        {
            try
            {
                Document doc = DocumentFactory.CreateDocument(type, title);
                var loadResult = await _storageStrategy.LoadAsync(filePath);
                if (!loadResult.IsSuccess)
                    return loadResult;

                _documents[title] = doc;
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC004", ex.Message);
            }
        }

        // Метод для сохранения документа через стратегию
        public async Task<Result> SaveDocumentAsync(string title, string format)
        {
            var result = GetDocument(title);
            if (!result.IsSuccess)
                return result.Error;

            Document doc = result.Value;

            try
            {
                return await _storageStrategy.SaveAsync(doc, format); // Сохраняем документ через стратегию
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC005", ex.Message);
            }
        }

        // Получение документа по названию
        public Result<Document> GetDocument(string title)
            => _documents.TryGetValue(title, out Document? doc)
                ? Result<Document>.Success(doc)
                : Error.NotFound("DOC006", "Документ не найден");

        // Удаление документа
        public Result DeleteDocument(string title)
            => _documents.Remove(title)
                ? Result.Success()
                : Error.NotFound("DOC007", "Документ не найден");

        // Вывод списка документов
        public IEnumerable<Document> ListDocuments()
            => _documents.Values;
    }
}
