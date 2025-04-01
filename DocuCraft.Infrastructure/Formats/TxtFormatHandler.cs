using System;
using System.IO;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Formats
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
                // Для TXT формата предполагаем, что документ – это PlainTextDocument.
                string title = Path.GetFileNameWithoutExtension(fileName);
                Document doc = new PlainTextDocument(title);
                doc.Content = content;
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
