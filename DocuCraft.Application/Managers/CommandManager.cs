using DocuCraft.Application.Commands;

namespace DocuCraft.Application.Managers
{
    public class CommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public Result ExecuteCommand(ICommand command)
        {
            var result = command.Execute();
            if (result.IsSuccess)
            {
                _undoStack.Push(command);
                _redoStack.Clear();
            }
            return result;
        }

        public Result Undo()
        {
            if (_undoStack.Count > 0)
            {
                ICommand command = _undoStack.Pop();
                var result = command.UnExecute();
                if (result.IsSuccess)
                    _redoStack.Push(command);
                return result;
            }
            return Error.Failure("CMD001", "Undo stack is empty");
        }

        public Result Redo()
        {
            if (_redoStack.Count > 0)
            {
                ICommand command = _redoStack.Pop();
                var result = command.Execute();
                if (result.IsSuccess)
                    _undoStack.Push(command);
                return result;
            }
            return Error.Failure("CMD002", "Redo stack is empty");
        }
    }
}
