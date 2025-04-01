using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Formats
{
    public interface IFormatHandler
    {
        Result Save(Document doc, string fileName);
        Result<Document> Load(string fileName);
    }
}
