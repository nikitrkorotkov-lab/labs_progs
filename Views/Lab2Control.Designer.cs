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
            this.btnGenerate    = new System.Windows.Forms.Button();
            this.lblInputArray  = new System.Windows.Forms.Label();
            this.dgvInput       = new System.Windows.Forms.DataGridView();
            this.grpProcess     = new System.Windows.Forms.GroupBox();
            this.lblK           = new System.Windows.Forms.Label();
            this.txtK           = new System.Windows.Forms.TextBox();
            this.btnNoThread    = new System.Windows.Forms.Button();
            this.btnThreaded    = new System.Windows.Forms.Button();
            this.lblTimeNoThread= new System.Windows.Forms.Label();
            this.lblTimeThread  = new System.Windows.Forms.Label();
            this.lblOutputArray = new System.Windows.Forms.Label();
            this.dgvOutput      = new System.Windows.Forms.DataGridView();
            this.lblSteps       = new System.Windows.Forms.Label();
            this.txtSteps       = new System.Windows.Forms.TextBox();
            this.lblExceptions  = new System.Windows.Forms.Label();
            this.txtExceptions  = new System.Windows.Forms.TextBox();
            this.btnClearLog    = new System.Windows.Forms.Button();

            this.grpSettings.SuspendLayout();
            this.panelFreq.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOutput)).BeginInit();
            this.grpProcess.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpSettings
            // 
            this.grpSettings.Location = new System.Drawing.Point(5, 5);
            this.grpSettings.Size = new System.Drawing.Size(775, 100);
            this.grpSettings.Text = "Настройки массива (n=10, диапазон [-40, 30])";

            this.grpSettings.Controls.Add(this.rbRandom);
            this.grpSettings.Controls.Add(this.rbFrequency);
            this.grpSettings.Controls.Add(this.panelFreq);
            this.grpSettings.Controls.Add(this.rbManual);
            this.grpSettings.Controls.Add(this.btnGenerate);
            // 
            // rbRandom
            // 
            this.rbRandom.Location = new System.Drawing.Point(10, 25);
            this.rbRandom.Size = new System.Drawing.Size(110, 20);
            this.rbRandom.Text = "Случайно";
            this.rbRandom.Checked = true;
            this.rbRandom.CheckedChanged += new System.EventHandler(this.rbRandom_CheckedChanged);
            // 
            // rbFrequency
            // 
            this.rbFrequency.Location = new System.Drawing.Point(10, 52);
            this.rbFrequency.Size = new System.Drawing.Size(130, 20);
            this.rbFrequency.Text = "С частотой:";
            this.rbFrequency.CheckedChanged += new System.EventHandler(this.rbFrequency_CheckedChanged);
            // 
            // panelFreq
            // 
            this.panelFreq.Location = new System.Drawing.Point(145, 46);
            this.panelFreq.Size = new System.Drawing.Size(310, 26);
            this.panelFreq.Visible = false;
            this.panelFreq.Controls.Add(this.lblFreqValue);
            this.panelFreq.Controls.Add(this.txtFreqValue);
            this.panelFreq.Controls.Add(this.lblFreqCount);
            this.panelFreq.Controls.Add(this.txtFreqCount);
            // 
            // lblFreqValue
            // 
            this.lblFreqValue.Text = "Число:";
            this.lblFreqValue.Location = new System.Drawing.Point(0, 5);
            this.lblFreqValue.AutoSize = true;
            // 
            // txtFreqValue
            // 
            this.txtFreqValue.Location = new System.Drawing.Point(50, 2);
            this.txtFreqValue.Size = new System.Drawing.Size(60, 20);
            this.txtFreqValue.Text = "11";
            // 
            // lblFreqCount
            // 
            this.lblFreqCount.Text = "Раз:";
            this.lblFreqCount.Location = new System.Drawing.Point(120, 5);
            this.lblFreqCount.AutoSize = true;
            // 
            // txtFreqCount
            // 
            this.txtFreqCount.Location = new System.Drawing.Point(150, 2);
            this.txtFreqCount.Size = new System.Drawing.Size(40, 20);
            this.txtFreqCount.Text = "3";
            // 
            // rbManual
            // 
            this.rbManual.Location = new System.Drawing.Point(10, 72);
            this.rbManual.Size = new System.Drawing.Size(110, 20);
            this.rbManual.Text = "Вручную";
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(575, 35);
            this.btnGenerate.Size = new System.Drawing.Size(185, 30);
            this.btnGenerate.Text = "Инициализировать массив";
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // lblInputArray
            // 
            this.lblInputArray.Location = new System.Drawing.Point(5, 112);
            this.lblInputArray.AutoSize = true;
            this.lblInputArray.Text = "Входной массив:";
            // 
            // dgvInput
            // 
            this.dgvInput.Location = new System.Drawing.Point(5, 130);
            this.dgvInput.Size = new System.Drawing.Size(775, 52);
            // 
            // grpProcess
            // 
            this.grpProcess.Location = new System.Drawing.Point(5, 190);
            this.grpProcess.Size = new System.Drawing.Size(775, 65);
            this.grpProcess.Text = "Обработка";
            this.grpProcess.Controls.Add(this.lblK);
            this.grpProcess.Controls.Add(this.txtK);
            this.grpProcess.Controls.Add(this.btnNoThread);
            this.grpProcess.Controls.Add(this.btnThreaded);
            this.grpProcess.Controls.Add(this.lblTimeNoThread);
            this.grpProcess.Controls.Add(this.lblTimeThread);
            // 
            // lblK
            // 
            this.lblK.Text = "k =";
            this.lblK.Location = new System.Drawing.Point(10, 25);
            this.lblK.AutoSize = true;
            // 
            // txtK
            // 
            this.txtK.Location = new System.Drawing.Point(35, 22);
            this.txtK.Size = new System.Drawing.Size(55, 20);
            this.txtK.Text = "99";
            // 
            // btnNoThread
            // 
            this.btnNoThread.Location = new System.Drawing.Point(100, 20);
            this.btnNoThread.Size = new System.Drawing.Size(185, 26);
            this.btnNoThread.Text = "Выполнить (без потоков)";
            this.btnNoThread.Click += new System.EventHandler(this.btnNoThread_Click);
            // 
            // btnThreaded
            // 
            this.btnThreaded.Location = new System.Drawing.Point(295, 20);
            this.btnThreaded.Size = new System.Drawing.Size(185, 26);
            this.btnThreaded.Text = "Выполнить (с потоками)";
            this.btnThreaded.Click += new System.EventHandler(this.btnThreaded_Click);
            // 
            // lblTimeNoThread
            // 
            this.lblTimeNoThread.Location = new System.Drawing.Point(490, 12);
            this.lblTimeNoThread.Size = new System.Drawing.Size(270, 18);
            this.lblTimeNoThread.Text = "Без потоков: —";
            // 
            // lblTimeThread
            // 
            this.lblTimeThread.Location = new System.Drawing.Point(490, 35);
            this.lblTimeThread.Size = new System.Drawing.Size(270, 18);
            this.lblTimeThread.Text = "С потоками:  —";
            // 
            // lblOutputArray
            // 
            this.lblOutputArray.Location = new System.Drawing.Point(5, 263);
            this.lblOutputArray.AutoSize = true;
            this.lblOutputArray.Text = "Результирующий массив:";
            // 
            // dgvOutput
            // 
            this.dgvOutput.Location = new System.Drawing.Point(5, 280);
            this.dgvOutput.Size = new System.Drawing.Size(775, 52);
            // 
            // lblSteps
            // 
            this.lblSteps.Location = new System.Drawing.Point(5, 340);
            this.lblSteps.AutoSize = true;
            this.lblSteps.Text = "Промежуточные шаги:";
            // 
            // txtSteps
            // 
            this.txtSteps.Location = new System.Drawing.Point(5, 358);
            this.txtSteps.Size = new System.Drawing.Size(375, 140);
            this.txtSteps.Multiline = true;
            this.txtSteps.ReadOnly = true;
            this.txtSteps.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtSteps.Font = new System.Drawing.Font("Consolas", 8.5f);
            this.txtSteps.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // lblExceptions
            // 
            this.lblExceptions.Location = new System.Drawing.Point(390, 340);
            this.lblExceptions.AutoSize = true;
            this.lblExceptions.Text = "Лог исключений (также сохраняется в exceptions_lab2.log):";
            // 
            // txtExceptions
            // 
            this.txtExceptions.Location = new System.Drawing.Point(390, 358);
            this.txtExceptions.Size = new System.Drawing.Size(320, 112);
            this.txtExceptions.Multiline = true;
            this.txtExceptions.ReadOnly = true;
            this.txtExceptions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtExceptions.Font = new System.Drawing.Font("Consolas", 8f);
            this.txtExceptions.BackColor = System.Drawing.Color.MistyRose;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(390, 476);
            this.btnClearLog.Size = new System.Drawing.Size(320, 22);
            this.btnClearLog.Text = "Очистить лог";
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // Lab2Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpSettings);
            this.Controls.Add(this.lblInputArray);
            this.Controls.Add(this.dgvInput);
            this.Controls.Add(this.grpProcess);
            this.Controls.Add(this.lblOutputArray);
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
        private System.Windows.Forms.Button           btnGenerate;
        private System.Windows.Forms.Label            lblInputArray;
        private System.Windows.Forms.DataGridView     dgvInput;
        private System.Windows.Forms.GroupBox         grpProcess;
        private System.Windows.Forms.Label            lblK;
        private System.Windows.Forms.TextBox          txtK;
        private System.Windows.Forms.Button           btnNoThread;
        private System.Windows.Forms.Button           btnThreaded;
        private System.Windows.Forms.Label            lblTimeNoThread;
        private System.Windows.Forms.Label            lblTimeThread;
        private System.Windows.Forms.Label            lblOutputArray;
        private System.Windows.Forms.DataGridView     dgvOutput;
        private System.Windows.Forms.Label            lblSteps;
        private System.Windows.Forms.TextBox          txtSteps;
        private System.Windows.Forms.Label            lblExceptions;
        private System.Windows.Forms.TextBox          txtExceptions;
        private System.Windows.Forms.Button           btnClearLog;
    }
}
