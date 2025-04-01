using DocuCraft.Models;

namespace DocuCraft.Factories
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

        public static Document CreateDocumentByType(string docType, string title)
        {
            string normalized = docType.ToLower();
            if (normalized.EndsWith("document"))
            {
                normalized = normalized.Substring(0, normalized.Length - "document".Length);
            }
            return CreateDocument(normalized, title);
        }
    }
}
