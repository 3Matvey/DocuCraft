namespace DocuCraft.Domain.Entities
{
    public class RichTextDocument(string title)
        : Document(title)
    {
        public override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase)
                ? $"{Title}.rtf.txt"
                : $"{Title}.rtf.{format.ToLower()}";
        }
    }
}
