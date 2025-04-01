namespace DocuCraft.Application.Commands
{
    public interface ICommand
    {
        Result Execute();
        Result UnExecute();
    }
}
