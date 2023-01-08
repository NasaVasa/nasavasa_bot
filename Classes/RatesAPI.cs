namespace c_sharp.nasavasa.ru.Classes;

public class RatesAPI
{
    public class Root
    {
        public string disclaimer { get; init; }
        public string date { get; init; }
        public int timestamp { get; init; }
        public string @base { get; init; }
        public Rates rates { get; init; }
    }

    public class Rates
    {
        public double AUD { get; init; }
        public double AZN { get; init; }
        public double GBP { get; init; }
        public double AMD { get; init; }
        public double BYN { get; init; }
        public double BGN { get; init; }
        public double BRL { get; init; }
        public double HUF { get; init; }
        public double HKD { get; init; }
        public double DKK { get; init; }
        public double USD { get; init; }
        public double EUR { get; init; }
        public double INR { get; init; }
        public double KZT { get; init; }
        public double CAD { get; init; }
        public double KGS { get; init; }
        public double CNY { get; init; }
        public double MDL { get; init; }
        public double NOK { get; init; }
        public double PLN { get; init; }
        public double RON { get; init; }
        public double XDR { get; init; }
        public double SGD { get; init; }
        public double TJS { get; init; }
        public double TRY { get; init; }
        public double TMT { get; init; }
        public double UZS { get; init; }
        public double UAH { get; init; }
        public double CZK { get; init; }
        public double SEK { get; init; }
        public double CHF { get; init; }
        public double ZAR { get; init; }
        public double KRW { get; init; }
        public double JPY { get; init; }    
    }
}