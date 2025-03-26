using DocuCraft.Models;
using DocuCraft.Factories;
using DocuCraft.Commands;
using DocuCraft.Storage;
using DocuCraft.Services;

namespace DocuCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в DocuCraft!");

            // Пример создания документа через фабрику
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine();
            Document document = DocumentFactory.CreateDocument("plaintext", title);
            Console.WriteLine("Документ создан.");

            // Создание менеджера команд для поддержки undo/redo
            CommandManager commandManager = new CommandManager();

            // Пример операции: вставка текста
            Console.Write("Введите текст для вставки: ");
            string textToInsert = Console.ReadLine();
            Console.Write("Введите позицию для вставки (0 для начала): ");
            int pos = int.Parse(Console.ReadLine());

            var insertCommand = new InsertTextCommand(document, textToInsert, pos);
            commandManager.ExecuteCommand(insertCommand);

            Console.WriteLine("\nТекущий контент:");
            Console.WriteLine(document.Content);

            // Демонстрация undo
            Console.WriteLine("\nВыполняем undo...");
            commandManager.Undo();
            Console.WriteLine("Текущий контент после undo:");
            Console.WriteLine(document.Content);

            // Демонстрация redo
            Console.WriteLine("\nВыполняем redo...");
            commandManager.Redo();
            Console.WriteLine("Текущий контент после redo:");
            Console.WriteLine(document.Content);

            // Пример сохранения документа через локальное хранилище
            IStorageStrategy storage = new LocalStorageStrategy();
            StorageManager storageManager = new StorageManager(storage);
            storageManager.SaveDocument(document, "txt");

            // Пример работы с настройками через Singleton
            SettingsManager.Instance.SetTheme("Dark");
            Console.WriteLine("\nТекущая тема: " + SettingsManager.Instance.Theme);

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
