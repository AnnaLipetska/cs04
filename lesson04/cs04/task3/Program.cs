using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task3
{
    class Program
    {
        // Напишите шуточную программу «Дешифратор», которая бы в текстовом файле могла бы
        // заменить все предлоги на слово «ГАВ!»
        static void Main(string[] args)
        {
            var prepositions = new List<string>() { "без", "в", "для", "до", "за", "из", "к", "между", "на", "над", "напротив", "о", "об", "около", "от", "по", "под", "после", "при", "про", "против", "ради", "с", "сверху" /*и т.д. и т.п.*/ };
            var patterns = new List<string>();

            foreach (var preposition in prepositions)
            {
                var prepositionCapitalized = preposition.First().ToString().ToUpper() + preposition.Substring(1);
                
                patterns.Add(@"\s" + preposition + @"\s");
                patterns.Add(@"\s" + prepositionCapitalized + @"\s");
            }

            Console.Write("Введите путь к текстовому файлу: ");
            string path = Console.ReadLine();

            Console.WriteLine(new string('-', 30));

            try
            {
                string text = File.ReadAllText(path);

                Console.WriteLine("Исходный текст:");
                Console.WriteLine(text);
                Console.WriteLine(new string('-', 30));

                foreach (var pattern in patterns)
                {
                    text = Regex.Replace(text, pattern, " ГАВ! ");
                }

                Console.WriteLine("Дешифрованный текст:");
                Console.WriteLine(text);
                Console.WriteLine(new string('-', 30));

                File.WriteAllText("deciphered.txt", text);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}" );
            }

            Console.ReadKey();
        }
    }
}
