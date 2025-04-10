﻿namespace DocuCraft.Domain.Entities
{
    public class MarkdownDocument(string title) 
        : Document(title)
    {
        public override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase)
                ? $"{Title}.md.txt"
                : $"{Title}.md.{format.ToLower()}";
        }
    }
}
