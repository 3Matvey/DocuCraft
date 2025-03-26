using DocuCraft.Models;

namespace DocuCraft.Factories
{
    public static class DocumentFactory
    {
        public static Document CreateDocument(string type, string title)
         => type.ToLower() switch
         {
             "plaintext" => new PlainTextDocument(title),
             _ => throw new ArgumentException("Неизвестный тип документа."),
         };
    }
}
