using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;

namespace DocuCraft.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Result Save(Document doc, string fileName);
        Result<Document> Load(string id);
    }
}
