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

        private const int RangeMin = -40;
        private const int RangeMax = 30;

        private List<int> _generatedInput;
        private List<int> _lastOutput;

        public Lab2Control()
        {
            InitializeComponent();
            ConfigureGrids();
        }

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

        private void ShowInGrid(DataGridView dgv, List<int> arr, Label infoLabel, string arrayName)
        {
            int limit = (int)nudDisplayLimit.Value;
            int shown = Math.Min(arr.Count, limit);

            dgv.Columns.Clear();
            dgv.Rows.Clear();

            for (int i = 0; i < shown; i++)
            {
                dgv.Columns.Add($"c{i}", $"[{i}]");
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (shown > 0)
            {
                dgv.Rows.Add();
                for (int i = 0; i < shown; i++)
                    dgv.Rows[0].Cells[i].Value = arr[i];
            }

            if (infoLabel != null)
            {
                infoLabel.Text = arr.Count > limit
                    ? $"показано первых {shown} из {arr.Count} элементов"
                    : $"показаны все {arr.Count} элементов";
            }
        }

        private void ShowInGridFull(DataGridView dgv, List<int> arr)
        {
            dgv.Columns.Clear();
            dgv.Rows.Clear();

            for (int i = 0; i < arr.Count; i++)
            {
                dgv.Columns.Add($"c{i}", $"[{i}]");
                dgv.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            if (arr.Count > 0)
            {
                dgv.Rows.Add();
                for (int i = 0; i < arr.Count; i++)
                    dgv.Rows[0].Cells[i].Value = arr[i];
            }

            lblInputInfo.Text = $"ручной ввод: {arr.Count} элементов (полностью редактируемо)";
        }

        private List<int> ReadFromGrid()
        {
            if (_generatedInput != null)
                return new List<int>(_generatedInput);

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

        private void LogException(Exception ex)
        {
            string msg = $"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] " +
                         $"{ex.GetType().Name}: {ex.Message}\r\n" +
                         $"Стек: {ex.StackTrace}\r\n" +
                         $"{new string('-', 50)}\r\n";

            txtExceptions.AppendText(msg);

            try
            {
                File.AppendAllText("exceptions_lab2.log", msg, System.Text.Encoding.UTF8);
            }
            catch { }

            ExceptionLogger.LogException(ex);
        }

        private void nudDisplayLimit_ValueChanged(object sender, EventArgs e)
        {
            if (_generatedInput != null)
                ShowInGrid(dgvInput, _generatedInput, lblInputInfo, "входной");
            if (_lastOutput != null)
                ShowInGrid(dgvOutput, _lastOutput, lblOutputInfo, "результирующий");
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                AppStatus.Report("Лаб.2: формирование массива", -1);
                int arraySize = (int)nudArraySize.Value;
                var arr = new List<int>();

                if (rbRandom.Checked)
                {
                    for (int i = 0; i < arraySize; i++)
                        arr.Add(_rnd.Next(RangeMin, RangeMax + 1));

                    dgvInput.ReadOnly = true;
                    _generatedInput = arr;
                    ShowInGrid(dgvInput, arr, lblInputInfo, "входной");
                }
                else if (rbFrequency.Checked)
                {
                    if (!int.TryParse(txtFreqValue.Text, out int freqVal))
                        throw new FormatException("Укажите корректное 'Число' для частотного заполнения.");
                    if (!int.TryParse(txtFreqCount.Text, out int freqCount)
                        || freqCount < 1 || freqCount > arraySize)
                        throw new ArgumentOutOfRangeException(
                            $"'Количество раз' должно быть от 1 до {arraySize}.");

                    for (int i = 0; i < freqCount; i++)
                        arr.Add(freqVal);

                    for (int i = freqCount; i < arraySize; i++)
                        arr.Add(_rnd.Next(RangeMin, RangeMax + 1));

                    arr = arr.OrderBy(_ => _rnd.Next()).ToList();
                    dgvInput.ReadOnly = true;
                    _generatedInput = arr;
                    ShowInGrid(dgvInput, arr, lblInputInfo, "входной");
                }
                else
                {
                    for (int i = 0; i < arraySize; i++)
                        arr.Add(0);

                    dgvInput.ReadOnly = false;
                    _generatedInput = null;
                    ShowInGridFull(dgvInput, arr);
                }

                dgvOutput.Columns.Clear();
                dgvOutput.Rows.Clear();
                lblOutputInfo.Text = "";
                _lastOutput = null;
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
            finally
            {
                AppStatus.Idle();
            }
        }

        private void btnNoThread_Click(object sender, EventArgs e)
        {
            try
            {
                var input = ReadFromGrid();
                if (!int.TryParse(txtK.Text, out int k))
                    throw new FormatException("Введите целое число k.");

                AppStatus.Report("Лаб.2: обработка массива (без потоков)", -1);
                var sw = Stopwatch.StartNew();
                var result = _service.ProcessWithoutThreads(input, k);
                sw.Stop();

                _lastOutput = result;
                ShowInGrid(dgvOutput, result, lblOutputInfo, "результирующий");
                lblTimeNoThread.Text = $"Без потоков: {sw.Elapsed.TotalMilliseconds:F4} мс";

                ShowSteps(input, k);
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                AppStatus.Idle();
            }
        }

        private void btnThreaded_Click(object sender, EventArgs e)
        {
            try
            {
                var input = ReadFromGrid();
                if (!int.TryParse(txtK.Text, out int k))
                    throw new FormatException("Введите целое число k.");

                AppStatus.Report("Лаб.2: обработка массива (с потоками)", -1);
                var sw = Stopwatch.StartNew();
                var result = _service.ProcessWithThreads(input, k);
                sw.Stop();

                _lastOutput = result;
                ShowInGrid(dgvOutput, result, lblOutputInfo, "результирующий");
                lblTimeThread.Text = $"С потоками:  {sw.Elapsed.TotalMilliseconds:F4} мс";

                ShowSteps(input, k);
            }
            catch (Exception ex)
            {
                LogException(ex);
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                AppStatus.Idle();
            }
        }

        private void ShowSteps(List<int> input, int k)
        {
            txtSteps.Clear();

            string Preview(List<int> a)
            {
                const int maxShow = 30;
                if (a.Count <= maxShow) return $"[{string.Join(", ", a)}]";
                return $"[{string.Join(", ", a.Take(maxShow))}, ... ещё {a.Count - maxShow}]";
            }

            txtSteps.AppendText($"Исходный ({input.Count} эл.):\r\n{Preview(input)}\r\n\r\n");

            var s1 = _service.DeleteSameDigits(input);
            txtSteps.AppendText($"Шаг 1 (удалить одинаковые), {s1.Count} эл.:\r\n{Preview(s1)}\r\n\r\n");

            var s2 = _service.InsertBeforeDigit1(s1, k);
            txtSteps.AppendText($"Шаг 2 (вставить k={k} перед '1'), {s2.Count} эл.:\r\n{Preview(s2)}\r\n\r\n");

            if (s2.Count >= 6)
            {
                var s3 = _service.SwapFirstAndLast3(s2);
                txtSteps.AppendText($"Шаг 3 (переставить 1-3 и последние 3), {s3.Count} эл.:\r\n{Preview(s3)}\r\n");
            }
            else
            {
                txtSteps.AppendText($"Шаг 3: пропущен (элементов {s2.Count} < 6)\r\n");
            }
        }

        private void rbFrequency_CheckedChanged(object sender, EventArgs e)
        {
            panelFreq.Visible = rbFrequency.Checked;
        }

        private void rbRandom_CheckedChanged(object sender, EventArgs e)
        {
            panelFreq.Visible = rbFrequency.Checked;
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            txtExceptions.Clear();
        }
    }
}
