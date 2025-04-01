//using DocuCraft.Models;

//namespace DocuCraft.Factories
//{
//    public static class DocumentFactory
//    {
//        public static Document CreateDocument(string type, string title)
//         => type.ToLower() switch
//         {
//             "plaintext" => new PlainTextDocument(title),
//             "markdown" => new MarkdownDocument(title),
//             "richtext" => new RichTextDocument(title),
//             _ => throw new ArgumentException("Неизвестный тип документа."),
//         };
//    }
//}
using DocuCraft.Models;

namespace DocuCraft.Factories
{
    public static class DocumentFactory
    {
        // Используем словарь для регистрации фабричных методов по типу документа.
        private static readonly Dictionary<string, Func<string, Document>> _creators =
            new(StringComparer.OrdinalIgnoreCase)
            {
                ["plaintext"] = title => new PlainTextDocument(title),
                ["markdown"] = title => new MarkdownDocument(title),
                ["richtext"] = title => new RichTextDocument(title)
            };
        
        // Позволяем расширять фабрику, регистрируя новые типы документов
        public static void RegisterDocumentCreator(string type, Func<string, Document> creator)
        {
            _creators[type] = creator;
        }

        public static Document CreateDocument(string type, string title)
        {
            if (_creators.TryGetValue(type, out var creator))
            {
                return creator(title);
            }
            throw new ArgumentException("Неизвестный тип документа.", nameof(type));
        }
    }
}
