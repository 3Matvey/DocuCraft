namespace DocuCraft.Domain.Interfaces
{
    public interface IFormatHandlerFactory
    {
        IFormatHandler GetHandler(string format);
    }
}
