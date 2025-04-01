namespace DocuCraft.Application.Storage
{
    public interface IStorageStrategy
    {
        Result Save(Document document, string format);
        Result Load(Document document, string filePath);
    }
}
