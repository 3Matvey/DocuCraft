using DocuCraft.Factories;
using DocuCraft.Formats;
using DocuCraft.ResultPattern;

namespace DocuCraft.Models
{
    public abstract class Document(string title)
    {
        public string Title { get; set; } = title;
        public string Content { get; set; } = string.Empty;

        public virtual Result Save(string format)
        {
            try
            {
                IFormatHandler handler = FormatHandlerFactory.GetHandler(format);
                string fileName = GetFileName(format);
                return handler.Save(this, fileName);
            }
            catch (Exception ex)
            {
                return Error.Failure("SaveError", ex.Message);
            }
        }

        public virtual Result<Document> Load(string format, string filePath)
        {
            try
            {
                IFormatHandler handler = FormatHandlerFactory.GetHandler(format);
                return handler.Load(filePath);
            }
            catch (Exception ex)
            {
                return Error.Failure("LoadError", ex.Message);
            }
        }

        protected abstract string GetFileName(string format);
       
    }
}
