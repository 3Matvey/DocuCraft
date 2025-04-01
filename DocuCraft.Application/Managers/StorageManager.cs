using DocuCraft.Application.Storage;

namespace DocuCraft.Application.Managers
{
    public class StorageManager(IStorageStrategy strategy)
    {
        public void SaveDocument(Document document, string format)
        {
            strategy.Save(document, format);
        }

        public void LoadDocument(Document document, string filePath)
        {
            strategy.Load(document, filePath);
        }
    }
}
