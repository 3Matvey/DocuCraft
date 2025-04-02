namespace DocuCraft.Application.Storage
{
    public interface IStorageStrategy
    {
        Task<Result> SaveAsync(Document document, string format);
        Task<Result<Document>> LoadAsync(string filePath);
        Task<Result> DeleteAsync(string filePath);
    }
}
