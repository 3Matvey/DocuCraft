using DocuCraft.ResultPattern;

namespace DocuCraft.Commands
{
    public interface ICommand
    {
        Result Execute();
        Result UnExecute();
    }
}
