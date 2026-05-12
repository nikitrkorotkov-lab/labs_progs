using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using labs_prog.Services.Lab3;

namespace labs_prog.Views
{
    // Панель с двойной буферизацией для устранения мерцания
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.UpdateStyles();
        }
    }

    public partial class Lab3Control : UserControl
    {
        private Lab3Service _service = new Lab3Service();
        private Bitmap _canvas;
        private Graphics _gCanvas;
        private Color _lineColor = Color.Black;
        private Color _fillColor = Color.White;
        private Color _bgColor = Color.White;
        private int _lineWidth = 2;
        private DashStyle _dashStyle = DashStyle.Solid;
        private string _drawMode = "Pencil";
        private bool _drawing = false;
        private bool _enableFill = false;
        private Point _startPt, _lastPt;
        private Panel _pnlCanvas;
        private MenuStrip _menuStrip;
        private ToolStrip _toolStrip;
        private StatusStrip _statusStrip;
        private ToolStripStatusLabel _lblStatus;
        private Lab3FractalControl _fractalControl;

        public Lab3Control()
        {
            InitializeComponent();
            BuildUI();
        }

        private void BuildUI()
        {
            var innerTabs = new TabControl { Dock = DockStyle.Fill };

            var tabEditor = new TabPage("Часть 1: Графический редактор");
            BuildEditorTab(tabEditor);

            var tabFractals = new TabPage("Часть 2: Фракталы");
            _fractalControl = new Lab3FractalControl { Dock = DockStyle.Fill };
            tabFractals.Controls.Add(_fractalControl);

            var tabFourier = new TabPage("Часть 2: Ряд Фурье (Вар.13)");
            BuildFourierTab(tabFourier);

            innerTabs.TabPages.Add(tabEditor);
            innerTabs.TabPages.Add(tabFractals);
            innerTabs.TabPages.Add(tabFourier);

            this.Controls.Add(innerTabs);
        }

        private void BuildEditorTab(TabPage tab)
        {
            var container = new Panel { Dock = DockStyle.Fill };
            BuildMenu(container);
            BuildToolStrip(container);
            BuildCanvas(container);
            BuildStatusBar(container);
            tab.Controls.Add(container);
            NewCanvas(800, 600);
            container.Resize += (s, e) => _pnlCanvas?.Invalidate();
        }

        private void BuildFourierTab(TabPage tab)
        {
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            var btn = new Button
            {
                Text = "Открыть окно визуализации рядов Фурье",
                Size = new Size(300, 40),
                Location = new Point(20, 20),
                Font = new Font("Segoe UI", 10f)
            };
            btn.Click += (s, e) => OpenFourier();
            panel.Controls.Add(btn);
            tab.Controls.Add(panel);
        }

        private void BuildMenu(Control parent)
        {
            _menuStrip = new MenuStrip();

            var mFile = new ToolStripMenuItem("Файл");
            mFile.DropDownItems.Add("Новый", null, (s, e) => MenuNew());
            mFile.DropDownItems.Add("Открыть", null, (s, e) => MenuOpen());
            mFile.DropDownItems.Add("Сохранить", null, (s, e) => MenuSave());

            var mEdit = new ToolStripMenuItem("Правка");
            mEdit.DropDownItems.Add("Очистить", null, (s, e) => ClearCanvas());
            mEdit.DropDownItems.Add("Фон...", null, (s, e) => ChooseBgColor());

            _menuStrip.Items.AddRange(new ToolStripItem[] { mFile, mEdit });
            parent.Controls.Add(_menuStrip);
        }

        private void BuildToolStrip(Control parent)
        {
            _toolStrip = new ToolStrip { Dock = DockStyle.Top };

            AddToolBtn("✏ Карандаш", "Pencil");
            AddToolBtn("╱ Линия", "Line");
            AddToolBtn("▭ Прямоугольник", "Rect");
            AddToolBtn("◯ Эллипс", "Ellipse");
            AddToolBtn("🪣 Заливка", "Fill");
            _toolStrip.Items.Add(new ToolStripSeparator());

            var btnLineColor = new ToolStripButton("Цвет линии") { BackColor = _lineColor, ForeColor = Color.White };
            btnLineColor.Click += (s, e) =>
            {
                var cd = new ColorDialog { Color = _lineColor };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _lineColor = cd.Color;
                    btnLineColor.BackColor = _lineColor;
                    btnLineColor.ForeColor = cd.Color.GetBrightness() > 0.5f ? Color.Black : Color.White;
                }
            };

            var btnFillColor = new ToolStripButton("Цвет заливки") { BackColor = _fillColor };
            btnFillColor.Click += (s, e) =>
            {
                var cd = new ColorDialog { Color = _fillColor };
                if (cd.ShowDialog() == DialogResult.OK)
                {
                    _fillColor = cd.Color;
                    btnFillColor.BackColor = _fillColor;
                    btnFillColor.ForeColor = cd.Color.GetBrightness() > 0.5f ? Color.Black : Color.White;
                }
            };

            _toolStrip.Items.Add(btnLineColor);
            _toolStrip.Items.Add(btnFillColor);
            _toolStrip.Items.Add(new ToolStripSeparator());

            var chkFill = new CheckBox { Text = "Заливка", Checked = false, AutoSize = true };
            chkFill.CheckedChanged += (s, e) => _enableFill = chkFill.Checked;
            _toolStrip.Items.Add(new ToolStripControlHost(chkFill));
            _toolStrip.Items.Add(new ToolStripSeparator());

            _toolStrip.Items.Add(new ToolStripLabel("Толщина:"));
            var nudWidth = new NumericUpDown { Minimum = 1, Maximum = 30, Value = _lineWidth, Width = 50 };
            nudWidth.ValueChanged += (s, e) => _lineWidth = (int)nudWidth.Value;
            _toolStrip.Items.Add(new ToolStripControlHost(nudWidth));
            _toolStrip.Items.Add(new ToolStripSeparator());

            _toolStrip.Items.Add(new ToolStripLabel("Тип линии:"));
            var cbDash = new ComboBox { Width = 110, DropDownStyle = ComboBoxStyle.DropDownList };
            cbDash.Items.AddRange(new object[] { "Сплошная", "Пунктир", "Штрих", "Штрих-пунктир" });
            cbDash.SelectedIndex = 0;
            cbDash.SelectedIndexChanged += (s, e) =>
            {
                _dashStyle = cbDash.SelectedIndex switch
                {
                    1 => DashStyle.Dot,
                    2 => DashStyle.Dash,
                    3 => DashStyle.DashDot,
                    _ => DashStyle.Solid
                };
            };
            _toolStrip.Items.Add(new ToolStripControlHost(cbDash));
            _toolStrip.Items.Add(new ToolStripSeparator());

            var btnNew = new ToolStripButton("Новый");
            btnNew.Click += (s, e) => MenuNew();
            var btnOpen = new ToolStripButton("Открыть");
            btnOpen.Click += (s, e) => MenuOpen();
            var btnSave = new ToolStripButton("Сохранить");
            btnSave.Click += (s, e) => MenuSave();
            var btnClear = new ToolStripButton("Очистить");
            btnClear.Click += (s, e) => ClearCanvas();

            _toolStrip.Items.Add(btnNew);
            _toolStrip.Items.Add(btnOpen);
            _toolStrip.Items.Add(btnSave);
            _toolStrip.Items.Add(btnClear);

            parent.Controls.Add(_toolStrip);
        }

        private void AddToolBtn(string text, string mode)
        {
            var btn = new ToolStripButton(text);
            btn.Click += (s, e) =>
            {
                _drawMode = mode;
                _lblStatus.Text = $"Инструмент: {text}";
            };
            _toolStrip.Items.Add(btn);
        }

        private void BuildCanvas(Control parent)
        {
            var scroll = new Panel { Dock = DockStyle.Fill, AutoScroll = true, BackColor = Color.DimGray };
            _pnlCanvas = new DoubleBufferedPanel { BackColor = Color.White, Size = new Size(800, 600) };
            _pnlCanvas.Paint += PnlCanvas_Paint;
            _pnlCanvas.MouseDown += Canvas_MouseDown;
            _pnlCanvas.MouseMove += Canvas_MouseMove;
            _pnlCanvas.MouseUp += Canvas_MouseUp;
            scroll.Controls.Add(_pnlCanvas);
            parent.Controls.Add(scroll);
        }

        private void BuildStatusBar(Control parent)
        {
            _statusStrip = new StatusStrip();
            _lblStatus = new ToolStripStatusLabel("Готово") { Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            var lblCoord = new ToolStripStatusLabel("X:0 Y:0");
            _pnlCanvas.MouseMove += (s, e) => lblCoord.Text = $"X:{e.X}  Y:{e.Y}";
            _statusStrip.Items.Add(_lblStatus);
            _statusStrip.Items.Add(lblCoord);
            parent.Controls.Add(_statusStrip);
        }

        private void NewCanvas(int w, int h)
        {
            _canvas?.Dispose();
            _gCanvas?.Dispose();
            _canvas = new Bitmap(w, h);
            _gCanvas = Graphics.FromImage(_canvas);
            _gCanvas.SmoothingMode = SmoothingMode.AntiAlias;
            _gCanvas.Clear(_bgColor);
            _pnlCanvas.Size = new Size(w, h);
            _pnlCanvas.Invalidate();
        }

        private void ClearCanvas()
        {
            _gCanvas.Clear(_bgColor);
            _pnlCanvas.Invalidate();
        }

        private void ChooseBgColor()
        {
            var cd = new ColorDialog { Color = _bgColor };
            if (cd.ShowDialog() == DialogResult.OK)
            {
                _bgColor = cd.Color;
                _pnlCanvas.BackColor = _bgColor;
                
                Bitmap newCanvas = new Bitmap(_canvas.Width, _canvas.Height);
                using (Graphics g = Graphics.FromImage(newCanvas))
                {
                    g.Clear(_bgColor);
                    g.DrawImage(_canvas, 0, 0);
                }
                _canvas.Dispose();
                _gCanvas.Dispose();
                _canvas = newCanvas;
                _gCanvas = Graphics.FromImage(_canvas);
                _gCanvas.SmoothingMode = SmoothingMode.AntiAlias;
                _pnlCanvas.Invalidate();
            }
        }

        private void PnlCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (_canvas == null) return;
            e.Graphics.DrawImage(_canvas, 0, 0);

            if (_drawing && (_drawMode == "Line" || _drawMode == "Rect" || _drawMode == "Ellipse"))
            {
                using var pen = _service.CreatePen(_lineColor, _lineWidth, _dashStyle);
                DrawShapePreview(e.Graphics, pen, _drawMode, _startPt, _lastPt, _enableFill);
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _drawing = true;
            _startPt = _lastPt = e.Location;

            if (_drawMode == "Fill")
            {
                _service.FloodFill(_canvas, e.Location, _fillColor);
                _pnlCanvas.Invalidate();
                _drawing = false;
                return;
            }
            if (_drawMode == "Pencil")
            {
                using var pen = _service.CreatePen(_lineColor, _lineWidth, _dashStyle);
                _service.DrawPencilPoint(_gCanvas, pen, e.Location, _lineWidth);
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_drawing) return;

            if (_drawMode == "Pencil")
            {
                using var pen = _service.CreatePen(_lineColor, _lineWidth, _dashStyle);
                _service.DrawLine(_gCanvas, pen, _lastPt, e.Location);
                _lastPt = e.Location;
                _pnlCanvas.Invalidate();
            }
            else
            {
                _lastPt = e.Location;
                _pnlCanvas.Invalidate();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (!_drawing) return;
            _drawing = false;

            if (_drawMode == "Line" || _drawMode == "Rect" || _drawMode == "Ellipse")
            {
                using var pen = _service.CreatePen(_lineColor, _lineWidth, _dashStyle);
                DrawShapePreview(_gCanvas, pen, _drawMode, _startPt, e.Location, _enableFill);
                _pnlCanvas.Invalidate();
            }
        }

        private void MenuNew()
        {
            using var dlg = new Lab3NewCanvasDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
                NewCanvas(dlg.CanvasWidth, dlg.CanvasHeight);
        }

        private void MenuOpen()
        {
            var ofd = new OpenFileDialog { Filter = "Изображения|*.png;*.bmp;*.jpg;*.jpeg;*.gif|Все файлы|*.*" };
            if (ofd.ShowDialog() != DialogResult.OK) return;

            var loaded = new Bitmap(ofd.FileName);
            NewCanvas(loaded.Width, loaded.Height);
            _gCanvas.DrawImage(loaded, 0, 0);
            _pnlCanvas.Invalidate();
        }

        private void MenuSave()
        {
            var sfd = new SaveFileDialog { Filter = "PNG|*.png|BMP|*.bmp|JPEG|*.jpg", DefaultExt = "png" };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var fmt = sfd.FilterIndex switch { 2 => ImageFormat.Bmp, 3 => ImageFormat.Jpeg, _ => ImageFormat.Png };
            _canvas.Save(sfd.FileName, fmt);
            _lblStatus.Text = $"Сохранено: {sfd.FileName}";
        }

        private void OpenFourier()
        {
            var f = new Lab3FourierForm();
            f.Show();
        }

        private void DrawShapePreview(Graphics g, Pen pen, string mode, Point a, Point b, bool fill)
        {
            switch (mode)
            {
                case "Line":
                    _service.DrawLine(g, pen, a, b);
                    break;
                case "Rect":
                    _service.DrawRectangle(g, pen, _fillColor, a, b, fill);
                    break;
                case "Ellipse":
                    _service.DrawEllipse(g, pen, _fillColor, a, b, fill);
                    break;
            }
        }
    }
}
