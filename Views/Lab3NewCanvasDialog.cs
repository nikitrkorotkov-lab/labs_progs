using System;
using System.Drawing;
using System.Windows.Forms;

namespace labs_prog.Views
{
    /// <summary>
    /// Диалог создания нового холста
    /// </summary>
    public class Lab3NewCanvasDialog : Form
    {
        public int CanvasWidth { get; private set; } = 800;
        public int CanvasHeight { get; private set; } = 600;

        public Lab3NewCanvasDialog()
        {
            this.Text = "Новый холст";
            this.Size = new Size(260, 160);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            var nudW = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 4000,
                Value = 800,
                Location = new Point(120, 15),
                Width = 80
            };

            var nudH = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 4000,
                Value = 600,
                Location = new Point(120, 45),
                Width = 80
            };

            var btnOk = new Button
            {
                Text = "OK",
                DialogResult = DialogResult.OK,
                Location = new Point(60, 85),
                Width = 60
            };

            var btnCancel = new Button
            {
                Text = "Отмена",
                DialogResult = DialogResult.Cancel,
                Location = new Point(130, 85),
                Width = 70
            };

            btnOk.Click += (s, e) =>
            {
                CanvasWidth = (int)nudW.Value;
                CanvasHeight = (int)nudH.Value;
            };

            this.Controls.AddRange(new Control[]
            {
                new Label { Text = "Ширина:", Location = new Point(15, 18), AutoSize = true },
                new Label { Text = "Высота:", Location = new Point(15, 48), AutoSize = true },
                nudW,
                nudH,
                btnOk,
                btnCancel
            });

            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }
    }
}
