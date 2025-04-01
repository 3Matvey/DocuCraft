using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Formats
{
    public class TxtFormatHandler : IFormatHandler
    {
        public Result Save(Document doc, string fileName)
        {
            try
            {
                File.WriteAllText(fileName, doc.Content);
                Console.WriteLine($"Документ сохранён как {fileName}");
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("TxtSaveError", ex.Message);
            }
        }

        public Result<Document> Load(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return Error.NotFound("FileNotFound", $"Файл {fileName} не найден.");

                string content = File.ReadAllText(fileName);
                // Используем имя файла (без расширения) в качестве заголовка
                string title = Path.GetFileNameWithoutExtension(fileName);
                // Для TXT формата создаём документ типа PlainTextDocument
                Document doc = new PlainTextDocument(title)
                {
                    Content = content
                };
                Console.WriteLine($"Документ {fileName} успешно загружен (TXT).");
                return doc;
            }
            catch (Exception ex)
            {
                return Error.Failure("TxtLoadError", ex.Message);
            }
        }
    }
}
