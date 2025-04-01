using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Storage
{
    public interface IStorageStrategy
    {
        Result Save(Document document, string format);
        Result Load(Document document, string filePath);
    }
}
