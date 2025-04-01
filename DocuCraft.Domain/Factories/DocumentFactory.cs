using DocuCraft.Domain.Entities;

namespace DocuCraft.Domain.Factories
{
    public static class DocumentFactory
    {
        public static Document CreateDocument(string type, string title)
        {
            return type.ToLower() switch
            {
                "plaintext" or "plaintextdocument" => new PlainTextDocument(title),
                "markdown" or "markdowndocument" => new MarkdownDocument(title),
                "richtext" or "richtextdocument" => new RichTextDocument(title),
                _ => throw new ArgumentException("Неизвестный тип документа."),
            };
        }
    }
}
