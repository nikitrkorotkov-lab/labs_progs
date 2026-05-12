using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace labs_prog.Services.Lab3
{
    public class Lab3Service
    {
        // ══════════════════════════════════════════════════════════════════
        //  Рисование фигур
        // ══════════════════════════════════════════════════════════════════

        public Pen CreatePen(Color color, int width, DashStyle style)
        {
            return new Pen(color, width) { DashStyle = style };
        }

        public void DrawLine(Graphics g, Pen pen, Point start, Point end)
        {
            g.DrawLine(pen, start, end);
        }

        public void DrawRectangle(Graphics g, Pen pen, Color fillColor, Point a, Point b, bool fill)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int w = Math.Abs(b.X - a.X);
            int h = Math.Abs(b.Y - a.Y);

            if (fill)
                g.FillRectangle(new SolidBrush(fillColor), x, y, w, h);
            g.DrawRectangle(pen, x, y, w, h);
        }

        public void DrawEllipse(Graphics g, Pen pen, Color fillColor, Point a, Point b, bool fill)
        {
            int x = Math.Min(a.X, b.X);
            int y = Math.Min(a.Y, b.Y);
            int w = Math.Abs(b.X - a.X);
            int h = Math.Abs(b.Y - a.Y);

            if (fill)
                g.FillEllipse(new SolidBrush(fillColor), x, y, w, h);
            g.DrawEllipse(pen, x, y, w, h);
        }

        public void DrawPencilPoint(Graphics g, Pen pen, Point pt, int width)
        {
            g.FillEllipse(new SolidBrush(pen.Color), pt.X - width / 2f, pt.Y - width / 2f, width, width);
        }

        // ══════════════════════════════════════════════════════════════════
        //  Заливка (flood fill)
        // ══════════════════════════════════════════════════════════════════

        public void FloodFill(Bitmap bmp, Point pt, Color fillColor)
        {
            if (pt.X < 0 || pt.X >= bmp.Width || pt.Y < 0 || pt.Y >= bmp.Height)
                return;

            Color target = bmp.GetPixel(pt.X, pt.Y);
            if (target.ToArgb() == fillColor.ToArgb())
                return;

            var stack = new Stack<Point>();
            stack.Push(pt);
            int w = bmp.Width, h = bmp.Height;
            int targetArgb = target.ToArgb();

            while (stack.Count > 0)
            {
                var p = stack.Pop();
                if (p.X < 0 || p.X >= w || p.Y < 0 || p.Y >= h)
                    continue;
                if (bmp.GetPixel(p.X, p.Y).ToArgb() != targetArgb)
                    continue;

                bmp.SetPixel(p.X, p.Y, fillColor);
                stack.Push(new Point(p.X + 1, p.Y));
                stack.Push(new Point(p.X - 1, p.Y));
                stack.Push(new Point(p.X, p.Y + 1));
                stack.Push(new Point(p.X, p.Y - 1));
            }
        }

        // ══════════════════════════════════════════════════════════════════
        //  Сигналы Фурье (для 2-й части)
        // ══════════════════════════════════════════════════════════════════

        public enum SignalType { Square, Triangle, Sawtooth }

        public int GetHarmonicIndex(SignalType type, int n)
        {
            return type switch
            {
                SignalType.Triangle => 2 * n + 1,
                SignalType.Sawtooth => n + 1,
                _ => 2 * n + 1
            };
        }

        public double HarmonicValue(SignalType type, int k, double x)
        {
            return type switch
            {
                SignalType.Square => (4.0 / Math.PI) * Math.Sin(k * x) / k,
                SignalType.Triangle => (8.0 / (Math.PI * Math.PI)) * Math.Pow(-1, (k - 1) / 2.0) * Math.Sin(k * x) / (k * k),
                SignalType.Sawtooth => (2.0 / Math.PI) * Math.Pow(-1, k + 1) * Math.Sin(k * x) / k,
                _ => 0
            };
        }

        public double FourierSum(SignalType type, int N, double x)
        {
            double sum = 0;
            for (int n = 0; n < N; n++)
                sum += HarmonicValue(type, GetHarmonicIndex(type, n), x);
            return sum;
        }

        public double FourierSumRecursive(SignalType type, int n, int maxN, double x)
        {
            if (n >= maxN) return 0;
            int k = GetHarmonicIndex(type, n);
            return HarmonicValue(type, k, x) + FourierSumRecursive(type, n + 1, maxN, x);
        }

        public Color HsvToColor(int h, float s, float v, int alpha)
        {
            float hh = h / 60f;
            int i = (int)hh;
            float ff = hh - i;
            float p = v * (1 - s), q = v * (1 - s * ff), t = v * (1 - s * (1 - ff));
            float r, gr, b;
            switch (i % 6)
            {
                case 0: r = v; gr = t; b = p; break;
                case 1: r = q; gr = v; b = p; break;
                case 2: r = p; gr = v; b = t; break;
                case 3: r = p; gr = q; b = v; break;
                case 4: r = t; gr = p; b = v; break;
                default: r = v; gr = p; b = q; break;
            }
            return Color.FromArgb(alpha, (int)(r * 255), (int)(gr * 255), (int)(b * 255));
        }
    }
}
