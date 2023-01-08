using Telegram.Bot;
using c_sharp.nasavasa.ru;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
var tgBot = new TelegramBotClient("TOP SECRET");
tgBot.StartReceiving(
    Handlers.UpdateHandler,
    Handlers.ErrorHandler);
app.UseSwagger();
app.UseSwaggerUI();
app.MapGet("/", () => Results.LocalRedirect("/swagger"));
app.MapGet("/task1/{n:int}/{k:int}/{inp}", Handlers.GetWord);
app.MapGet("/task2/{link}", Handlers.GetWikiLinks);
//app.MapPost("/task3/Initialization", Handlers.PostInitialization);
app.Run();
