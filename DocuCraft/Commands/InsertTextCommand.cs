using DocuCraft.Models;

namespace DocuCraft.Commands
{
    public class InsertTextCommand(Document document, string text, int position) 
        : ICommand
    {
        public void Execute()
        {
            document.InsertText(text, position);
        }

        public void UnExecute()
        {
            document.DeleteText(position, text.Length);
        }
    }
}
