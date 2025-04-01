using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Domain.Entities
{
    public class MarkdownDocument(string title) 
        : Document(title)
    {
        protected override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase)
                ? $"{Title}.md.txt"
                : $"{Title}.md.{format.ToLower()}";
        }
    }
}
