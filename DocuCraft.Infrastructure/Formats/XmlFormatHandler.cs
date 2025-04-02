using System.Xml.Linq;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Factories;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Formats
{
    public class XmlFormatHandler : IFormatHandler
    {
        public string Serialize(Document doc)
        {
            var xml = new XElement("Document",
                        new XElement("DocumentType", doc.GetType().Name),
                        new XElement("Title", doc.Title),
                        new XElement("Content", doc.Content));
            return xml.ToString();
        }

        public Result<Document> Deserialize(string data)
        {
            try
            {
                XElement xml = XElement.Parse(data);
                string docType = xml.Element("DocumentType")?.Value ?? "";
                string title = xml.Element("Title")?.Value ?? "";
                string content = xml.Element("Content")?.Value ?? "";

                Document doc = DocumentFactory.CreateDocument(docType, title);
                doc.Content = content;
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("XmlDeserializeError", ex.Message);
            }
        }
    }
}
