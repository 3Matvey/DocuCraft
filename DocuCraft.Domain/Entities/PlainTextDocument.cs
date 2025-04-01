using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Domain.Entities
{
    public class PlainTextDocument(string title) 
        : Document(title)
    {
        protected override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase) 
                ? $"{Title}.txt"
                : $"{Title}.{format.ToLower()}";
        }
    }
}
