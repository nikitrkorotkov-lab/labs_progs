using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using labs_prog.Services.Lab3;

namespace labs_prog.Views
{
    public partial class Lab3FractalControl : UserControl
    {
        private readonly Lab3FractalService _service = new Lab3FractalService();
        private Bitmap _bmpPythagoras;
        private Bitmap _bmpSierpinski;
        private bool _pythagorasDrawing = false;
        private bool _sierpinskiDrawing = false;

        public Lab3FractalControl()
        {
            InitializeComponent();
            pnlPythagoras.Paint += PnlPythagoras_Paint;
            pnlSierpinski.Paint += PnlSierpinski_Paint;
            pnlPythagoras.Resize += (s, e) => RedrawPythagoras();
            pnlSierpinski.Resize += (s, e) => RedrawSierpinski();
            nudPythagoras.ValueChanged += (s, e) => RedrawPythagoras();
            nudSierpinski.ValueChanged += (s, e) => RedrawSierpinski();
            this.Load += async (s, e) =>
            {
                await Task.Delay(50);
                RedrawPythagoras();
                RedrawSierpinski();
            };
        }

        private async void RedrawPythagoras()
        {
            if (_pythagorasDrawing) return;
            _pythagorasDrawing = true;
            lblStatus.Text = "Рисую дерево Пифагора...";

            int depth = (int)nudPythagoras.Value;
            int w = Math.Max(pnlPythagoras.Width, 10);
            int h = Math.Max(pnlPythagoras.Height, 10);

            Bitmap bmp = await Task.Run(() => RenderPythagoras(w, h, depth));
            _bmpPythagoras?.Dispose();
            _bmpPythagoras = bmp;
            pnlPythagoras.Invalidate();

            lblStatus.Text = $"Дерево Пифагора: глубина {depth} — готово";
            _pythagorasDrawing = false;
        }

        private Bitmap RenderPythagoras(int w, int h, int depth)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(25, 25, 25));
                
                float trunkLen = w * 0.14f;
                float x1 = w / 2f - trunkLen / 2f;
                float x2 = w / 2f + trunkLen / 2f;
                float y = h - 20f;
                
                _service.DrawPythagorasTree(g, x1, y, x2, y, 0, depth);
            }
            return bmp;
        }

        private async void RedrawSierpinski()
        {
            if (_sierpinskiDrawing) return;
            _sierpinskiDrawing = true;
            lblStatus.Text = "Рисую салфетку Серпинского...";

            int depth = (int)nudSierpinski.Value;
            int w = Math.Max(pnlSierpinski.Width, 10);
            int h = Math.Max(pnlSierpinski.Height, 10);

            Bitmap bmp = await Task.Run(() => RenderSierpinski(w, h, depth));
            _bmpSierpinski?.Dispose();
            _bmpSierpinski = bmp;
            pnlSierpinski.Invalidate();

            lblStatus.Text = $"Серпинский: глубина {depth} — готово";
            _sierpinskiDrawing = false;
        }

        private Bitmap RenderSierpinski(int w, int h, int depth)
        {
            var bmp = new Bitmap(w, h);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.FromArgb(25, 25, 25));
                
                int margin = 15;
                float size = Math.Min(w, h) - margin * 2;
                float cx = w / 2f;
                float topY = margin;
                float bottomY = topY + size;
                
                float ax = cx;
                float ay = topY;
                float bx = cx - size / 2f;
                float by = bottomY;
                float ccx = cx + size / 2f;
                float ccy = bottomY;
                
                _service.DrawSierpinski(g, ax, ay, bx, by, ccx, ccy, 0, depth);
            }
            return bmp;
        }

        private void PnlPythagoras_Paint(object sender, PaintEventArgs e)
        {
            if (_bmpPythagoras != null)
                e.Graphics.DrawImage(_bmpPythagoras, 0, 0);
        }

        private void PnlSierpinski_Paint(object sender, PaintEventArgs e)
        {
            if (_bmpSierpinski != null)
                e.Graphics.DrawImage(_bmpSierpinski, 0, 0);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            _bmpPythagoras?.Dispose();
            _bmpSierpinski?.Dispose();
            base.OnHandleDestroyed(e);
        }
    }
}
