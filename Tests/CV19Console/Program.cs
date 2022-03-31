using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19Console
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        //создаем метод который будет возвращать поток из которого можно будет считывать эти данные
        //тобишь будем считывать и скачивать файл поэтапно, извлекать будем только то, что нужно

        private static async Task<Stream> GetDataStream() //создаем метод байт данных
        {
            var client = new HttpClient();//создаем клиента внутри себя
            var response = await client.GetAsync(data_url, //получает ответ от удаленного сервера
                HttpCompletionOption.ResponseHeadersRead); //получаем только заголовки
            return await response.Content.ReadAsStreamAsync();
        }


        private static IEnumerable<string> GetDataLines() //метод, который читает текстовые данные, разбивая их на строки
        {
            using var data_stream = GetDataStream().Result; //отправяем запрос к сереверу и HttpClient скачает только заголовок,
                                                            //при этом все тело ответа останется не принятым
            using var data_reader = new StreamReader(data_stream); //создаем объект, который на основе потока будет читать строковые данные

            while (!data_reader.EndOfStream)//читаем данные до тех пор пока не наступит конец потока
            {
                var line = data_reader.ReadLine(); //извлекаем из ридера очередную строку и помещаем её в переменную

                if (string.IsNullOrWhiteSpace(line)) continue; //проверяем что строка не пуста, если что, то пропускаем ее и делаем следующий цикл
                yield return line;
            }
        }

        private static DateTime[] GetDates() => GetDataLines() //получаем перечисление строк всего запроса
            .First() //берем только первую строку
            .Split(',') //берем первую строку и разбиваем строку по разделителю запятой
            //итого получаем строку с заголовками
            .Skip(4) //отбрасываем первые четыре (страна, провинция, широта и долгота)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture)) //оставшиеся строки преобразуем в DateTime
            .ToArray();



        static void Main(string[] args)
        {
            //находим данные в формате raw, копируем ссылку на них
            //WebClient client = new WebClient(); //класс WebClient отправляет http запросы. С его помощью можно скачивать данные с любых сайтов

            //var client = new HttpClient(); //но в современнлм .NET core есть более новая и современная версия HttpClient

            //var response = client.GetAsync(data_url).Result; //получаем файл
            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach (var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();
            Console.WriteLine(string.Join("\r\n", dates));

            Console.ReadLine();
        }
    }
}
