namespace DocuCraft.Models
{
    public abstract class Document(string title)
    {
        public string Title { get; set; } = title;
        public string Content { get; set; } = "";

        public abstract void Save(string format);
        public abstract void Load(string filePath);

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
