using System;
using System.Collections.Generic;
using System.Text;

namespace labs_prog.Services.Lab1
{
    public class Lab1Service
    {
        // Задание 1: Поразрядное сложение строк (фамилия + имя)
        public string BitwiseAddition(string surname, string name)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("=== Поразрядное сложение строк ===\n");
            result.AppendLine($"Фамилия: {surname}");
            result.AppendLine($"Имя: {name}\n");

            // Выравниваем строки до одинаковой длины пробелами
            int maxLen = Math.Max(surname.Length, name.Length);
            string str1 = surname.PadRight(maxLen);
            string str2 = name.PadRight(maxLen);

            result.AppendLine("Промежуточные результаты:");
            
            // Складываем коды символов на каждой позиции
            for (int i = 0; i < maxLen; i++)
            {
                int code1 = str1[i];
                int code2 = str2[i];
                int sum = code1 + code2;
                result.AppendLine($"Позиция {i + 1}: '{str1[i]}' ({code1}) + '{str2[i]}' ({code2}) = {sum}");
            }

            return result.ToString();
        }

        // Задание 2: Найти все нечетные делители числа и их сумму
        public string FindOddDivisorsSum(int number)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("\n=== Сумма нечетных делителей ===\n");
            result.AppendLine($"Число: {number}");

            if (number <= 0)
            {
                result.AppendLine("Ошибка: введите натуральное число!");
                return result.ToString();
            }

            List<int> oddDivisors = new List<int>();
            int sum = 0;

            // Проверяем все числа от 1 до number
            for (int i = 1; i <= number; i++)
            {
                // Если делится без остатка И нечетное
                if (number % i == 0 && i % 2 != 0)
                {
                    oddDivisors.Add(i);
                    sum += i;
                }
            }

            result.AppendLine($"Нечетные делители: {string.Join(", ", oddDivisors)}");
            result.AppendLine($"Сумма: {sum}");

            return result.ToString();
        }

        // Задание 3: Найти все прямоугольники с площадью S (стороны от 1 до 20)
        public string FindEqualAreaRectangles(int area)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("\n=== Равновеликие прямоугольники ===\n");
            result.AppendLine($"Площадь S = {area}");

            if (area <= 0)
            {
                result.AppendLine("Ошибка: площадь должна быть положительной!");
                return result.ToString();
            }

            List<string> rectangles = new List<string>();

            // Перебираем все комбинации сторон A и B
            for (int a = 1; a <= 20; a++)
            {
                for (int b = a; b <= 20; b++) // b >= a, чтобы избежать дублей
                {
                    if (a * b == area)
                    {
                        rectangles.Add($"A = {a}, B = {b}");
                    }
                }
            }

            if (rectangles.Count > 0)
            {
                result.AppendLine("Найденные прямоугольники:");
                foreach (var rect in rectangles)
                {
                    result.AppendLine(rect);
                }
            }
            else
            {
                result.AppendLine("Прямоугольники не найдены в заданном диапазоне (1-20)");
            }

            return result.ToString();
        }
    }
}
