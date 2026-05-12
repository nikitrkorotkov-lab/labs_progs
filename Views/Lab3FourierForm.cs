using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using labs_prog.Services.Lab3;

namespace labs_prog.Views
{
    /// <summary>
    /// Окно «живых» сигналов Фурье
    /// </summary>
    public class Lab3FourierForm : Form
    {
        private readonly Lab3Service _service;
        private int _harmonics = 5;
        private Lab3Service.SignalType _signalType = Lab3Service.SignalType.Square;
        private Panel _pnl;
        private Timer _timer;
        private double _phase = 0.0;

        public Lab3FourierForm()
        {
            _service = new Lab3Service();

            this.Text = "Сигналы Фурье — Lab 3";
            this.Size = new Size(900, 500);
            this.MinimumSize = new Size(600, 380);
            this.BackColor = Color.FromArgb(20, 20, 20);

            BuildUI();

            _timer = new Timer { Interval = 30 };
            _timer.Tick += (s, e) => { _phase += 0.05; _pnl.Invalidate(); };
            _timer.Start();

            this.FormClosed += (s, e) => _timer.Stop();
            this.Resize += (s, e) => _pnl.Invalidate();
        }

        private void BuildUI()
        {
            var top = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(40, 40, 40) };

            var lblType = new Label
            {
                Text = "Тип:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(10, 13),
                Font = new Font("Segoe UI", 10)
            };

            var cbType = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(50, 10),
                Width = 150,
                Font = new Font("Segoe UI", 10)
            };
            cbType.Items.AddRange(new object[] { "Прямоугольная", "Треугольная", "Пилообразная" });
            cbType.SelectedIndex = 0;
            cbType.SelectedIndexChanged += (s, e) =>
            {
                _signalType = cbType.SelectedIndex switch
                {
                    1 => Lab3Service.SignalType.Triangle,
                    2 => Lab3Service.SignalType.Sawtooth,
                    _ => Lab3Service.SignalType.Square
                };
                _pnl.Invalidate();
            };

            var lblN = new Label
            {
                Text = "Гармоник:",
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(215, 13),
                Font = new Font("Segoe UI", 10)
            };

            var nud = new NumericUpDown
            {
                Minimum = 1,
                Maximum = 50,
                Value = _harmonics,
                Location = new Point(300, 10),
                Width = 65,
                Font = new Font("Segoe UI", 10),
                BackColor = Color.FromArgb(60, 60, 60),
                ForeColor = Color.White
            };
            nud.ValueChanged += (s, e) => { _harmonics = (int)nud.Value; _pnl.Invalidate(); };

            var btnPause = new Button
            {
                Text = "⏸ Пауза",
                Location = new Point(385, 8),
                Width = 90,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(70, 70, 70),
                Font = new Font("Segoe UI", 9)
            };
            btnPause.Click += (s, e) =>
            {
                if (_timer.Enabled)
                {
                    _timer.Stop();
                    btnPause.Text = "▶ Пуск";
                }
                else
                {
                    _timer.Start();
                    btnPause.Text = "⏸ Пауза";
                }
            };

            top.Controls.AddRange(new Control[] { lblType, cbType, lblN, nud, btnPause });

            _pnl = new DoubleBufferedPanel { Dock = DockStyle.Fill, BackColor = Color.FromArgb(15, 15, 15) };
            _pnl.Paint += Pnl_Paint;

