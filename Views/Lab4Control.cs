using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogParserDLL;

namespace labs_prog.Views
{
    public partial class Lab4Control : UserControl
    {
        private FormParser _parser = new FormParser();

        // Фамилия и инициалы (Вариант 13)
        private const string AuthorLabel = "Коротков Н.Д.";

        public Lab4Control()
        {
            InitializeComponent();
        }

        // ─── Пункт 1: Выделить включения при выборе сигнатуры в comboBox ────────
        private void comboSignature_SelectedIndexChanged(object sender, EventArgs e)
        {
            HighlightSignature(comboSignature.SelectedItem?.ToString());
        }

        private void HighlightSignature(string signature)
        {
            if (string.IsNullOrEmpty(signature)) return;
            var rtb = richBoxMain;
            string text = rtb.Text;

            // Сбрасываем цвет
            rtb.SelectAll();
            rtb.SelectionColor = Color.Black;
            rtb.SelectionStart = 0;
            rtb.SelectionLength = 0;

            int idx = 0;
            while ((idx = text.IndexOf(signature, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                rtb.Select(idx, signature.Length);
                rtb.SelectionColor = Color.Blue;
                idx += signature.Length;
            }
            rtb.SelectionStart = 0;
            rtb.SelectionLength = 0;
        }

        // ─── Пункт 2: При вводе текста вручную — поиск сигнатур последовательно ──
        private void richBoxMain_TextChanged(object sender, EventArgs e)
        {
            if (_isLoadingFile) return;
            // Перебираем все сигнатуры из comboBox
            foreach (string sig in comboSignature.Items)
            {
                HighlightAllOccurrences(richBoxMain, sig, Color.DarkGreen);
            }
        }

        private void HighlightAllOccurrences(RichTextBox rtb, string word, Color color)
        {
            string text = rtb.Text;
            int idx = 0;
            while ((idx = text.IndexOf(word, idx, StringComparison.OrdinalIgnoreCase)) >= 0)
            {
                rtb.Select(idx, word.Length);
                rtb.SelectionColor = color;
                idx += word.Length;
            }
        }

        // ─── Пункт 3: Определить, в каких формах появлялись исключения ──────────
        private void btnAnalyzeForms_Click(object sender, EventArgs e)
        {
            string text = richBoxMain.Text;
            // Ищем наименования форм (FormN, Form1, Form2 и т.д.)
            var regex = new System.Text.RegularExpressions.Regex(@"Form\d+", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            var dict = new Dictionary<string, int>();
            foreach (System.Text.RegularExpressions.Match m in regex.Matches(text))
            {
                string key = m.Value;
                if (!dict.ContainsKey(key)) dict[key] = 0;
                dict[key]++;
            }

            // Подсветить вхождения
            foreach (System.Text.RegularExpressions.Match m in regex.Matches(text))
            {
                richBoxMain.Select(m.Index, m.Length);
                richBoxMain.SelectionColor = Color.OrangeRed;
            }
            richBoxMain.SelectionStart = 0;

            // Вывести статистику
            richBoxStats.Clear();
            richBoxStats.AppendText("Статистика по формам:\r\n");
            foreach (var kv in dict)
                richBoxStats.AppendText($"  {kv.Key}: {kv.Value} раз\r\n");
        }

        // ─── Пункт 4 (Вариант 13): Парсинг "form*" с датами, добавление ФИО ─────
        private void btnParseVariant_Click(object sender, EventArgs e)
        {
            if (!datePickerFrom.Checked || !datePickerTo.Checked)
            {
                MessageBox.Show("Выберите диапазон дат.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DateTime dateFrom = datePickerFrom.Value.Date;
            DateTime dateTo = datePickerTo.Value.Date;
            string text = richBoxMain.Text;

            var positions = _parser.Parse(text, dateFrom, dateTo);

            // Сначала сбрасываем цвет
            richBoxMain.SelectAll();
            richBoxMain.SelectionColor = Color.Black;

            // Подсвечиваем найденные вхождения
            foreach (var (idx, len) in positions)
            {
                richBoxMain.Select(idx, len);
                richBoxMain.SelectionColor = Color.DarkBlue;
            }

            // Добавляем ФИО после каждого вхождения (в обратном порядке, чтобы не сбить индексы)
            for (int i = positions.Count - 1; i >= 0; i--)
            {
                int insertAt = positions[i].index + positions[i].length;
                richBoxMain.Select(insertAt, 0);
                richBoxMain.SelectionColor = Color.Red;
                richBoxMain.SelectedText = $" [{AuthorLabel}]";
            }

            richBoxMain.SelectionStart = 0;
            richBoxStats.AppendText($"\r\nВариант 13: найдено {positions.Count} вхождений 'form' в диапазоне дат.\r\n");
        }

        // ─── Пункт 5: Парсинг через DLL, измерение времени ──────────────────────
        private void btnLoadLog_Click(object sender, EventArgs e)
        {
            using var ofd = new OpenFileDialog { Filter = "Log files|*.log;*.txt|All|*.*", Multiselect = true };
            if (ofd.ShowDialog() != DialogResult.OK) return;
            listBoxLogs.Items.Clear();
            foreach (string f in ofd.FileNames)
                listBoxLogs.Items.Add(f);
        }

        private void btnParseSelected_Click(object sender, EventArgs e)
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
                string content = File.ReadAllText(path);
                var sw = Stopwatch.StartNew();
                var res = _parser.Parse(content, dateFrom, dateTo);
                sw.Stop();
                richBoxStats.AppendText($"{Path.GetFileName(path)}: {res.Count} вхождений, время: {sw.ElapsedMilliseconds} мс\r\n");
            }

            swAll.Stop();
            richBoxStats.AppendText($"Суммарное время (синхронно): {swAll.ElapsedMilliseconds} мс\r\n");
        }

        // ─── Пункт 6: Асинхронный парсинг ───────────────────────────────────────
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
            var tasks = new List<Task<string>>();

            foreach (string path in paths)
            {
                tasks.Add(Task.Run(() =>
                {
                    string content = File.ReadAllText(path);
                    var sw = Stopwatch.StartNew();
                    var res = _parser.Parse(content, dateFrom, dateTo);
                    sw.Stop();
                    return $"{Path.GetFileName(path)}: {res.Count} вхождений, время: {sw.ElapsedMilliseconds} мс";
                }));
            }

            string[] results = await Task.WhenAll(tasks);
            swAll.Stop();

            foreach (string r in results)
                richBoxStats.AppendText(r + "\r\n");

            richBoxStats.AppendText($"Суммарное время (асинхронно): {swAll.ElapsedMilliseconds} мс\r\n");

            // Открываем форму с графиком
            var graphForm = new Lab4GraphForm(paths, dateFrom, dateTo, _parser);
            graphForm.Show();
        }

        private bool _isLoadingFile = false;

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
