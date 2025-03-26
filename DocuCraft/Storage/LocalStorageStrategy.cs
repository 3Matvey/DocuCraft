using DocuCraft.Models;

namespace DocuCraft.Storage
{
    public class LocalStorageStrategy : IStorageStrategy
    {
        public void Save(Document document, string format)
        {
            document.Save(format);
        }

        public void Load(Document document, string filePath)
        {
            document.Load(filePath);
        }
    }
}
