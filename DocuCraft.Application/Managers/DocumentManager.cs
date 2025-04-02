using DocuCraft.Application.Storage;
using DocuCraft.Domain.Factories;

namespace DocuCraft.Application.Managers
{
    public class DocumentManager(IStorageStrategy storageStrategy)
    {
        private readonly Dictionary<string, Document> _documents = [];

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
                return Error.NotFound("DOC003", "document not exist");

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
                var loadResult = await storageStrategy.LoadAsync(filePath);
                var doc = loadResult.Value;
                if (!loadResult.IsSuccess)
                    return loadResult.Error!;

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
                return Error.NotFound("DOC003", "document not exist");

            Document doc = result.Value;

            try
            {
                return await storageStrategy.SaveAsync(doc, format); // Сохраняем документ через стратегию
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC005", ex.Message);
            }
        }

        // Метод для удаления документа
        public async Task<Result> DeleteDocumentAsync(string title)
        {
            var result = GetDocument(title);
            if (!result.IsSuccess)
                return Error.NotFound("DOC003", "document not exist");

            Document doc = result.Value;

            try
            {
                // Удаляем документ из памяти
                _documents.Remove(title);

                // Удаляем документ из хранилища через стратегию
                string filePath = doc.GetFileName("json"); // Например, определим путь к файлу
                return await storageStrategy.DeleteAsync(filePath); // Удаляем файл через стратегию
            }
            catch (Exception ex)
            {
                return Error.Failure("DOC006", ex.Message);
            }
        }

        // Получение документа по названию
        public Result<Document> GetDocument(string title)
            => _documents.TryGetValue(title, out Document? doc)
                ? Result<Document>.Success(doc)
                : Error.NotFound("DOC007", "Документ не найден");


        // Удаление документа
        public Result DeleteDocument(string title)
            => _documents.Remove(title)
                ? Result.Success()
                : Error.NotFound("DOC008", "Документ не найден");

        // Вывод списка документов
        public IEnumerable<Document> ListDocuments()
            => _documents.Values;
    }
}

