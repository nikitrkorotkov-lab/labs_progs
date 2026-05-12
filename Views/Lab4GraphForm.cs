using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LogParserDLL;

namespace labs_prog.Views
{
    public class Lab4GraphForm : Form
    {
        private Chart _chart;
        private List<string> _paths;
        private DateTime _from, _to;
        private FormParser _parser;

        public Lab4GraphForm(List<string> paths, DateTime from, DateTime to, FormParser parser)
        {
            _paths = paths;
            _from = from;
            _to = to;
            _parser = parser;

            this.Text = "Сравнение времени парсинга";
            this.Size = new Size(800, 500);

            _chart = new Chart { Dock = DockStyle.Fill };
            _chart.ChartAreas.Add(new ChartArea("main"));
            _chart.Legends.Add(new Legend());

            var serSync = new Series("Синхронно") { ChartType = SeriesChartType.Line, BorderWidth = 2, Color = Color.Blue };
            var serAsync = new Series("Асинхронно") { ChartType = SeriesChartType.Line, BorderWidth = 2, Color = Color.Red };
            _chart.Series.Add(serSync);
            _chart.Series.Add(serAsync);

            this.Controls.Add(_chart);
            this.Load += async (s, e) => await BuildChart();
        }

        private async Task BuildChart()
        {
            int[] multipliers = { 1, 2, 3, 5, 8 };

            foreach (int mult in multipliers)
            {
                int totalChars = 0;
                var contents = new List<string>();

                // Подготовка данных
                foreach (string path in _paths)
                {
                    string baseContent = File.ReadAllText(path);
                    string content = string.Concat(System.Linq.Enumerable.Repeat(baseContent, mult));
                    contents.Add(content);
                    totalChars += content.Length;
                }

                // Синхронный парсинг (последовательно)
                var swSync = Stopwatch.StartNew();
                foreach (string content in contents)
                {
                    _parser.Parse(content, _from, _to);
                }
                swSync.Stop();
                long syncTime = swSync.ElapsedMilliseconds;

                // Асинхронный парсинг (параллельно через Task.WhenAll)
                var swAsync = Stopwatch.StartNew();
                var tasks = contents.Select(content => Task.Run(() => _parser.Parse(content, _from, _to))).ToArray();
                await Task.WhenAll(tasks);
                swAsync.Stop();
                long asyncTime = swAsync.ElapsedMilliseconds;

                _chart.Series["Синхронно"].Points.AddXY(totalChars, syncTime);
                _chart.Series["Асинхронно"].Points.AddXY(totalChars, asyncTime);
            }

            _chart.ChartAreas["main"].AxisX.Title = "Размер журнала (символов)";
            _chart.ChartAreas["main"].AxisY.Title = "Время (мс)";
        }
    }
}
