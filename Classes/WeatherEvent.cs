using System.Globalization;
using System.Text.Json;
using MySql.Data.MySqlClient;

class WeatherEvent
{
    public WeatherEvent(
        int eventId,
        Type type,
        Severity severity,
        DateTime startTime,
        DateTime endTime,
        float precipitation,
        string timeZone,
        string airportCode,
        float locationLat,
        float locationLng,
        string city,
        string county,
        string state,
        int zipCode
    )
    {
        EventId = eventId;
        Type = type;
        Severity = severity;
        StartTime = startTime;
        EndTime = endTime;
        Precipitation = precipitation;
        TimeZone = timeZone;
        AirportCode = airportCode;
        LocationLat = locationLat;
        LocationLng = locationLng;
        City = city;
        County = county;
        State = state;
        ZipCode = zipCode;
    }

    public int EventId { get; set; }
    public Type Type { get; set; }
    public Severity Severity { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public float Precipitation { get; set; }

    public string TimeZone { get; set; }
    public string AirportCode { get; set; }
    public float LocationLat { get; set; }
    public float LocationLng { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string State { get; set; }
    public int ZipCode { get; set; }
    public async Task ToDataBase(MySqlConnection mySqlConnection, string database)
    {
        var cmd = new MySqlCommand($"INSERT INTO {database} "+
                                  "(EventId, TypeId,SeverityId,StartTime,EndTime,Precipitation,TimeZone,AirportCode,LocationLat,LocationLng,City,County,State,ZipCode) "+
                                  $"VALUES ({EventId}, {(int)Type}, {(int)Severity}, '{StartTime.ToString("yyyy-MM-dd HH:mm:ss")}', '{EndTime.ToString("yyyy-MM-dd HH:mm:ss")}', {Precipitation.ToString(CultureInfo.InvariantCulture)}, '{TimeZone}', '{AirportCode}', {LocationLat.ToString(CultureInfo.InvariantCulture)}, {LocationLng.ToString(CultureInfo.InvariantCulture)}, '{City}', '{County}', '{State}', {ZipCode})",mySqlConnection);
         await cmd.ExecuteNonQueryAsync();
    }
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }

    public static WeatherEvent Parse(string str)
    {
        var arr = str.Split(',');
        var eventId = int.Parse(arr[0][2..]);
        var type = Enum.Parse<Type>(arr[1]);
        var severity = Enum.Parse<Severity>(arr[2]);
        var startTime = DateTime.Parse(arr[3]);
        var endTime = DateTime.Parse(arr[4]);
        var precipitation = float.Parse(arr[5]);
        var timeZone =arr[6];
        var airportCode  =arr[7];
        var locationLat = float.Parse(arr[8]);
        var locationLng = float.Parse(arr[9]);
        var city = arr[10];
        var country = arr[11];
        var state = arr[12];
        var zipCode = int.Parse(arr[13]);
        return new WeatherEvent(
            eventId,
            type,
            severity,
            startTime,
            endTime,
            precipitation,
            timeZone,
            airportCode,
            locationLat,
            locationLng,
            city,
            country,
            state,
            zipCode);
    }

    
}

enum Type
{
    Snow = 1,
    Fog = 2,
    Cold = 3,
    Storm = 4,
    Rain = 5,
    Precipitation = 6,
    Hail = 7
}

enum Severity
{
    Light = 1,
    Severe = 2,
    Moderate = 3,
    Heavy = 4,
    UNK = 5,
    Other =6
}