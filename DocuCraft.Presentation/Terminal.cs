using DocuCraft.Application.Commands;
using DocuCraft.Application.Managers;
using DocuCraft.Domain.Entities;

namespace DocuCraft.Presentation
{
    public class Terminal
    {
        private readonly DocumentManager _docManager;

        public Terminal(DocumentManager docManager)
        {
            _docManager = docManager;
        }

        public void Run()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("===========================================");
                Console.WriteLine("Менеджер документов DocuCraft");
                Console.WriteLine("===========================================");
                ShowMenu();

                string choice = Console.ReadLine()?.Trim() ?? string.Empty;
                switch (choice)
                {
                    case "1":
                        CreateDocument();
                        break;
                    case "2":
                        OpenDocumentAsync().GetAwaiter().GetResult();
                        break;
                    case "3":
                        ListDocuments();
                        break;
                    case "4":
                        DeleteDocument();
                        break;
                    case "5":
                        EditDocument();
                        break;
                    case "6":
                        SaveDocumentAsync().GetAwaiter().GetResult();
                        break;
                    case "7":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        private static void ShowMenu()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать новый документ");
            Console.WriteLine("2. Открыть документ");
            Console.WriteLine("3. Просмотреть список документов");
            Console.WriteLine("4. Удалить документ");
            Console.WriteLine("5. Редактировать документ");
            Console.WriteLine("6. Сохранить документ");
            Console.WriteLine("7. Выход");
            Console.Write("Ваш выбор: ");
        }

        private static string GetDocumentType()
        {
            Console.Clear();
            Console.WriteLine("Выберите тип документа:");
            Console.WriteLine("1. PlainText");
            Console.WriteLine("2. Markdown");
            Console.WriteLine("3. RichText");
            Console.Write("Номер типа документа: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            return input switch
            {
                "1" => "plaintext",
                "2" => "markdown",
                "3" => "richtext",
                _ => GetDocumentType()
            };
        }

        private void CreateDocument()
        {
            string type = GetDocumentType();
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine()?.Trim() ?? "";

            var result = _docManager.CreateDocument(type, title);
            if (result.IsSuccess)
                Console.WriteLine($"Документ \"{title}\" успешно создан.");
            else
                Console.WriteLine($"Ошибка: {result.Error?.Description}");
        }

        private async Task OpenDocumentAsync()
        {
            string type = GetDocumentType();
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            if (!title.Contains('.')) 
            {
                title += ".txt";
            }
            Console.Write("Введите путь к файлу (оставьте пустым для рабочей директории): ");
            string filePath = Console.ReadLine()?.Trim() ?? "";
            const char separator = '\\';
            if (string.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory + separator + title;

            var result = await _docManager.OpenDocumentAsync(type, title, filePath);
            if (result.IsSuccess)
            {
                Console.WriteLine($"Документ \"{title}\" успешно открыт.");
                Console.WriteLine("Содержимое документа:");
                Console.WriteLine(result.Value.Content);
            }
            else
            {
                Console.WriteLine($"Ошибка: {result.Error?.Description}");
            }
        }

        private void ListDocuments()
        {
            var docs = _docManager.ListDocuments();
            Console.WriteLine("\nСписок документов:");
            foreach (var doc in docs)
            {
                Console.WriteLine($"- {doc.Title}");
            }
        }

        private void DeleteDocument()
        {
            Console.Write("Введите название документа для удаления: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            var result = _docManager.DeleteDocument(title);
            if (result.IsSuccess)
                Console.WriteLine($"Документ \"{title}\" успешно удалён.");
            else
                Console.WriteLine($"Ошибка: {result.Error?.Description}");
        }

        private void EditDocument()
        {
            Console.Write("Введите название документа для редактирования: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            var resultGet = _docManager.GetDocument(title);
            if (!resultGet.IsSuccess)
            {
                Console.WriteLine($"Ошибка: {resultGet.Error?.Description}");
                return;
            }

            Document docToEdit = resultGet.Value;
            Console.WriteLine("Текущий контент:");
            Console.WriteLine(docToEdit.Content);
            Console.Write("Введите текст для вставки: ");
            string text = Console.ReadLine() ?? "";
            int position = GetIntegerInput("Введите позицию вставки: ");
            var command = new InsertTextCommand(docToEdit, text, position);
            var result = command.Execute();
            Console.WriteLine(result.IsSuccess ? "Текст успешно вставлен." : $"Ошибка редактирования: {result.Error?.Description}");
        }

        private async Task SaveDocumentAsync()
        {
            Console.Write("Введите название документа для сохранения: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Введите формат для сохранения (txt, json, xml): ");
            string format = Console.ReadLine()?.Trim().ToLower() ?? "";
            var result = await _docManager.SaveDocumentAsync(title, format);
            Console.WriteLine(result.IsSuccess ? $"Документ \"{title}\" успешно сохранён." : $"Ошибка сохранения: {result.Error?.Description}");
        }

        private static int GetIntegerInput(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim() ?? "";
                if (int.TryParse(input, out value))
                    break;
                Console.WriteLine("Неверный ввод. Попробуйте снова.");
            }
            return value;
        }
    }
}
