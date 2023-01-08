using System.Globalization;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace c_sharp.nasavasa.ru.Classes;

public static class TgBotOperations
{
    static Random _rnd = new Random();

    static string[] _greetings = new[]
    {
        "Здравствуй", "Приветствую вас", "Привет", "Салют", "Приветик", "Хай", "Здарова",
        "Ave", "Ас-саляму алейкум", "Алоха", "Ола", "Здравия желаю"
    };

    static string[] _askUserId = new[]
    {
        "Пришли мне мой Id", "Скинь мне мой Id", "Дай Id", "Id"
    };

    static string[] _askExchange = new[]
    {
        "Пришли курс валют", "Валюта", "Курс"
    };

    public static async Task ScanMessage(ITelegramBotClient client, Message message)
    {
        var text = message.Text;
        foreach (var cGreeting in _greetings)
        {
            var regexForGreating = new Regex(cGreeting);
            if (!regexForGreating.IsMatch(text!)) continue;
            await client.SendTextMessageAsync(
                message.Chat.Id,
                _greetings[_rnd.Next(_greetings.Length)]);
            return;
        }

        foreach (var cAskId in _askUserId)
        {
            var regexForAskId = new Regex(cAskId);
            if (!regexForAskId.IsMatch(text!)) continue;
            await client.SendTextMessageAsync(
                message.Chat.Id,
                $"Твой ID: {message.From!.Id}");
            return;
        }

        var regexForMathematicalExample = new Regex("[0-9]+[.]*[0-9]*[-+*/][0-9]+[.]*[0-9]*");
        foreach (Match currentMatch in regexForMathematicalExample.Matches(text!))
        {
            var tmp = currentMatch.Value.Split('+', '-', '*', '/');
            var (firstNumber, secondNumber) = (double.Parse(tmp[0]), double.Parse(tmp[1]));
            var result = 0.0;
            try
            {
                checked
                {
                    switch (currentMatch.Value[tmp[0].Length])
                    {
                        case '+':
                            result = firstNumber + secondNumber;
                            break;
                        case '-':
                            result = firstNumber - secondNumber;
                            break;
                        case '*':
                            result = firstNumber * secondNumber;
                            break;
                        case '/':
                            if (secondNumber == 0)
                            {
                                throw new DivideByZeroException();
                            }

                            result = firstNumber / secondNumber;
                            break;
                    }
                }

                Console.WriteLine(result.ToString(CultureInfo.InvariantCulture));
                await client.SendTextMessageAsync(message.Chat.Id, $"{result:F3}");
                return;
            }
            catch (DivideByZeroException)
            {
                await client.SendTextMessageAsync(message.Chat.Id, "Дурак?");
                return;
            }
        }

        foreach (var cAskExcange in _askExchange)
        {
            var regexForExcange = new Regex(cAskExcange);
            if (!regexForExcange.IsMatch(text)) continue;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("https://www.cbr-xml-daily.ru/latest.js");
            var content = await response.Content.ReadAsStringAsync();
            var api = JsonConvert.DeserializeObject<RatesAPI.Root>(content);
            if (api != null)
                await client.SendTextMessageAsync(message.Chat.Id, $"Курс на {api.date}:\n" +
                                                                   $"    \u25CF 1 \u0024 = {1 / api.rates.USD:F} \u20BD\n" +
                                                                   $"    \u25CF 1 \u00A5 = {1 / api.rates.CNY:F} \u20BD\n" +
                                                                   $"    \u25CF 1 \u20AC = {1 / api.rates.EUR:F} \u20BD");
            return;
        }


        await client.SendTextMessageAsync(message.Chat.Id, "По вашему запросу ничего не нашлось.\n" +
                                                           "Пока что весь функционал ограничен сообщениями:\n" +
                                                           "    \u25CF Привет\n" +
                                                           "    \u25CF Пришли мне мой Id (Id)\n" +
                                                           "    \u25CF Пришли курс валют (Валюта)\n" +
                                                           "    \u25CF Решить пример вида (X[+-*/]X, X - вещественное число, [.] - разделитель дробной части)");
    }
}