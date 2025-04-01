using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Commands
{
    public class DeleteTextCommand(Document document, int position, int length) 
        : ICommand
    {
        private string _deletedText = string.Empty;

        public Result Execute()
        {
            try
            {
                // Сохраняем удаляемый текст для возможности отмены
                _deletedText = document.Content.Substring(position, length);
                document.DeleteText(position, length);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("DEL001", ex.Message);
            }
        }

        public Result UnExecute()
        {
            try
            {
                document.InsertText(_deletedText, position);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("DEL002", ex.Message);
            }
        }
    }
}
