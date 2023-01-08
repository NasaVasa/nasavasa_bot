using System.Text.RegularExpressions;
using System.Web;

namespace c_sharp.nasavasa.ru;

public static partial class Handlers
{
    public static async Task<IResult> GetWikiLinks(string link)
    {
        try
        {
            using HttpClient client = new HttpClient();
            link = HttpUtility.UrlDecode(link);
            var response = await client.GetAsync(link);
            var inp = await response.Content.ReadAsStringAsync();
            var regexForAll = new Regex(@"href=""http(.+?)""");
            var allMatches = regexForAll.Matches(inp);
            var result = new List<string>();
            foreach (var currentMatch in allMatches)
            {
                result.Add(currentMatch.ToString()![6..^1]);
            }
            var wl = new WikiLinks((uint)allMatches.Count, result);
            return Results.Ok(wl);
        }
        catch (HttpRequestException e)
        {
            return Results.BadRequest("Сайт введён неверно");
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    private class WikiLinks
    {
        private uint _quantity;
        public uint Quantity
        {
            get => _quantity;
            set
            {
                if (value<0)
                {
                    throw new ArgumentException("Количество ссылок должно быть неотрицательным");
                }

                _quantity = value;
            }
        }
        private List<string> _links;
        public  List<string>  Links
        {
            get => _links;
            set => _links = value;
        }

        public WikiLinks(uint quantity, List<string> links)
        {
            Quantity = quantity;
            Links = links;
        }
    }
}