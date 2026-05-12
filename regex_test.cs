using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        var regex = new Regex(@"\bform\w*", RegexOptions.IgnoreCase);
        
        string[] testStrings = {
            "Form1",
            "Form2", 
            "format",
            "FormatException",
            "performance",
            "form",
            "formatted",
            "information"
        };
        
        Console.WriteLine("Тест регулярного выражения \\bform\\w*:");
        foreach (string test in testStrings)
        {
            bool matches = regex.IsMatch(test);
            Console.WriteLine($"{test}: {(matches ? "НАЙДЕНО" : "НЕ НАЙДЕНО")}");
        }
        
        Console.WriteLine("\nОжидаемый результат:");
        Console.WriteLine("Form1: НАЙДЕНО");
        Console.WriteLine("Form2: НАЙДЕНО");
        Console.WriteLine("format: НЕ НАЙДЕНО");
        Console.WriteLine("FormatException: НЕ НАЙДЕНО");
        Console.WriteLine("performance: НЕ НАЙДЕНО");
        Console.WriteLine("form: НАЙДЕНО");
        Console.WriteLine("formatted: НАЙДЕНО");
        Console.WriteLine("information: НЕ НАЙДЕНО");
    }
}