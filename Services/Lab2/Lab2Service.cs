using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace labs_prog.Services.Lab2
{
    public class Lab2Service
    {
        // Проверка: все цифры числа одинаковы (11, 222, -33, 5 → true)
        public bool HasAllSameDigits(int n)
        {
            string digits = Math.Abs(n).ToString();
            return digits.All(c => c == digits[0]);
        }

        // Проверка: число содержит цифру 1 (работает с отрицательными)
        public bool ContainsDigit1(int n)
        {
            return Math.Abs(n).ToString().Contains('1');
        }

        // Шаг 1: удалить элементы с одинаковыми цифрами
        public List<int> DeleteSameDigits(List<int> arr)
        {
            return arr.Where(x => !HasAllSameDigits(x)).ToList();
        }

        // Шаг 2: вставить k перед каждым элементом, содержащим цифру 1
        public List<int> InsertBeforeDigit1(List<int> arr, int k)
        {
            var result = new List<int>();
            foreach (var x in arr)
            {
                if (ContainsDigit1(x))
                    result.Add(k); // вставляем k перед элементом
                result.Add(x);
            }
            return result;
        }

        // Шаг 3: переставить первые 3 и последние 3 элемента местами
        // [a,b,c, ..., x,y,z] → [x,y,z, ..., a,b,c]
        public List<int> SwapFirstAndLast3(List<int> arr)
        {
            if (arr.Count < 6)
                throw new InvalidOperationException(
                    $"После обработки осталось {arr.Count} эл. — нужно минимум 6 для перестановки.");

            var result = new List<int>(arr);
            int n = result.Count;

            // Сохраняем первые 3 элемента
            int a0 = result[0], a1 = result[1], a2 = result[2];

            // Последние 3 → на место первых 3
            result[0] = result[n - 3];
            result[1] = result[n - 2];
            result[2] = result[n - 1];

            // Первые 3 → на место последних 3
            result[n - 3] = a0;
            result[n - 2] = a1;
            result[n - 1] = a2;

            return result;
        }

        // Полная обработка БЕЗ потоков (последовательно)
        public List<int> ProcessWithoutThreads(List<int> input, int k)
        {
            var step1 = DeleteSameDigits(input);
            var step2 = InsertBeforeDigit1(step1, k);
            return SwapFirstAndLast3(step2);
        }

        // Полная обработка С ПОТОКАМИ
        // Шаг 1: массив делится на 2 части → каждая фильтруется в отдельном потоке
        // Шаги 2 и 3: выполняются в отдельных потоках последовательно
        public List<int> ProcessWithThreads(List<int> input, int k)
        {
            // Делим массив пополам
            int half = input.Count / 2;
            var part1 = input.Take(half).ToList();
            var part2 = input.Skip(half).ToList();

            List<int> filtered1 = null;
            List<int> filtered2 = null;

            // Два потока фильтруют каждую половину параллельно
            var t1 = new Thread(() => { filtered1 = DeleteSameDigits(part1); });
            var t2 = new Thread(() => { filtered2 = DeleteSameDigits(part2); });
            t1.Start(); t2.Start();
            t1.Join(); t2.Join();

            // Объединяем результаты
            var step1Result = filtered1.Concat(filtered2).ToList();

            // Третий поток выполняет вставку k
            List<int> step2Result = null;
            var t3 = new Thread(() => { step2Result = InsertBeforeDigit1(step1Result, k); });
            t3.Start(); t3.Join();

            // Четвёртый поток выполняет перестановку
            List<int> step3Result = null;
            var t4 = new Thread(() => { step3Result = SwapFirstAndLast3(step2Result); });
            t4.Start(); t4.Join();

            return step3Result;
        }
    }
}
