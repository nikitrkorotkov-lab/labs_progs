using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogParserDLL;

namespace labs_prog.Views
{
    public partial class Lab6Control : UserControl
    {
        private FormParser _parser = new FormParser();
        private const string AuthorLabel = "Коротков Н.Д.";
        private bool _isLoadingFile = false;

        public Lab6Control()
        {
            InitializeComponent();
        }

        // Пункт 1: Асинхронное выделение сигнатур при выборе в ComboBox
        private async void comboSignature_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HighlightSignatureAsync(comboSignature.SelectedItem?.ToString());
        }

        private async Task HighlightSignatureAsync(string signature)
        {
            if (string.IsNullOrEmpty(signature)) return;

            var sw = Stopwatch.StartNew();
            var rtb = richBoxMain;
            string text = rtb.Text;

            // Асинхронный поиск всех вхождений
            var positions = await Task.Run(() =>
            {
                var result = new List<(int index, int length)>();
                int idx = 0;
                while ((idx = text.IndexOf(signature, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    result.Add((idx, signature.Length));
                    idx += signature.Length;
                }
                return result;
            });

            // Сброс цвета
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
            rtb.SelectionStart = 0;
            rtb.SelectionLength = 0;

            // Выделение найденных позиций
            foreach (var (index, length) in positions)
            {
                rtb.Select(index, length);
                rtb.SelectionColor = Color.Blue;
            }
            rtb.SelectionStart = 0;
            rtb.SelectionLength = 0;

            sw.Stop();
            richBoxStats.AppendText($"[ASYNC] Выделение '{signature}': {positions.Count} вхождений, время: {sw.ElapsedMilliseconds} мс\r\n");
        }

        // Пункт 2: Асинхронный поиск при ручном вводе текста
        private async void richBoxMain_TextChanged(object sender, EventArgs e)
        {
            if (_isLoadingFile) return;

            var sw = Stopwatch.StartNew();
            await Task.Run(async () =>
            {
                await Task.Delay(500); // Debounce для избежания частых вызовов
            });

            foreach (string sig in comboSignature.Items)
            {
                await HighlightAllOccurrencesAsync(richBoxMain, sig, Color.DarkGreen);
            }
            sw.Stop();
            richBoxStats.AppendText($"[ASYNC] Автопоиск при вводе: {sw.ElapsedMilliseconds} мс\r\n");
        }

        private async Task HighlightAllOccurrencesAsync(RichTextBox rtb, string word, Color color)
        {
            string text = rtb.Text;
            var positions = await Task.Run(() =>
            {
                var result = new List<(int index, int length)>();
                int idx = 0;
                while ((idx = text.IndexOf(word, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
                {
                    result.Add((idx, word.Length));
                    idx += word.Length;
                }
                return result;
            });

            foreach (var (index, length) in positions)
            {
                rtb.Select(index, length);
                rtb.SelectionColor = color;
            }
        }

        // Пункт 3: Асинхронный анализ форм с измерением времени
        private async void btnAnalyzeForms_Click(object sender, EventArgs e)
        {
            var sw = Stopwatch.StartNew();
            string text = richBoxMain.Text;

            var (dict, positions) = await Task.Run(() =>
            {
                var regex = new System.Text.RegularExpressions.Regex(@"Form\d+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                var formDict = new Dictionary<string, int>();
                var posList = new List<(int index, int length)>();

                foreach (System.Text.RegularExpressions.Match m in regex.Matches(text))
                {
                    string key = m.Value;
                    if (!formDict.ContainsKey(key)) formDict[key] = 0;
                    formDict[key]++;
                    posList.Add((m.Index, m.Length));
                }
                return (formDict, posList);
            });

            // Подсветка
            foreach (var (index, length) in positions)
            {
                richBoxMain.Select(index, length);
                richBoxMain.SelectionColor = Color.OrangeRed;
            }
            richBoxMain.SelectionStart = 0;

            sw.Stop();

            // Статистика
            richBoxStats.Clear();
            richBoxStats.AppendText($"[ASYNC] Анализ форм: {sw.ElapsedMilliseconds} мс\r\n\r\n");
            richBoxStats.AppendText("Статистика по формам:\r\n");
            foreach (var kv in dict)
                richBoxStats.AppendText($"  {kv.Key}: {kv.Value} раз\r\n");
        }

        // Пункт 4 (Вариант 13): Асинхронный парсинг с датами и добавлением ФИО
        private async void btnParseVariant_Click(object sender, EventArgs e)
        {
            if (!datePickerFrom.Checked || !datePickerTo.Checked)
            {
                MessageBox.Show("Выберите диапазон дат.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dateFrom = datePickerFrom.Value.Date;
            DateTime dateTo = datePickerTo.Value.Date;
            string text = richBoxMain.Text;

            var sw = Stopwatch.StartNew();
            var positions = await Task.Run(() => _parser.Parse(text, dateFrom, dateTo));
            sw.Stop();

            // Сброс цвета
            richBoxMain.SelectAll();
            richBoxMain.SelectionColor = Color.Black;

            // Подсветка
            foreach (var (idx, len) in positions)
            {
                richBoxMain.Select(idx, len);
                richBoxMain.SelectionColor = Color.DarkBlue;
            }

            // Добавление ФИО (в обратном порядке)
            for (int i = positions.Count - 1; i >= 0; i--)
            {
                int insertAt = positions[i].index + positions[i].length;
                richBoxMain.Select(insertAt, 0);
                richBoxMain.SelectionColor = Color.Red;
                richBoxMain.SelectedText = $" [{AuthorLabel}]";
            }

            richBoxMain.SelectionStart = 0;
            richBoxStats.AppendText($"\r\n[ASYNC] Вариант 13: {positions.Count} вхождений, время: {sw.ElapsedMilliseconds} мс\r\n");
        }

        // Пункт 5: Загрузка журналов
        private void btnLoadLog_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Log files|*.log;*.txt|All|*.*", Multiselect = true };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            listBoxLogs.Items.Clear();
            foreach (string f in ofd.FileNames)
                listBoxLogs.Items.Add(f);
        }

        // Пункт 6: Синхронный парсинг журналов
        private async void btnParseSelected_Click(object sender, EventArgs e)
        {
            if (listBoxLogs.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите лог-журналы из списка.");
                return;
            }

            DateTime dateFrom = datePickerFrom.Value.Date;
            DateTime dateTo = datePickerTo.Value.Date;

            richBoxStats.AppendText("\r\n=== Парсинг журналов (синхронно) ===\r\n");
            var swAll = Stopwatch.StartNew();

            foreach (string path in listBoxLogs.SelectedItems)
            {
                string content = await Task.Run(() => File.ReadAllText(path));
                var sw = Stopwatch.StartNew();
                var res = await Task.Run(() => _parser.Parse(content, dateFrom, dateTo));
                sw.Stop();
                richBoxStats.AppendText($"{Path.GetFileName(path)}: {res.Count} вхождений, время: {sw.ElapsedMilliseconds} мс\r\n");
            }

            swAll.Stop();
            richBoxStats.AppendText($"Суммарное время: {swAll.ElapsedMilliseconds} мс\r\n");
        }

        // Пункт 7: Асинхронный парсинг журналов
        private async void btnParseAsync_Click(object sender, EventArgs e)
        {
            if (listBoxLogs.SelectedItems.Count == 0)
            {
                MessageBox.Show("Выберите лог-журналы из списка.");
                return;
            }

            DateTime dateFrom = datePickerFrom.Value.Date;
            DateTime dateTo = datePickerTo.Value.Date;

            richBoxStats.AppendText("\r\n=== Парсинг журналов (асинхронно) ===\r\n");

            var paths = new List<string>();
            foreach (string p in listBoxLogs.SelectedItems) paths.Add(p);

            var swAll = Stopwatch.StartNew();
            var tasks = paths.Select(path => Task.Run(() =>
            {
                string content = File.ReadAllText(path);
                var sw = Stopwatch.StartNew();
                var res = _parser.Parse(content, dateFrom, dateTo);
                sw.Stop();
                return $"{Path.GetFileName(path)}: {res.Count} вхождений, время: {sw.ElapsedMilliseconds} мс";
            })).ToArray();

            string[] results = await Task.WhenAll(tasks);
            swAll.Stop();

            foreach (string r in results)
                richBoxStats.AppendText(r + "\r\n");

            richBoxStats.AppendText($"Суммарное время (асинхронно): {swAll.ElapsedMilliseconds} мс\r\n");

            var graphForm = new Lab4GraphForm(paths, dateFrom, dateTo, _parser);
            graphForm.Show();
        }

        private void btnLoadText_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Text|*.txt;*.log|All|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            _isLoadingFile = true;
            richBoxMain.Text = File.ReadAllText(ofd.FileName);
            _isLoadingFile = false;
        }
    }
}
