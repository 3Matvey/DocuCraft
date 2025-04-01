using System;
using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Storage
{
    public class LocalStorageStrategy 
        : IStorageStrategy
    {
        public Result Save(Document document, string format)
        {
            try
            {
                document.Save(format);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("LS001", ex.Message);
            }
        }

        public Result Load(Document document, string filePath)
        {
            try
            {
                document.Load(filePath);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("LS002", ex.Message);
            }
        }
    }
}
