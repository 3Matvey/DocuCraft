using System;
using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Entities;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Infrastructure.Formats
{
    public class TxtFormatHandler : IFormatHandler
    {
        public string Serialize(Document doc)
        {
            return doc.Content;
        }

        public Result<Document> Deserialize(string data)
        {
            try
            {
                string title = "Untitled";
                Document doc = new PlainTextDocument(title)
                {
                    Content = data
                };
                return Result<Document>.Success(doc);
            }
            catch (Exception ex)
            {
                return Error.Failure("TxtDeserializeError", ex.Message);
            }
        }
    }
}
