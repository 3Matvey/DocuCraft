using DocuCraft.Models;

namespace DocuCraft.Storage
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
