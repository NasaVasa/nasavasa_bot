using Microsoft.AspNetCore.Http;

namespace c_sharp.nasavasa.ru;

public static partial class Handlers
{
    public static IResult GetWord(int n, int k, string inp)
    {
        try
        {
            var _rnd = new Random();
            var len = inp.Length;
            if (n < 1)
            {
                return Results.BadRequest("Длина списка строк меньше 1");
            }

            if (k < 1)
            {
                return Results.BadRequest("Длина строки меньше 1");
            }

            for (var i = 0; i < len - 1; i++)
            {
                for (var j = i + 1; j < len; j++)
                {
                    if (inp[i] == inp[j])
                    {
                        return Results.BadRequest("В списке обнаружены два одинаковых элемента");
                    }
                }
            }

            var stringResList = new List<string>();
            for (var i = 0; i < n; i++)
            {
                var currentString = "";
                for (var j = 0; j < k; j++)
                {
                    currentString += inp[_rnd.Next(0, len)];
                }

                stringResList.Add(currentString);
            }


            return Results.Ok(stringResList);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
}