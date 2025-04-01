using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft.Commands
{
    public class InsertTextCommand(Document document, string text, int position) : ICommand
    {
        public Result Execute()
        {
            try
            {
                document.InsertText(text, position);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("INS001", ex.Message);
            }
        }

        public Result UnExecute()
        {
            try
            {
                document.DeleteText(position, text.Length);
                return Result.Success();
            }
            catch (Exception ex)
            {
                return Error.Failure("INS002", ex.Message);
            }
        }
    }
}
