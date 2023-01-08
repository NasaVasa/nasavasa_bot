using System.Text.Json;
using c_sharp.nasavasa.ru.Classes;
using MySql.Data.MySqlClient;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace c_sharp.nasavasa.ru;

public static partial class Handlers
{
    static MySqlConnectionStringBuilder connectionStr = new MySqlConnectionStringBuilder
    {
        Server = "localhost",
        Database = "nasavasa_bot",
        UserID = "www-data",
        Password = "Wcf3g3dsFA2?",
        ConnectionTimeout = 60,
        Port = 3306,
        AllowZeroDateTime = true
    };

    public static async Task UpdateHandler(
        ITelegramBotClient client,
        Update update,
        CancellationToken ct)
    {
        try
        {
            Console.WriteLine("YESSSS");
            if (update.Message is not null && update.Type is UpdateType.Message && !update.Message.From!.IsBot)
            {
                var id = update.Id;
                var username = update.Message.From.Username!;
                var text = update.Message.Text!;
                var date = update.Message.Date.ToString("yyyy-MM-dd HH:mm:ss");
                /*await using (var connection = new MySqlConnection(connectionStr.ConnectionString))
                {
                    await connection.OpenAsync();
                    var cmd = new MySqlCommand($"INSERT INTO messages VALUES ({id}, '{username}', " + 
                                               $"'{text}', '{date}');", connection);
                    await cmd.ExecuteNonQueryAsync();
                }*/
                await TgBotOperations.ScanMessage(client,update.Message);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(
                $"{e.Message}\n{e.InnerException}\n");
        }
    }

    public static async Task ErrorHandler(
        ITelegramBotClient client,
        Exception ex,
        CancellationToken ct)
    {
    }
}