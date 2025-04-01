using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;

namespace DocuCraft.Domain.Interfaces
{
    public interface IFormatHandler
    {
        string Serialize(Document doc);
        Result<Document> Deserialize(string data);
    }
}
