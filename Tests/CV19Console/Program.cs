using System;
using System.Net;
using System.Net.Http;

namespace CV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        static void Main(string[] args)
        {
            //находим данные в формате raw, копируем ссылку на них
            //WebClient client = new WebClient(); //класс WebClient отправляет http запросы. С его помощью можно скачивать данные с любых сайтов

            var client = new HttpClient(); //но в современнлм .NET core есть более новая и современная версия HttpClient

            var response = client.GetAsync(data_url).Result; //получаем файл
            var csv_str = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }
    }
}
