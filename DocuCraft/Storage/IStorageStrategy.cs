using DocuCraft.Models;

namespace DocuCraft.Storage
{
    public interface IStorageStrategy
    {
        void Save(Document document, string format);
        void Load(Document document, string filePath);
    }
}
