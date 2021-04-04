using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Напишите программу, которая бы позволила вам по указанному адресу web - страницы
            // выбирать все ссылки на другие страницы, номера телефонов, почтовые адреса и сохраняла
            // полученный результат в файл.

            // Тестовый url:
            // https://itea.ua/contacts/

            Console.Write("Укажите адрес web-страницы: ");
            string url = Console.ReadLine();

            string webPageText = GetWebPageText(url);

            var linkRegex = new Regex(@"href=""(?<link>[^#]\S+)"">");
            var phoneRegex = new Regex(@"(?<phone>[+3(0-90-90-9)\s]{2,}[0-9]{3}[\s\-][0-9]{2}[\s\-][0-9]{2})");
            var emailRegex = new Regex(@"(?<email>[0-9A-Za-z_.-]+@[0-9a-zA-Z-]+\.[a-zA-Z]{2,4})");

            using (var writer = File.CreateText("result.txt"))
            {
                var linkMatches = linkRegex.Matches(webPageText);
                if (linkMatches.Count > 0)
                {
                    writer.WriteLine("Ссылки:");

                    foreach (Match match in linkMatches)
                    {
                        writer.WriteLine(match.Groups["link"]);
                    }

                    writer.WriteLine();
                }

                var phoneMatches = phoneRegex.Matches(webPageText);
                if (phoneMatches.Count > 0)
                {
                    writer.WriteLine("Номера телефонов:");

                    foreach (Match match in phoneMatches)
                    {
                        writer.WriteLine(match.Groups["phone"]);
                    }

                    writer.WriteLine();
                }

                var emailMatches = emailRegex.Matches(webPageText);
                if (emailMatches.Count > 0)
                {
                    writer.WriteLine("Адреса email:");

                    foreach (Match match in emailMatches)
                    {
                        writer.WriteLine(match.Groups["email"]);
                    }

                    writer.WriteLine();
                }
            }

            Console.WriteLine("Файл с найденными данными успешно сохранён.");
            Console.ReadKey();
        }

        private static string GetWebPageText(string url)
        {
            WebRequest request = WebRequest.Create(url);
            try
            {
                using (var response = request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            return reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"No response from the url {url}");
                return string.Empty;
            }

        }
    }
}
