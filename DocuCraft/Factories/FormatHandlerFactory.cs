using System;
using DocuCraft.Formats;

namespace DocuCraft.Factories
{
    public static class FormatHandlerFactory
    {
        public static IFormatHandler GetHandler(string format)
        {
            return format.ToLower() switch
            {
                "txt" => new TxtFormatHandler(),
                "json" => new JsonFormatHandler(),
                "xml" => new XmlFormatHandler(),
                _ => throw new ArgumentException("Unsupported format", nameof(format)),
            };
        }
    }
}
