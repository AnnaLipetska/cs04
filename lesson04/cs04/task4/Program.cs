using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task4
{
    class Program
    {
        // Создайте текстовый файл-чек по типу «Наименование товара – 0.00 (цена) грн.» с
        // определенным количеством наименований товаров и датой совершения покупки.Выведите на
        // экран информацию из чека в формате текущей локали пользователя и в формате локали en-US.
        static void Main(string[] args)
        {
            var path = "bill.txt";
            if (!File.Exists(path))
            {
                string text = "лампа - 500.00 грн. - 11.11.2000\nручка - 10 грн. - 12.11.2000\nстол - 2000 грн. - 14.11.2000";
                File.WriteAllText("bill.txt", text);
            }

            var initialLines = File.ReadAllLines(path);
            Console.WriteLine("Исходный текст");

            foreach (var line in initialLines)
            {
                Console.WriteLine(line);
            }

            Console.WriteLine(new string('-', 30));

            var regex = new Regex(@"^(?<name>[^-]+)- (?<price>\S+) грн. - (?<day>\d{1,2}).(?<month>\d{1,2}).(?<year>\d{2,4})$");

            foreach (var line in initialLines)
            {
                var matches = regex.Matches(line);
                if (matches.Count > 0)
                {
                    string name;
                    double price;
                    var us = new CultureInfo("en-US");

                    name = matches[0].Groups["name"].ToString();

                    double.TryParse(matches[0].Groups["price"].ToString().Replace('.', ','), out price);

                    int day, month, year;

                    if (int.TryParse(matches[0].Groups["day"].ToString(), out day) &&
                        int.TryParse(matches[0].Groups["month"].ToString(), out month) &&
                        int.TryParse(matches[0].Groups["year"].ToString(), out year))
                    {
                        var date = new DateTime(year, month, day);
                        Console.WriteLine($"Товар: \"{name}\" по цене " + price.ToString("C") + " был продан " + date.ToShortDateString());
                        Console.WriteLine($"Товар: \"{name}\" по цене " + price.ToString("C", us) + " был продан " + date.ToString(us));
                    }

                    Console.WriteLine();
                }
            }


            Console.ReadKey();
        }
    }
}
