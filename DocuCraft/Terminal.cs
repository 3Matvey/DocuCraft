using System;
using DocuCraft.Managers;
using DocuCraft.Models;
using DocuCraft.ResultPattern;

namespace DocuCraft
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
                        OpenDocument();
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
                        SaveDocument();
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

        private void OpenDocument()
        {
            string type = GetDocumentType();
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            Console.Write("Введите путь к файлу (оставьте пустым для рабочей директории): ");
            string filePath = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory;

            var result = _docManager.OpenDocument(type, title, filePath);
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
            try
            {
                docToEdit.InsertText(text, position);
                Console.WriteLine("Текст успешно вставлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка редактирования: {ex.Message}");
            }
        }

        private void SaveDocument()
        {
            Console.Write("Введите название документа для сохранения: ");
            string title = Console.ReadLine()?.Trim() ?? "";
            var resultGet = _docManager.GetDocument(title);
            if (!resultGet.IsSuccess)
            {
                Console.WriteLine($"Ошибка: {resultGet.Error?.Description}");
                return;
            }

            Document docToSave = resultGet.Value;
            Console.Write("Введите формат для сохранения (txt, json, xml): ");
            string format = Console.ReadLine()?.Trim().ToLower() ?? "";
            try
            {
                docToSave.Save(format);
                Console.WriteLine($"Документ \"{title}\" успешно сохранён.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения: {ex.Message}");
            }
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
