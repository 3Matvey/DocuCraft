namespace DocuCraft.Domain.Entities
{
    public class PlainTextDocument(string title) 
        : Document(title)
    {
        public override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase) 
                ? $"{Title}.txt"
                : $"{Title}.{format.ToLower()}";
        }
    }
}
