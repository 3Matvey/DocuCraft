using DocuCraft.Application.Storage;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;

namespace DocuCraft.Infrastructure.Storage
{
    public class CloudStorageStrategy 
        : IStorageStrategy
    {
        public async Task<Result> SaveAsync(Document document, string format)
        {
           throw new NotImplementedException(nameof(SaveAsync));
        }

        public async Task<Result<Document>> LoadAsync(string filePath)
        {
            throw new NotImplementedException(nameof(LoadAsync));
        }

        // Заглушка для метода удаления
        public async Task<Result> DeleteAsync(string filePath)
        {
            throw new NotImplementedException(nameof(DeleteAsync));
        }
    }
}
