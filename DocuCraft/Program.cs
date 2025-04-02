using DocuCraft.Application.Managers;
using DocuCraft.Presentation;
using DocuCraft.Infrastructure.Storage;
using DocuCraft.Infrastructure.Formats;

var txt = new TxtFormatHandler();
var localStorageStrategy = new LocalStorageStrategy(txt);
var docManager = new DocumentManager(localStorageStrategy);
var terminal = new Terminal(docManager);
terminal.Run();
