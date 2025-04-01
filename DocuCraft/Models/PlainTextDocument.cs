namespace DocuCraft.Models
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
