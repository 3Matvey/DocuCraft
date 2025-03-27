using System;
using System.Collections.Generic;
using DocuCraft.Models;
using DocuCraft.Factories;

namespace DocuCraft.Managers
{
    public class DocumentManager
    {
        private readonly Dictionary<string, Document> _documents = [];

        // Создание нового документа
        public Document CreateDocument(string type, string title)
        {
            if (_documents.ContainsKey(title))
            {
                throw new Exception("Документ с таким названием уже существует.");
            }
            Document doc = DocumentFactory.CreateDocument(type, title);
            _documents.Add(title, doc);
            return doc;
        }

        // Открытие документа из файловой системы
        public Document OpenDocument(string type, string title, string filePath)
        {
            Document doc = DocumentFactory.CreateDocument(type, title);
            doc.Load(filePath);
            _documents[title] = doc;
            return doc;
        }

        // Получение документа для редактирования
        public Document GetDocument(string title)
        {
            if (_documents.TryGetValue(title, out Document? doc))
            {
                return doc;
            }
            throw new Exception("Документ не найден.");
        }

        // Удаление документа
        public void DeleteDocument(string title)
        {
            if (_documents.Remove(title))
                Console.WriteLine($"Документ {title} удалён из менеджера.");
            else
                Console.WriteLine($"Документ {title} не найден.");
        }

        // Вывод списка открытых документов
        public IEnumerable<Document> ListDocuments()
        {
            return _documents.Values;
        }
    }
}
