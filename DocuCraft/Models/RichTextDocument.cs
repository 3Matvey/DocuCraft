﻿namespace DocuCraft.Models
{
    public class RichTextDocument(string title) : Document(title)
    {
        protected override string GetFileName(string format)
        {
            return format.Equals("txt", StringComparison.CurrentCultureIgnoreCase)
                ? $"{Title}.rtf.txt"
                : $"{Title}.rtf.{format.ToLower()}";
        }
    }
}