            this.Controls.Add(_pnl);
            this.Controls.Add(top);
        }

        private void Pnl_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int W = _pnl.Width, H = _pnl.Height;
            int mL = 65, mR = 20, mT = 25, mB = 45;
            int pW = W - mL - mR, pH = H - mT - mB;
            if (pW < 10 || pH < 10) return;

            double xMin = 0, xMax = 4 * Math.PI;
            double yMin = -1.5, yMax = 1.5;
            int steps = pW;
            double dx = (xMax - xMin) / steps;

            Func<double, float> sx = xv => (float)(mL + (xv - xMin) / (xMax - xMin) * pW);
            Func<double, float> sy = yv => (float)(mT + (1.0 - (yv - yMin) / (yMax - yMin)) * pH);

            // Фон
            g.FillRectangle(new SolidBrush(Color.FromArgb(22, 22, 22)), mL, mT, pW, pH);

            // Сетка
            using (var grid = new Pen(Color.FromArgb(45, 45, 45), 1) { DashStyle = DashStyle.Dot })
            {
                double[] yg = { -1, -.5, 0, .5, 1 };
                foreach (var yv in yg)
                    g.DrawLine(grid, mL, sy(yv), mL + pW, sy(yv));
                for (int i = 0; i <= 4; i++)
                {
                    float x0 = sx(i * Math.PI);
                    g.DrawLine(grid, x0, mT, x0, mT + pH);
                }
            }

            // Оси
            using (var axPen = new Pen(Color.Gray, 1.5f))
            {
                float axY = sy(0), axX = sx(0);
                g.DrawLine(axPen, mL, axY, mL + pW, axY);
                g.DrawLine(axPen, axX, mT, axX, mT + pH);

                // Засечки X
                using (var fnt = new Font("Segoe UI", 8))
                using (var br = new SolidBrush(Color.LightGray))
                {
                    string[] xlbl = { "0", "π", "2π", "3π", "4π" };
                    for (int i = 0; i <= 4; i++)
                    {
                        float x0 = sx(i * Math.PI);
                        g.DrawLine(axPen, x0, axY - 4, x0, axY + 4);
                        var sz = g.MeasureString(xlbl[i], fnt);
                        g.DrawString(xlbl[i], fnt, br, x0 - sz.Width / 2, axY + 5);
                    }

                    // Засечки Y
                    double[] yg = { -1, -.5, 0, .5, 1 };
                    foreach (var yv in yg)
                    {
                        if (Math.Abs(yv) < 1e-9) continue;
                        float y0 = sy(yv);
                        g.DrawLine(axPen, axX - 4, y0, axX + 4, y0);
                        var s2 = g.MeasureString(yv.ToString("0.0"), fnt);
                        g.DrawString(yv.ToString("0.0"), fnt, br, axX - s2.Width - 6, y0 - s2.Height / 2);
                    }
                }

                // Подписи осей
                using (var fntI = new Font("Segoe UI", 9, FontStyle.Italic))
                using (var br = new SolidBrush(Color.LightGray))
                {
                    g.DrawString("f(x)", fntI, br, axX + 4, mT + 2);
                    g.DrawString("x", fntI, br, mL + pW - 12, axY - 18);
                }
            }

            // Название сигнала
            string title = _signalType switch
            {
                Lab3Service.SignalType.Triangle => "Треугольная огибающая",
                Lab3Service.SignalType.Sawtooth => "Пилообразная огибающая",
                _ => "Прямоугольная огибающая"
            };
            using (var fntT = new Font("Segoe UI", 11, FontStyle.Bold))
            using (var br = new SolidBrush(Color.Silver))
            {
                g.DrawString($"{title}  (N={_harmonics})", fntT, br, mL + 5, mT + 3);
            }

            int maxK = _harmonics;

            // Отдельные гармоники
            for (int n = 0; n < maxK; n++)
            {
                int k = _service.GetHarmonicIndex(_signalType, n);
                float alpha = Math.Max(30, 160 - n * 15);
                var hc = _service.HsvToColor(n * 40 % 360, 0.7f, 0.85f, (int)alpha);
                using (var hp = new Pen(hc, 1f))
                {
                    PointF? prev = null;
                    for (int i = 0; i <= steps; i++)
                    {
                        double xv = xMin + i * dx;
                        double yv = _service.HarmonicValue(_signalType, k, xv + _phase);
                        yv = Math.Max(yMin, Math.Min(yMax, yv));
                        var pt = new PointF(sx(xv), sy(yv));
                        if (prev.HasValue)
                            g.DrawLine(hp, prev.Value, pt);
                        prev = pt;
                    }
                }
            }

            // Суммарный сигнал (рекурсивный вызов)
            using (var sumPen = new Pen(Color.FromArgb(255, 220, 60), 2.5f))
            {
                PointF? prevSum = null;
                for (int i = 0; i <= steps; i++)
                {
                    double xv = xMin + i * dx;
                    double yv = _service.FourierSumRecursive(_signalType, 0, maxK, xv + _phase);
                    yv = Math.Max(yMin, Math.Min(yMax, yv));
                    var pt = new PointF(sx(xv), sy(yv));
                    if (prevSum.HasValue)
                        g.DrawLine(sumPen, prevSum.Value, pt);
                    prevSum = pt;
                }
            }

            // Легенда
            using (var fnt = new Font("Segoe UI", 8))
            using (var br = new SolidBrush(Color.LightGray))
            using (var sp = new Pen(Color.FromArgb(255, 220, 60), 2f))
            {
                g.DrawLine(sp, mL + 10, mT + 33, mL + 38, mT + 33);
                g.DrawString($"Сумма {maxK} гармоник", fnt, br, mL + 42, mT + 25);
            }
        }
    }
}
