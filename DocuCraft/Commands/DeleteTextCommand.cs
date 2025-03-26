using DocuCraft.Models;

namespace DocuCraft.Commands
{
    public class DeleteTextCommand(Document document, int position, int length) 
        : ICommand
    {
        private string _deletedText = string.Empty;

        public void Execute()
        {
            // Сохраняем удаляемый текст для возможности отмены операции
            _deletedText = document.Content.Substring(position, length);
            document.DeleteText(position, length);
        }

        public void UnExecute()
        {
            document.InsertText(_deletedText, position);
        }
    }
}
