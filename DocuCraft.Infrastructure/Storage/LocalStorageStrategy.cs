using DocuCraft.Application.Storage;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using static DocuCraft.Infrastructure.Factories.FormatHandlerFactory;

namespace DocuCraft.Infrastructure.Storage
{
    public class LocalStorageStrategy() : 
        IStorageStrategy
    {
        public async Task<Result> SaveAsync(Document document, string format)
        {
            try
            {
                string fileName = document.GetFileName(format);
                string serializedData = GetHandler(format).Serialize(document);

                await File.WriteAllTextAsync(fileName, serializedData);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("LocalSaveError", ex.Message);
            }
        }

        public async Task<Result<Document>> LoadAsync(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                    return Error.NotFound("FileNotFound", $"Файл {filePath} не найден.");

                string data = await File.ReadAllTextAsync(filePath);
                string format = Path.GetExtension(filePath).TrimStart('.').ToLower();
                return GetHandler(format).Deserialize(data);
            }
            catch (Exception ex)
            {
                return Error.Failure("LocalLoadError", ex.Message);
            }
        }

        // Реализация метода удаления документа
        public Task<Result> DeleteAsync(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return Task.FromResult(Result.Success());
                }
                else
                {
                    return Task.FromResult<Result>(Error.NotFound("FileNotFound", $"Файл {filePath} не найден."));
                }
            }
            catch (Exception ex)
            {
                return Task.FromResult<Result>(Error.Failure("LocalDeleteError", ex.Message));
            }
        }
    }
}
