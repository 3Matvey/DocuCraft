using DocuCraft.Application.Storage;

namespace DocuCraft.Application.Managers
{
    public class StorageManager(IStorageStrategy initialStrategy)
    {
        public void SetStorageStrategy(IStorageStrategy newStrategy)
        {
            initialStrategy = newStrategy;
        }

        public IStorageStrategy GetStorageStrategy() => initialStrategy;

        public Task<Result> SaveDocumentAsync(Document document, string format)
        {
            return initialStrategy.SaveAsync(document, format);
        }

        public Task<Result<Document>> LoadDocumentAsync(string filePath)
        {
            return initialStrategy.LoadAsync(filePath);
        }
    }
}
