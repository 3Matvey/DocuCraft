using DocuCraft.Application.Managers;
using DocuCraft.Presentation;
using DocuCraft.Infrastructure.Storage;

var localStorageStrategy = new LocalStorageStrategy();
var docManager = new DocumentManager(localStorageStrategy);
var terminal = new Terminal(docManager);
terminal.Run();
