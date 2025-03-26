namespace DocuCraft.Models
{
    public class PlainTextDocument(string title) 
        : Document(title)
    {
        public override void Save(string format)
        {
            if (format.Equals("txt"))
            {
                string fileName = $"{Title}.txt";
                File.WriteAllText(fileName, Content);
                Console.WriteLine($"Документ сохранён как {fileName}");
            }
            else
            {
                Console.WriteLine($"Формат {format} не поддерживается для PlainTextDocument.");
            }
        }

        public override void Load(string filePath)
        {
            if (File.Exists(filePath))
            {
                Content = File.ReadAllText(filePath);
                Console.WriteLine($"Документ {filePath} успешно загружен.");
            }
            else
            {
                Console.WriteLine($"Файл {filePath} не найден.");
            }
        }
    }
}
