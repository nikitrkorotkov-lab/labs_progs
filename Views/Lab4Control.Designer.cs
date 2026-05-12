namespace labs_prog.Views
{
    partial class Lab4Control
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.richBoxMain = new System.Windows.Forms.RichTextBox();
            this.richBoxStats = new System.Windows.Forms.RichTextBox();
            this.comboSignature = new System.Windows.Forms.ComboBox();
            this.datePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.datePickerTo = new System.Windows.Forms.DateTimePicker();
            this.btnParseVariant = new System.Windows.Forms.Button();
            this.btnAnalyzeForms = new System.Windows.Forms.Button();
            this.btnLoadText = new System.Windows.Forms.Button();
            this.btnLoadLog = new System.Windows.Forms.Button();
            this.btnParseSelected = new System.Windows.Forms.Button();
            this.btnParseAsync = new System.Windows.Forms.Button();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblSig = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // richBoxMain
            // 
            this.richBoxMain.Location = new System.Drawing.Point(6, 60);
            this.richBoxMain.Size = new System.Drawing.Size(460, 280);
            this.richBoxMain.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richBoxMain.TabIndex = 0;
            this.richBoxMain.TextChanged += new System.EventHandler(this.richBoxMain_TextChanged);
            // 
            // richBoxStats
            // 
            this.richBoxStats.Location = new System.Drawing.Point(6, 350);
            this.richBoxStats.Size = new System.Drawing.Size(460, 150);
            this.richBoxStats.ReadOnly = true;
            this.richBoxStats.BackColor = System.Drawing.Color.WhiteSmoke;
            this.richBoxStats.TabIndex = 1;
            // 
            // lblSig
            // 
            this.lblSig.Text = "Сигнатура:";
            this.lblSig.Location = new System.Drawing.Point(480, 10);
            this.lblSig.Size = new System.Drawing.Size(70, 20);
            this.lblSig.TabIndex = 2;
            // 
            // comboSignature
            // 
            this.comboSignature.Location = new System.Drawing.Point(555, 7);
            this.comboSignature.Size = new System.Drawing.Size(200, 21);
            this.comboSignature.Items.AddRange(new object[] { "form", "Form", "System", "Exception", "ParseInt", "Convert", "String", "dataGridView" });
            this.comboSignature.TabIndex = 3;
            this.comboSignature.SelectedIndexChanged += new System.EventHandler(this.comboSignature_SelectedIndexChanged);
            // 
            // lblFrom
            // 
            this.lblFrom.Text = "Дата с:";
            this.lblFrom.Location = new System.Drawing.Point(480, 40);
            this.lblFrom.Size = new System.Drawing.Size(55, 20);
            this.lblFrom.TabIndex = 4;
            // 
            // datePickerFrom
            // 
            this.datePickerFrom.Location = new System.Drawing.Point(540, 37);
            this.datePickerFrom.Size = new System.Drawing.Size(120, 20);
            this.datePickerFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerFrom.ShowCheckBox = true;
            this.datePickerFrom.TabIndex = 5;
            // 
            // lblTo
            // 
            this.lblTo.Text = "по:";
            this.lblTo.Location = new System.Drawing.Point(665, 40);
            this.lblTo.Size = new System.Drawing.Size(25, 20);
            this.lblTo.TabIndex = 6;
            // 
            // datePickerTo
            // 
            this.datePickerTo.Location = new System.Drawing.Point(693, 37);
            this.datePickerTo.Size = new System.Drawing.Size(120, 20);
            this.datePickerTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.datePickerTo.ShowCheckBox = true;
            this.datePickerTo.TabIndex = 7;
            // 
            // btnLoadText
            // 
            this.btnLoadText.Text = "Загрузить текст";
            this.btnLoadText.Location = new System.Drawing.Point(6, 6);
            this.btnLoadText.Size = new System.Drawing.Size(120, 26);
            this.btnLoadText.TabIndex = 8;
            this.btnLoadText.Click += new System.EventHandler(this.btnLoadText_Click);
            // 
            // btnAnalyzeForms
            // 
            this.btnAnalyzeForms.Text = "Формы (п.3)";
            this.btnAnalyzeForms.Location = new System.Drawing.Point(134, 6);
            this.btnAnalyzeForms.Size = new System.Drawing.Size(100, 26);
            this.btnAnalyzeForms.TabIndex = 9;
            this.btnAnalyzeForms.Click += new System.EventHandler(this.btnAnalyzeForms_Click);
            // 
            // btnParseVariant
            // 
            this.btnParseVariant.Text = "Парсинг (вар.13)";
            this.btnParseVariant.Location = new System.Drawing.Point(242, 6);
            this.btnParseVariant.Size = new System.Drawing.Size(130, 26);
            this.btnParseVariant.TabIndex = 10;
            this.btnParseVariant.Click += new System.EventHandler(this.btnParseVariant_Click);
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.Location = new System.Drawing.Point(480, 70);
            this.listBoxLogs.Size = new System.Drawing.Size(280, 120);
            this.listBoxLogs.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxLogs.TabIndex = 11;
            // 
            // btnLoadLog
            // 
            this.btnLoadLog.Text = "Добавить журналы";
            this.btnLoadLog.Location = new System.Drawing.Point(480, 200);
            this.btnLoadLog.Size = new System.Drawing.Size(130, 26);
            this.btnLoadLog.TabIndex = 12;
            this.btnLoadLog.Click += new System.EventHandler(this.btnLoadLog_Click);
            // 
            // btnParseSelected
            // 
            this.btnParseSelected.Text = "Парсить (синхр.)";
            this.btnParseSelected.Location = new System.Drawing.Point(480, 234);
            this.btnParseSelected.Size = new System.Drawing.Size(130, 26);
            this.btnParseSelected.TabIndex = 13;
            this.btnParseSelected.Click += new System.EventHandler(this.btnParseSelected_Click);
            // 
            // btnParseAsync
            // 
            this.btnParseAsync.Text = "Парсить (асинхр.)";
            this.btnParseAsync.Location = new System.Drawing.Point(620, 234);
            this.btnParseAsync.Size = new System.Drawing.Size(130, 26);
            this.btnParseAsync.TabIndex = 14;
            this.btnParseAsync.Click += new System.EventHandler(this.btnParseAsync_Click);
            // 
            // Lab4Control
            // 
            this.Size = new System.Drawing.Size(820, 510);
            this.Controls.Add(this.richBoxMain);
            this.Controls.Add(this.richBoxStats);
            this.Controls.Add(this.comboSignature);
            this.Controls.Add(this.datePickerFrom);
            this.Controls.Add(this.datePickerTo);
            this.Controls.Add(this.btnParseVariant);
            this.Controls.Add(this.btnAnalyzeForms);
            this.Controls.Add(this.btnLoadText);
            this.Controls.Add(this.btnLoadLog);
            this.Controls.Add(this.btnParseSelected);
            this.Controls.Add(this.btnParseAsync);
            this.Controls.Add(this.listBoxLogs);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.lblSig);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.RichTextBox richBoxMain;
        private System.Windows.Forms.RichTextBox richBoxStats;
        private System.Windows.Forms.ComboBox comboSignature;
        private System.Windows.Forms.DateTimePicker datePickerFrom;
        private System.Windows.Forms.DateTimePicker datePickerTo;
        private System.Windows.Forms.Button btnParseVariant;
        private System.Windows.Forms.Button btnAnalyzeForms;
        private System.Windows.Forms.Button btnLoadText;
        private System.Windows.Forms.Button btnLoadLog;
        private System.Windows.Forms.Button btnParseSelected;
        private System.Windows.Forms.Button btnParseAsync;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblSig;
    }
}
