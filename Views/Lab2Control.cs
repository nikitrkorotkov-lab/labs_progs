using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using labs_prog.Services.Lab2;

namespace labs_prog.Views
{
    public partial class Lab2Control : UserControl
    {
        private readonly Lab2Service _service = new Lab2Service();
        private readonly Random _rnd = new Random();

        private const int ArraySize = 10;
        private const int RangeMin = -40;
        private const int RangeMax = 30;

        public Lab2Control()
        {
            InitializeComponent();
            ConfigureGrids();
        }

        // Настройка внешнего вида таблиц
        private void ConfigureGrids()
        {
            foreach (var dgv in new[] { dgvInput, dgvOutput })
            {
                dgv.AllowUserToAddRows = false;
                dgv.RowHeadersVisible = false;
                dgv.ScrollBars = ScrollBars.Horizontal;
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            }
        }

        // Показать массив в таблице (горизонтально: 1 строка, N столбцов)
        private void ShowInGrid(DataGridView dgv, List<int> arr)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            // Создаём столбцы с индексами [0], [1], [2]...
            for (int i = 0; i < arr.Count; i++)
            {
                dgv.Columns.Add($"c{i}", $"[{i}]");
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            // Заполняем единственную строку значениями
            if (arr.Count > 0)
            {
                dgv.Rows.Add();
                for (int i = 0; i < arr.Count; i++)
                    dgv.Rows[0].Cells[i].Value = arr[i];
            }
        }

        // Считать массив из таблицы (для ручного режима)
        private List<int> ReadFromGrid()
        {
            var result = new List<int>();

            if (dgvInput.Columns.Count == 0)
                throw new InvalidOperationException("Сначала создайте массив кнопкой 'Инициализировать'.");

            for (int i = 0; i < dgvInput.Columns.Count; i++)
            {
                var cell = dgvInput.Rows[0].Cells[i];
                if (cell.Value == null || !int.TryParse(cell.Value.ToString(), out int val))
                    throw new FormatException($"Ячейка [{i}]: введите целое число.");
                result.Add(val);
            }
            return result;
        }

        // Логирование исключения в TextBox и в файл
        private void LogException(Exception ex)
        {
            string msg = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] " +
                         $"{ex.GetType().Name}: {ex.Message}\r\n" +
                         $"Стек: {ex.StackTrace}\r\n" +
                         $"{new string('-', 50)}\r\n";

            txtExceptions.AppendText(msg);

            // Запись в файл рядом с .exe
            try
            {
                File.AppendAllText("exceptions_lab2.log", msg, System.Text.Encoding.UTF8);
            }
            catch { /* файл может быть недоступен — не критично */ }
        }

        // Кнопка: Инициализировать массив
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                var arr = new List<int>();

                if (rbRandom.Checked)
                {
                    // Полностью случайный массив
                    for (int i = 0; i < ArraySize; i++)
                        arr.Add(_rnd.Next(RangeMin, RangeMax + 1));

                    dgvInput.ReadOnly = true;
                }
                else if (rbFrequency.Checked)
                {
                    // Заданное число появляется заданное количество раз
                    if (!int.TryParse(txtFreqValue.Text, out int freqVal))
                        throw new FormatException("Укажите корректное 'Число' для частотного заполнения.");
                    if (!int.TryParse(txtFreqCount.Text, out int freqCount)
                        || freqCount < 1 || freqCount > ArraySize)
                        throw new ArgumentOutOfRangeException(
                            $"'Количество раз' должно быть от 1 до {ArraySize}.");

                    // Добавляем частотное число
                    for (int i = 0; i < freqCount; i++)
                        arr.Add(freqVal);
                    
                    // Остальные — случайные
                    for (int i = freqCount; i < ArraySize; i++)
                        arr.Add(_rnd.Next(RangeMin, RangeMax + 1));

                    // Перемешиваем, чтобы частотное число не стояло кучей
                    arr = arr.OrderBy(_ => _rnd.Next()).ToList();
                    dgvInput.ReadOnly = true;
                }
                else // ручной режим
                {
                    // Инициализируем нулями — пользователь меняет сам
                    for (int i = 0; i < ArraySize; i++)
                        arr.Add(0);

                    dgvInput.ReadOnly = false; // разрешить редактирование
                }

                ShowInGrid(dgvInput, arr);
                
                // Очищаем результаты предыдущего запуска
                dgvOutput.Columns.Clear();
                dgvOutput.Rows.Clear();
                txtSteps.Clear();
                lblTimeNoThread.Text = "Без потоков: —";
                lblTimeThread.Text   = "С потоками:  —";
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show(ex.Message, "Ошибка генерации",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Кнопка: Выполнить БЕЗ потоков
        private void btnNoThread_Click(object sender, EventArgs e)
        {
            try
            {
                var input = ReadFromGrid();
                if (!int.TryParse(txtK.Text, out int k))
                    throw new FormatException("Введите целое число k.");

                // Замеряем время выполнения
                var sw = Stopwatch.StartNew();
                var result = _service.ProcessWithoutThreads(input, k);
                sw.Stop();

                ShowInGrid(dgvOutput, result);
                lblTimeNoThread.Text = $"Без потоков: {sw.Elapsed.TotalMilliseconds:F4} мс";

                ShowSteps(input, k); // показать промежуточные шаги
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Кнопка: Выполнить С потоками
        private void btnThreaded_Click(object sender, EventArgs e)
        {
            try
            {
                var input = ReadFromGrid();
                if (!int.TryParse(txtK.Text, out int k))
                    throw new FormatException("Введите целое число k.");

                // Замеряем время выполнения
                var sw = Stopwatch.StartNew();
                var result = _service.ProcessWithThreads(input, k);
                sw.Stop();

                ShowInGrid(dgvOutput, result);
                lblTimeThread.Text = $"С потоками:  {sw.Elapsed.TotalMilliseconds:F4} мс";

                ShowSteps(input, k);
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Показать промежуточные результаты каждого шага
        private void ShowSteps(List<int> input, int k)
        {
            txtSteps.Clear();
            txtSteps.AppendText($"Исходный:          [{string.Join(", ", input)}]\r\n\r\n");

            var s1 = _service.DeleteSameDigits(input);
            txtSteps.AppendText($"Шаг 1 (удалить одинаковые):\r\n[{string.Join(", ", s1)}]\r\n\r\n");

            var s2 = _service.InsertBeforeDigit1(s1, k);
            txtSteps.AppendText($"Шаг 2 (вставить k={k} перед '1'):\r\n[{string.Join(", ", s2)}]\r\n\r\n");

            if (s2.Count >= 6)
            {
                var s3 = _service.SwapFirstAndLast3(s2);
                txtSteps.AppendText($"Шаг 3 (переставить 1-3 и последние 3):\r\n[{string.Join(", ", s3)}]\r\n");
            }
            else
            {
                txtSteps.AppendText($"Шаг 3: пропущен (элементов {s2.Count} < 6)\r\n");
            }
        }

        // Переключение видимости панели частоты
        private void rbFrequency_CheckedChanged(object sender, EventArgs e)
        {
            panelFreq.Visible = rbFrequency.Checked;
        }

        private void rbRandom_CheckedChanged(object sender, EventArgs e)
        {
            panelFreq.Visible = rbFrequency.Checked;
        }

        // Кнопка: Очистить лог исключений
        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtExceptions.Clear();
        }
    }
}
