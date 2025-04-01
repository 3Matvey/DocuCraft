namespace DocuCraft.Application.Storage
{
    public interface IStorageStrategy
    {
        Result Save(Document document, string format);
        Result<Document> Load(string filePath);
    }
}
