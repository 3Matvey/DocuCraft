using System;
using DocuCraft.Managers;
using DocuCraft.Models;

namespace DocuCraft
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в DocuCraft Document Manager!");

            DocumentManager docManager = new DocumentManager();
            bool exit = false;

            while (!exit)
            {
                Console.Clear();  // Очистка консоли для лучшего восприятия
                Console.WriteLine("===========================================");
                Console.WriteLine("Добро пожаловать в менеджер документов!");
                Console.WriteLine("===========================================");
                ShowMenu();

                string choice = Console.ReadLine().Trim();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateDocument(docManager);
                            break;
                        case "2":
                            OpenDocument(docManager);
                            break;
                        case "3":
                            ListDocuments(docManager);
                            break;
                        case "4":
                            DeleteDocument(docManager);
                            break;
                        case "5":
                            EditDocument(docManager);
                            break;
                        case "6":
                            SaveDocument(docManager);
                            break;
                        case "7":
                            exit = true;
                            Console.WriteLine("\nВыход из приложения...");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор, попробуйте снова.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("\nОшибка: " + ex.Message);
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        // Отображение меню
        static void ShowMenu()
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

        // Создание нового документа
        static void CreateDocument(DocumentManager docManager)
        {
            Console.Write("\nВведите тип документа (plaintext, markdown, richtext): ");
            string type = Console.ReadLine().Trim().ToLower();
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine().Trim();

            Document newDoc = docManager.CreateDocument(type, title);
            Console.WriteLine($"Документ \"{title}\" успешно создан.");
        }

        // Открытие документа
        static void OpenDocument(DocumentManager docManager)
        {
            Console.Write("\nВведите тип документа (plaintext, markdown, richtext): ");
            string type = Console.ReadLine().Trim().ToLower();
            Console.Write("Введите название документа: ");
            string title = Console.ReadLine().Trim();
            Console.Write("Введите путь к файлу (оставьте пустым для текущей директории): ");
            string filePath = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(filePath))
                filePath = Environment.CurrentDirectory;

            Document openedDoc = docManager.OpenDocument(type, title, filePath);
            Console.WriteLine($"\nДокумент \"{title}\" успешно открыт.");
            Console.WriteLine("\nТекущий контент документа:");
            Console.WriteLine(openedDoc.Content);
        }

        // Просмотр списка документов
        static void ListDocuments(DocumentManager docManager)
        {
            Console.WriteLine("\nСписок документов:");
            foreach (var doc in docManager.ListDocuments())
            {
                Console.WriteLine($"- {doc.Title}");
            }
        }

        // Удаление документа
        static void DeleteDocument(DocumentManager docManager)
        {
            Console.Write("\nВведите название документа для удаления: ");
            string title = Console.ReadLine().Trim();
            docManager.DeleteDocument(title);
            Console.WriteLine($"Документ \"{title}\" удалён.");
        }

        // Редактирование документа
        static void EditDocument(DocumentManager docManager)
        {
            Console.Write("\nВведите название документа для редактирования: ");
            string title = Console.ReadLine().Trim();
            Document docToEdit = docManager.GetDocument(title);

            Console.WriteLine("\nТекущий контент:");
            Console.WriteLine(docToEdit.Content);

            Console.Write("\nВведите текст для вставки: ");
            string text = Console.ReadLine();
            int pos = GetIntegerInput("Введите позицию вставки: ");
            docToEdit.InsertText(text, pos);
            Console.WriteLine("\nТекст успешно вставлен.");
        }

        // Сохранение документа
        static void SaveDocument(DocumentManager docManager)
        {
            Console.Write("\nВведите название документа для сохранения: ");
            string title = Console.ReadLine().Trim();
            Document docToSave = docManager.GetDocument(title);
            Console.Write("\nВведите формат для сохранения (txt, json, xml): ");
            string format = Console.ReadLine().Trim().ToLower();
            docToSave.Save(format);
            Console.WriteLine($"Документ \"{title}\" успешно сохранён.");
        }

        // Получение целочисленного ввода с проверкой
        static int GetIntegerInput(string prompt)
        {
            int value;
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine().Trim();
                if (int.TryParse(input, out value))
                    break;
                Console.WriteLine("Неверный формат числа, попробуйте снова.");
            }
            return value;
        }
    }
}
