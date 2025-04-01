using DocuCraft.Common.ResultPattern;
using DocuCraft.Domain.Interfaces;

namespace DocuCraft.Domain.Entities
{
    public abstract class Document
    {

        public string Title { get; set; }
        public string Content { get; set; }

        private protected Document(string title)
        {
            Title = title;
            Content = string.Empty;
        }

        // Абстрактный метод для формирования имени файла
        protected abstract string GetFileName(string format);

        // Методы редактирования, например:
        public virtual void InsertText(string text, int position)
        {
            if (position < 0 || position > Content.Length)
                throw new ArgumentOutOfRangeException(nameof(position), "Неверная позиция для вставки текста.");
            Content = Content.Insert(position, text);
        }

        public virtual void DeleteText(int position, int length)
        {
            if (position < 0 || position + length > Content.Length)
                throw new ArgumentOutOfRangeException(nameof(position), "Неверные параметры для удаления текста.");
            Content = Content.Remove(position, length);
        }
    }
}
