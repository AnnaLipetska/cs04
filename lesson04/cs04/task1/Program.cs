using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace task1
{
    class Program
    {
        // Напишите консольное приложение, позволяющие пользователю зарегистрироваться под
        // «Логином», состоящем только из символов латинского алфавита, и пароля, состоящего из
        // цифр и символов.

        static void Main(string[] args)
        {
            var loginPattern = @"^[a-zA-Z]+$";
            var passwordPattern = @"^[0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]+$";
            //var passwordPattern = @"\d\D";
            for (; ; )
            {
                Console.Write("Введите логин: ");
                if (!Regex.IsMatch(Console.ReadLine(), loginPattern))
                {
                    Console.WriteLine("Логин должен состоять только из букв латинского алфавита.");
                    continue;
                }

                break;                
            }

            for (; ; )
            {
                Console.Write("Введите пароль: ");
                if (!Regex.IsMatch(Console.ReadLine(), passwordPattern))
                {
                    Console.WriteLine("Пароль должен состоять только из цифр и символов.");
                    continue;
                }

                break;
            }

            Console.WriteLine("Вы успешно прошли регистрацию.");
            Console.ReadKey();
        }
    }
}
