using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace labs_prog.Services.Lab3
{
    public class Lab3FractalService
    {
        public Color GetGradientColor(int depth, int maxDepth, Color startColor, Color endColor)
        {
            if (maxDepth <= 0) return startColor;
            float t = Math.Max(0f, Math.Min(1f, (float)depth / maxDepth));
            return Color.FromArgb(255,
                Clamp((int)(startColor.R + t * (endColor.R - startColor.R))),
                Clamp((int)(startColor.G + t * (endColor.G - startColor.G))),
                Clamp((int)(startColor.B + t * (endColor.B - startColor.B))));
        }
        
        private int Clamp(int v) => Math.Max(0, Math.Min(255, v));

        public void DrawPythagorasTree(Graphics g, float x1, float y1, float x2, float y2, int depth, int maxDepth)
        {
            if (depth > maxDepth) return;

            float dx = x2 - x1;
            float dy = y2 - y1;
            float len = (float)Math.Sqrt(dx * dx + dy * dy);
            if (len < 1f) return;

            float px = dy;
            float py = -dx;

            float p3x = x2 + px, p3y = y2 + py;
            float p4x = x1 + px, p4y = y1 + py;

            Color fill = GetGradientColor(depth, maxDepth,
                Color.FromArgb(139, 90, 43),
                Color.FromArgb(34, 139, 34));

            var square = new PointF[]
            {
                new PointF(x1, y1),
                new PointF(x2, y2),
                new PointF(p3x, p3y),
                new PointF(p4x, p4y)
            };
            using (var brush = new SolidBrush(fill))
                g.FillPolygon(brush, square);
            using (var pen = new Pen(Color.FromArgb(60, 60, 60), 0.5f))
                g.DrawPolygon(pen, square);

            float midX = (p4x + p3x) / 2f;
            float midY = (p4y + p3y) / 2f;

            float normPx = dy / len;
            float normPy = -dx / len;

            float apexX = midX + normPx * (len / 2f);
            float apexY = midY + normPy * (len / 2f);

            DrawPythagorasTree(g, p4x, p4y, apexX, apexY, depth + 1, maxDepth);
            DrawPythagorasTree(g, apexX, apexY, p3x, p3y, depth + 1, maxDepth);
        }

        public void DrawSierpinski(Graphics g, float ax, float ay, float bx, float by, float cx, float cy, int depth, int maxDepth)
        {
            Color fill = GetGradientColor(depth, maxDepth,
                Color.FromArgb(20, 80, 200),
                Color.FromArgb(200, 40, 220));

            var pts = new PointF[]
            {
                new PointF(ax, ay),
                new PointF(bx, by),
                new PointF(cx, cy)
            };
            using (var brush = new SolidBrush(fill))
                g.FillPolygon(brush, pts);
            using (var pen = new Pen(Color.FromArgb(30, 30, 30), 0.5f))
                g.DrawPolygon(pen, pts);

            if (depth == maxDepth) return;

            float mx1 = (ax + bx) / 2f, my1 = (ay + by) / 2f;
            float mx2 = (bx + cx) / 2f, my2 = (by + cy) / 2f;
            float mx3 = (ax + cx) / 2f, my3 = (ay + cy) / 2f;

            DrawSierpinski(g, ax, ay, mx1, my1, mx3, my3, depth + 1, maxDepth);
            DrawSierpinski(g, mx1, my1, bx, by, mx2, my2, depth + 1, maxDepth);
            DrawSierpinski(g, mx3, my3, mx2, my2, cx, cy, depth + 1, maxDepth);
        }
    }
}
