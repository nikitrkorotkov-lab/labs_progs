namespace labs_prog.Views
{
    partial class Lab2Control
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpSettings    = new System.Windows.Forms.GroupBox();
            this.rbRandom       = new System.Windows.Forms.RadioButton();
            this.rbFrequency    = new System.Windows.Forms.RadioButton();
            this.rbManual       = new System.Windows.Forms.RadioButton();
            this.panelFreq      = new System.Windows.Forms.Panel();
            this.lblFreqValue   = new System.Windows.Forms.Label();
            this.txtFreqValue   = new System.Windows.Forms.TextBox();
            this.lblFreqCount   = new System.Windows.Forms.Label();
            this.txtFreqCount   = new System.Windows.Forms.TextBox();
            this.lblArraySize   = new System.Windows.Forms.Label();
            this.nudArraySize   = new System.Windows.Forms.NumericUpDown();
            this.lblDisplayLimit= new System.Windows.Forms.Label();
            this.nudDisplayLimit= new System.Windows.Forms.NumericUpDown();
            this.btnGenerate    = new System.Windows.Forms.Button();
            this.lblInputArray  = new System.Windows.Forms.Label();
            this.lblInputInfo   = new System.Windows.Forms.Label();
            this.dgvInput       = new System.Windows.Forms.DataGridView();
            this.grpProcess     = new System.Windows.Forms.GroupBox();
            this.lblK           = new System.Windows.Forms.Label();
            this.txtK           = new System.Windows.Forms.TextBox();
            this.btnNoThread    = new System.Windows.Forms.Button();
            this.btnThreaded    = new System.Windows.Forms.Button();
            this.lblTimeNoThread= new System.Windows.Forms.Label();
            this.lblTimeThread  = new System.Windows.Forms.Label();
            this.lblOutputArray = new System.Windows.Forms.Label();
            this.lblOutputInfo  = new System.Windows.Forms.Label();
            this.dgvOutput      = new System.Windows.Forms.DataGridView();
            this.lblSteps       = new System.Windows.Forms.Label();
            this.txtSteps       = new System.Windows.Forms.TextBox();
            this.lblExceptions  = new System.Windows.Forms.Label();
            this.txtExceptions  = new System.Windows.Forms.TextBox();
            this.btnClearLog    = new System.Windows.Forms.Button();

            this.grpSettings.SuspendLayout();
            this.panelFreq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArraySize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.grpProcess.SuspendLayout();
            this.SuspendLayout();

            this.grpSettings.Location = new System.Drawing.Point(5, 5);
            this.grpSettings.Size = new System.Drawing.Size(775, 130);
            this.grpSettings.Text = "Настройки массива (диапазон [-40, 30])";

            this.grpSettings.Controls.Add(this.rbRandom);
            this.grpSettings.Controls.Add(this.rbFrequency);
            this.grpSettings.Controls.Add(this.panelFreq);
            this.grpSettings.Controls.Add(this.rbManual);
            this.grpSettings.Controls.Add(this.lblArraySize);
            this.grpSettings.Controls.Add(this.nudArraySize);
            this.grpSettings.Controls.Add(this.lblDisplayLimit);
            this.grpSettings.Controls.Add(this.nudDisplayLimit);
            this.grpSettings.Controls.Add(this.btnGenerate);

            this.rbRandom.Location = new System.Drawing.Point(10, 25);
            this.rbRandom.Size = new System.Drawing.Size(110, 20);
            this.rbRandom.Text = "Случайно";
            this.rbRandom.Checked = true;
            this.rbRandom.CheckedChanged += new System.EventHandler(this.rbRandom_CheckedChanged);

            this.rbFrequency.Location = new System.Drawing.Point(10, 52);
            this.rbFrequency.Size = new System.Drawing.Size(130, 20);
            this.rbFrequency.Text = "С частотой:";
            this.rbFrequency.CheckedChanged += new System.EventHandler(this.rbFrequency_CheckedChanged);

            this.panelFreq.Location = new System.Drawing.Point(145, 46);
            this.panelFreq.Size = new System.Drawing.Size(310, 26);
            this.panelFreq.Visible = false;
            this.panelFreq.Controls.Add(this.lblFreqValue);
            this.panelFreq.Controls.Add(this.txtFreqValue);
            this.panelFreq.Controls.Add(this.lblFreqCount);
            this.panelFreq.Controls.Add(this.txtFreqCount);

            this.lblFreqValue.Text = "Число:";
            this.lblFreqValue.Location = new System.Drawing.Point(0, 5);
            this.lblFreqValue.AutoSize = true;

            this.txtFreqValue.Location = new System.Drawing.Point(50, 2);
            this.txtFreqValue.Size = new System.Drawing.Size(60, 20);
            this.txtFreqValue.Text = "11";

            this.lblFreqCount.Text = "Раз:";
            this.lblFreqCount.Location = new System.Drawing.Point(120, 5);
            this.lblFreqCount.AutoSize = true;

            this.txtFreqCount.Location = new System.Drawing.Point(150, 2);
            this.txtFreqCount.Size = new System.Drawing.Size(40, 20);
            this.txtFreqCount.Text = "3";

            this.rbManual.Location = new System.Drawing.Point(10, 72);
            this.rbManual.Size = new System.Drawing.Size(110, 20);
            this.rbManual.Text = "Вручную";

            this.lblArraySize.Text = "Размер массива n:";
            this.lblArraySize.Location = new System.Drawing.Point(10, 100);
            this.lblArraySize.AutoSize = true;

            this.nudArraySize.Location = new System.Drawing.Point(145, 97);
            this.nudArraySize.Size = new System.Drawing.Size(70, 20);
            this.nudArraySize.Minimum = 6;
            this.nudArraySize.Maximum = 100000;
            this.nudArraySize.Value = 10;

            this.lblDisplayLimit.Text = "Показывать первых N элементов:";
            this.lblDisplayLimit.Location = new System.Drawing.Point(230, 100);
            this.lblDisplayLimit.AutoSize = true;

            this.nudDisplayLimit.Location = new System.Drawing.Point(475, 97);
            this.nudDisplayLimit.Size = new System.Drawing.Size(70, 20);
            this.nudDisplayLimit.Minimum = 1;
            this.nudDisplayLimit.Maximum = 100000;
            this.nudDisplayLimit.Value = 50;
            this.nudDisplayLimit.ValueChanged += new System.EventHandler(this.nudDisplayLimit_ValueChanged);

            this.btnGenerate.Location = new System.Drawing.Point(575, 35);
            this.btnGenerate.Size = new System.Drawing.Size(185, 30);
            this.btnGenerate.Text = "Инициализировать массив";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);

            this.lblInputArray.Location = new System.Drawing.Point(5, 142);
            this.lblInputArray.AutoSize = true;
            this.lblInputArray.Text = "Входной массив:";

            this.lblInputInfo.Location = new System.Drawing.Point(150, 142);
            this.lblInputInfo.AutoSize = true;
            this.lblInputInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblInputInfo.Text = "";

            this.dgvInput.Location = new System.Drawing.Point(5, 160);
            this.dgvInput.Size = new System.Drawing.Size(775, 52);

            this.grpProcess.Location = new System.Drawing.Point(5, 220);
            this.grpProcess.Size = new System.Drawing.Size(775, 65);
            this.grpProcess.Text = "Обработка";
            this.grpProcess.Controls.Add(this.lblK);
            this.grpProcess.Controls.Add(this.txtK);
            this.grpProcess.Controls.Add(this.btnNoThread);
            this.grpProcess.Controls.Add(this.btnThreaded);
            this.grpProcess.Controls.Add(this.lblTimeNoThread);
            this.grpProcess.Controls.Add(this.lblTimeThread);

            this.lblK.Text = "k =";
            this.lblK.Location = new System.Drawing.Point(10, 25);
            this.lblK.AutoSize = true;

            this.txtK.Location = new System.Drawing.Point(35, 22);
            this.txtK.Size = new System.Drawing.Size(55, 20);
            this.txtK.Text = "99";

            this.btnNoThread.Location = new System.Drawing.Point(100, 20);
            this.btnNoThread.Size = new System.Drawing.Size(185, 26);
            this.btnNoThread.Text = "Выполнить (без потоков)";
            this.btnNoThread.Click += new System.EventHandler(this.btnNoThread_Click);

            this.btnThreaded.Location = new System.Drawing.Point(295, 20);
            this.btnThreaded.Size = new System.Drawing.Size(185, 26);
            this.btnThreaded.Text = "Выполнить (с потоками)";
            this.btnThreaded.Click += new System.EventHandler(this.btnThreaded_Click);

            this.lblTimeNoThread.Location = new System.Drawing.Point(490, 12);
            this.lblTimeNoThread.Size = new System.Drawing.Size(270, 18);
            this.lblTimeNoThread.Text = "Без потоков: —";

            this.lblTimeThread.Location = new System.Drawing.Point(490, 35);
            this.lblTimeThread.Size = new System.Drawing.Size(270, 18);
            this.lblTimeThread.Text = "С потоками:  —";

            this.lblOutputArray.Location = new System.Drawing.Point(5, 293);
            this.lblOutputArray.AutoSize = true;
            this.lblOutputArray.Text = "Результирующий массив:";

            this.lblOutputInfo.Location = new System.Drawing.Point(210, 293);
            this.lblOutputInfo.AutoSize = true;
            this.lblOutputInfo.ForeColor = System.Drawing.Color.DimGray;
            this.lblOutputInfo.Text = "";

            this.dgvOutput.Location = new System.Drawing.Point(5, 310);
            this.dgvOutput.Size = new System.Drawing.Size(775, 52);

            this.lblSteps.Location = new System.Drawing.Point(5, 370);
            this.lblSteps.AutoSize = true;
            this.lblSteps.Text = "Промежуточные шаги:";

            this.txtSteps.Location = new System.Drawing.Point(5, 388);
            this.txtSteps.Size = new System.Drawing.Size(375, 110);
            this.txtSteps.Multiline = true;
            this.txtSteps.ReadOnly = true;
            this.txtSteps.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSteps.Font = new System.Drawing.Font("Consolas", 8.5f);
            this.txtSteps.BackColor = System.Drawing.Color.WhiteSmoke;

            this.lblExceptions.Location = new System.Drawing.Point(390, 370);
            this.lblExceptions.AutoSize = true;
            this.lblExceptions.Text = "Лог исключений (также сохраняется в exceptions_lab2.log):";

            this.txtExceptions.Location = new System.Drawing.Point(390, 388);
            this.txtExceptions.Size = new System.Drawing.Size(320, 82);
            this.txtExceptions.Multiline = true;
            this.txtExceptions.ReadOnly = true;
            this.txtExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptions.Font = new System.Drawing.Font("Consolas", 8f);
            this.txtExceptions.BackColor = System.Drawing.Color.MistyRose;

            this.btnClearLog.Location = new System.Drawing.Point(390, 476);
            this.btnClearLog.Size = new System.Drawing.Size(320, 22);
            this.btnClearLog.Text = "Очистить лог";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.lblInputArray);
            this.Controls.Add(this.lblInputInfo);
            this.Controls.Add(this.dgvInput);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.lblOutputArray);
            this.Controls.Add(this.lblOutputInfo);
            this.Controls.Add(this.dgvOutput);
            this.Controls.Add(this.lblSteps);
            this.Controls.Add(this.txtSteps);
            this.Controls.Add(this.lblExceptions);
            this.Controls.Add(this.txtExceptions);
            this.Controls.Add(this.btnClearLog);
            this.Name = "Lab2Control";
            this.Size = new System.Drawing.Size(786, 510);

            this.grpSettings.ResumeLayout(false);
            this.panelFreq.ResumeLayout(false);
            this.panelFreq.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudArraySize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDisplayLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).EndInit();
            this.grpProcess.ResumeLayout(false);
            this.grpProcess.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox         grpSettings;
        private System.Windows.Forms.RadioButton      rbRandom;
        private System.Windows.Forms.RadioButton      rbFrequency;
        private System.Windows.Forms.RadioButton      rbManual;
        private System.Windows.Forms.Panel            panelFreq;
        private System.Windows.Forms.Label            lblFreqValue;
        private System.Windows.Forms.TextBox          txtFreqValue;
        private System.Windows.Forms.Label            lblFreqCount;
        private System.Windows.Forms.TextBox          txtFreqCount;
        private System.Windows.Forms.Label            lblArraySize;
        private System.Windows.Forms.NumericUpDown    nudArraySize;
        private System.Windows.Forms.Label            lblDisplayLimit;
        private System.Windows.Forms.NumericUpDown    nudDisplayLimit;
        private System.Windows.Forms.Button           btnGenerate;
        private System.Windows.Forms.Label            lblInputArray;
        private System.Windows.Forms.Label            lblInputInfo;
        private System.Windows.Forms.DataGridView     dgvInput;
        private System.Windows.Forms.GroupBox         grpProcess;
        private System.Windows.Forms.Label            lblK;
        private System.Windows.Forms.TextBox          txtK;
        private System.Windows.Forms.Button           btnNoThread;
        private System.Windows.Forms.Button           btnThreaded;
        private System.Windows.Forms.Label            lblTimeNoThread;
        private System.Windows.Forms.Label            lblTimeThread;
        private System.Windows.Forms.Label            lblOutputArray;
        private System.Windows.Forms.Label            lblOutputInfo;
        private System.Windows.Forms.DataGridView     dgvOutput;
        private System.Windows.Forms.Label            lblSteps;
        private System.Windows.Forms.TextBox          txtSteps;
        private System.Windows.Forms.Label            lblExceptions;
        private System.Windows.Forms.TextBox          txtExceptions;
        private System.Windows.Forms.Button           btnClearLog;
    }
}
