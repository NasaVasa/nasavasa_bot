
using MySql.Data.MySqlClient;

namespace c_sharp.nasavasa.ru;

public static partial class Handlers
{
    public static async Task<IResult> PostInitialization()
    {
        try
        {
            using (var sr = new StreamReader("./././WeatherEvents.csv"))
            {
                await using (var connection = new MySqlConnection(connectionStr.ConnectionString))
                {
                    await connection.OpenAsync();
                    var cmd = new MySqlCommand("DELETE FROM WeatherEvents;", connection);
                    await cmd.ExecuteNonQueryAsync();
                    var currentString = sr.ReadLine();
                    for (int i = 0; i < 1000; i++)
                    {
                        currentString = sr.ReadLine();
                        var currentWeatherEvent = WeatherEvent.Parse(currentString);
                        await currentWeatherEvent.ToDataBase(connection, "WeatherEvents");
                    }
                    
                }
            }

            return Results.Ok("OK");
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Data}\n{e.Message}\n{e.Source}\n{e.HelpLink}\n{e.HResult}\n{e.InnerException}\n{e.StackTrace}");
            return Results.BadRequest(e.Message);
        }
    }
}