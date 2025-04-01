using DocuCraft.Application.Storage;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Storage
{
    public class LocalStorageStrategy : IStorageStrategy
    {
        private readonly IDocumentRepository _repository;

        public LocalStorageStrategy(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public Result Save(Document document, string format)
        {
            // Формирование имени файла может делаться через доменный метод (если он доступен)
            // Здесь используем наивное формирование имени: "Title.format"
            string fileName = document.GetFileName(format);
            return _repository.Save(document, fileName);
        }

        public Result<Document> Load(string filePath)
        {
            return _repository.Load(filePath);
        }
    }
}
