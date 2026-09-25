namespace labs_prog
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabLab1 = new System.Windows.Forms.TabPage();
            this.lab1Control = new labs_prog.Views.Lab1Control();
            this.tabLab2 = new System.Windows.Forms.TabPage();
            this.lab2Control = new labs_prog.Views.Lab2Control();
            this.tabLab3 = new System.Windows.Forms.TabPage();
            this.lab3Control = new labs_prog.Views.Lab3Control();
            this.tabLab4 = new System.Windows.Forms.TabPage();
            this.lab4Control = new labs_prog.Views.Lab4Control();
            this.tabLab5 = new System.Windows.Forms.TabPage();
            this.lab6Control = new labs_prog.Views.Lab6Control();

            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLabs = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLab1 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLab2 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLab3 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLab4 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuLab5 = new System.Windows.Forms.ToolStripMenuItem();

            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusProcessLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProcessName = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.statusDateTimeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusDateTime = new System.Windows.Forms.ToolStripStatusLabel();

            this.grpGlobalLog = new System.Windows.Forms.GroupBox();
            this.txtGlobalExceptions = new System.Windows.Forms.TextBox();

            this.timerClock = new System.Windows.Forms.Timer(this.components);

            this.tabControl.SuspendLayout();
            this.tabLab1.SuspendLayout();
            this.tabLab2.SuspendLayout();
            this.tabLab3.SuspendLayout();
            this.tabLab4.SuspendLayout();
            this.tabLab5.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.grpGlobalLog.SuspendLayout();
            this.SuspendLayout();

            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuLabs});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";

            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Text = "Файл";

            this.menuExit.Name = "menuExit";
            this.menuExit.Text = "Выход";

            this.menuLabs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuLab1,
            this.menuLab2,
            this.menuLab3,
            this.menuLab4,
            this.menuLab5});
            this.menuLabs.Name = "menuLabs";
            this.menuLabs.Text = "Лабораторные работы";

            this.menuLab1.Name = "menuLab1";
            this.menuLab1.Text = "Лабораторная №1";
            this.menuLab2.Name = "menuLab2";
            this.menuLab2.Text = "Лабораторная №2";
            this.menuLab3.Name = "menuLab3";
            this.menuLab3.Text = "Лабораторная №3";
            this.menuLab4.Name = "menuLab4";
            this.menuLab4.Text = "Лабораторная №4";
            this.menuLab5.Name = "menuLab5";
            this.menuLab5.Text = "Лабораторная №5";

            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusProcessLabel,
            this.statusProcessName,
            this.statusProgress,
            this.statusDateTimeLabel,
            this.statusDateTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 728);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 2;

            this.statusProcessLabel.Name = "statusProcessLabel";
            this.statusProcessLabel.Text = "Процесс:";
            this.statusProcessName.Name = "statusProcessName";
            this.statusProcessName.Text = "Ожидание";
            this.statusProcessName.Spring = false;
            this.statusProcessName.AutoSize = true;

            this.statusProgress.Name = "statusProgress";
            this.statusProgress.Size = new System.Drawing.Size(150, 16);

            this.statusDateTimeLabel.Name = "statusDateTimeLabel";
            this.statusDateTimeLabel.Text = "Дата/время:";
            this.statusDateTime.Name = "statusDateTime";
            this.statusDateTime.Text = "—";
            this.statusDateTime.AutoSize = true;

            this.grpGlobalLog.Controls.Add(this.txtGlobalExceptions);
            this.grpGlobalLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpGlobalLog.Height = 120;
            this.grpGlobalLog.Name = "grpGlobalLog";
            this.grpGlobalLog.TabStop = false;
            this.grpGlobalLog.Text = "Журнал необработанных исключений (главная форма + exceptions_log.txt)";

            this.txtGlobalExceptions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtGlobalExceptions.Multiline = true;
            this.txtGlobalExceptions.ReadOnly = true;
            this.txtGlobalExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtGlobalExceptions.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.txtGlobalExceptions.BackColor = System.Drawing.Color.FromArgb(24, 24, 28);
            this.txtGlobalExceptions.ForeColor = System.Drawing.Color.OrangeRed;
            this.txtGlobalExceptions.Name = "txtGlobalExceptions";

            this.tabControl.Controls.Add(this.tabLab1);
            this.tabControl.Controls.Add(this.tabLab2);
            this.tabControl.Controls.Add(this.tabLab3);
            this.tabControl.Controls.Add(this.tabLab4);
            this.tabControl.Controls.Add(this.tabLab5);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(800, 550);
            this.tabControl.TabIndex = 0;

            this.tabLab1.Controls.Add(this.lab1Control);
            this.tabLab1.Location = new System.Drawing.Point(4, 22);
            this.tabLab1.Name = "tabLab1";
            this.tabLab1.Padding = new System.Windows.Forms.Padding(3);
            this.tabLab1.Size = new System.Drawing.Size(792, 524);
            this.tabLab1.TabIndex = 0;
            this.tabLab1.Text = "Lab 1";
            this.tabLab1.UseVisualStyleBackColor = true;

            this.lab1Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab1Control.Location = new System.Drawing.Point(3, 3);
            this.lab1Control.Name = "lab1Control";
            this.lab1Control.Size = new System.Drawing.Size(786, 518);
            this.lab1Control.TabIndex = 0;

            this.tabLab2.Controls.Add(this.lab2Control);
            this.tabLab2.Location = new System.Drawing.Point(4, 22);
            this.tabLab2.Name = "tabLab2";
            this.tabLab2.Padding = new System.Windows.Forms.Padding(3);
            this.tabLab2.Size = new System.Drawing.Size(792, 524);
            this.tabLab2.TabIndex = 1;
            this.tabLab2.Text = "Lab 2";
            this.tabLab2.UseVisualStyleBackColor = true;

            this.lab2Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab2Control.Location = new System.Drawing.Point(3, 3);
            this.lab2Control.Name = "lab2Control";
            this.lab2Control.Size = new System.Drawing.Size(786, 518);
            this.lab2Control.TabIndex = 0;

            this.tabLab3.Controls.Add(this.lab3Control);
            this.tabLab3.Location = new System.Drawing.Point(4, 22);
            this.tabLab3.Name = "tabLab3";
            this.tabLab3.Size = new System.Drawing.Size(792, 524);
            this.tabLab3.TabIndex = 2;
            this.tabLab3.Text = "Lab 3";
            this.tabLab3.UseVisualStyleBackColor = true;

            this.lab3Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab3Control.Location = new System.Drawing.Point(3, 3);
            this.lab3Control.Name = "lab3Control";
            this.lab3Control.Size = new System.Drawing.Size(786, 518);
            this.lab3Control.TabIndex = 0;

            this.tabLab4.Controls.Add(this.lab4Control);
            this.tabLab4.Location = new System.Drawing.Point(4, 22);
            this.tabLab4.Name = "tabLab4";
            this.tabLab4.Size = new System.Drawing.Size(792, 524);
            this.tabLab4.TabIndex = 3;
            this.tabLab4.Text = "Lab 4";
            this.tabLab4.UseVisualStyleBackColor = true;

            this.lab4Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab4Control.Location = new System.Drawing.Point(3, 3);
            this.lab4Control.Name = "lab4Control";
            this.lab4Control.Size = new System.Drawing.Size(786, 518);
            this.lab4Control.TabIndex = 0;

            this.tabLab5.Controls.Add(this.lab6Control);
            this.tabLab5.Location = new System.Drawing.Point(4, 22);
            this.tabLab5.Name = "tabLab5";
            this.tabLab5.Size = new System.Drawing.Size(792, 524);
            this.tabLab5.TabIndex = 4;
            this.tabLab5.Text = "Lab 5";
            this.tabLab5.UseVisualStyleBackColor = true;

            this.lab6Control.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lab6Control.Location = new System.Drawing.Point(3, 3);
            this.lab6Control.Name = "lab6Control";
            this.lab6Control.Size = new System.Drawing.Size(786, 518);
            this.lab6Control.TabIndex = 0;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 750);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.grpGlobalLog);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Коротков Н.Д. | гр. 3437к | Методы програмирования";
            this.tabControl.ResumeLayout(false);
            this.tabLab1.ResumeLayout(false);
            this.tabLab2.ResumeLayout(false);
            this.tabLab3.ResumeLayout(false);
            this.tabLab4.ResumeLayout(false);
            this.tabLab5.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.grpGlobalLog.ResumeLayout(false);
            this.grpGlobalLog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabLab1;
        private System.Windows.Forms.TabPage tabLab2;
        private System.Windows.Forms.TabPage tabLab3;
        private System.Windows.Forms.TabPage tabLab4;
        private System.Windows.Forms.TabPage tabLab5;
        private Views.Lab1Control lab1Control;
        private Views.Lab2Control lab2Control;
        private Views.Lab3Control lab3Control;
        private Views.Lab4Control lab4Control;
        private Views.Lab6Control lab6Control;

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem menuExit;
        private System.Windows.Forms.ToolStripMenuItem menuLabs;
        private System.Windows.Forms.ToolStripMenuItem menuLab1;
        private System.Windows.Forms.ToolStripMenuItem menuLab2;
        private System.Windows.Forms.ToolStripMenuItem menuLab3;
        private System.Windows.Forms.ToolStripMenuItem menuLab4;
        private System.Windows.Forms.ToolStripMenuItem menuLab5;

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusProcessLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusProcessName;
        private System.Windows.Forms.ToolStripProgressBar statusProgress;
        private System.Windows.Forms.ToolStripStatusLabel statusDateTimeLabel;
        private System.Windows.Forms.ToolStripStatusLabel statusDateTime;

        private System.Windows.Forms.GroupBox grpGlobalLog;
        private System.Windows.Forms.TextBox txtGlobalExceptions;

        private System.Windows.Forms.Timer timerClock;

        #endregion
    }
}
