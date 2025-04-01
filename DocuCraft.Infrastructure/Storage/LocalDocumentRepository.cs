using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Storage
{
    public class LocalDocumentRepository(IFormatHandler formatHandler) 
        : IDocumentRepository
    {
        public Result Save(Document doc, string fileName)
        {
            try
            {
                string serialized = formatHandler.Serialize(doc);
                File.WriteAllText(fileName, serialized);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("LocalSaveError", ex.Message);
            }
        }

        public Result<Document> Load(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return Error.NotFound("FileNotFound", $"Файл {fileName} не найден.");

                string data = File.ReadAllText(fileName);
                return formatHandler.Deserialize(data);
            }
            catch (Exception ex)
            {
                return Error.Failure("LocalLoadError", ex.Message);
            }
        }
    }
}
