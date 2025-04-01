using DocuCraft.Managers;
using DocuCraft;

var docManager = new DocumentManager();
var terminal = new Terminal(docManager);
terminal.Run();
