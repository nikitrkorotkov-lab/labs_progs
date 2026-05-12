using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogParserDLL
{
    // Класс для парсинга вхождений "form" в лог-журнале (Вариант 13)
    public class FormParser
    {
        private static readonly Regex FormRegex = new Regex(@"\bForm\d*\b|\bform\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex DateRegex = new Regex(
            @"(\d{1,2})\s+(\w+)\s+(\d{4})\s+г\.\s+(\d{1,2}):(\d{2}):(\d{2})",
            RegexOptions.Compiled);

        private static readonly Dictionary<string, int> MonthMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            {"января",1},{"февраля",2},{"марта",3},{"апреля",4},{"мая",5},{"июня",6},
            {"июля",7},{"августа",8},{"сентября",9},{"октября",10},{"ноября",11},{"декабря",12}
        };

        /// <summary>
        /// Возвращает список позиций (index, length) вхождений "form*" в лог-блоках,
        /// дата которых попадает в указанный диапазон.
        /// </summary>
        public List<(int index, int length)> Parse(string logText, DateTime dateFrom, DateTime dateTo)
        {
            var result = new List<(int, int)>();
            // Разбиваем лог на блоки по строкам с датой
            var blocks = SplitByDateBlocks(logText);

            foreach (var block in blocks)
            {
                if (block.Date == null) continue;
                DateTime dt = block.Date.Value;
                if (dt >= dateFrom && dt <= dateTo)
                {
                    string blockText = logText.Substring(block.Start, block.Length);
                    foreach (Match m in FormRegex.Matches(blockText))
                    {
                        result.Add((block.Start + m.Index, m.Length));
                    }
                }
            }
            return result;
        }

        private List<LogBlock> SplitByDateBlocks(string text)
        {
            var blocks = new List<LogBlock>();
            var matches = new List<(int idx, DateTime dt)>();

            foreach (Match m in DateRegex.Matches(text))
            {
                int day = int.Parse(m.Groups[1].Value);
                string monthStr = m.Groups[2].Value;
                int year = int.Parse(m.Groups[3].Value);
                if (!MonthMap.TryGetValue(monthStr, out int month)) continue;
                DateTime dt = new DateTime(year, month, day);
                matches.Add((m.Index, dt));
            }

            for (int i = 0; i < matches.Count; i++)
            {
                int start = matches[i].idx;
                int end = (i + 1 < matches.Count) ? matches[i + 1].idx : text.Length;
                blocks.Add(new LogBlock { Start = start, Length = end - start, Date = matches[i].dt });
            }

            // Если до первой даты есть текст
            if (matches.Count > 0 && matches[0].idx > 0)
                blocks.Insert(0, new LogBlock { Start = 0, Length = matches[0].idx, Date = null });

            return blocks;
        }

        private class LogBlock
        {
            public int Start;
            public int Length;
            public DateTime? Date;
        }
    }
}
