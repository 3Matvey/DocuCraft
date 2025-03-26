namespace DocuCraft.Commands
{
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
