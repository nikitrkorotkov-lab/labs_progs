namespace labs_prog.Views
{
    partial class Lab3FractalControl
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.grpPythagoras = new System.Windows.Forms.GroupBox();
            this.nudPythagoras = new System.Windows.Forms.NumericUpDown();
            this.lblPythagorasN = new System.Windows.Forms.Label();
            this.grpSierpinski = new System.Windows.Forms.GroupBox();
            this.nudSierpinski = new System.Windows.Forms.NumericUpDown();
            this.lblSierpinskiN = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.pnlPythagoras = new labs_prog.Views.DoubleBufferedPanel();
            this.pnlSierpinski = new labs_prog.Views.DoubleBufferedPanel();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.grpPythagoras.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPythagoras)).BeginInit();
            this.grpSierpinski.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSierpinski)).BeginInit();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.grpPythagoras);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.grpSierpinski);
            this.splitMain.Size = new System.Drawing.Size(947, 692);
            this.splitMain.SplitterDistance = 473;
            this.splitMain.TabIndex = 0;
            // 
            // grpPythagoras
            // 
            this.grpPythagoras.Controls.Add(this.pnlPythagoras);
            this.grpPythagoras.Controls.Add(this.nudPythagoras);
            this.grpPythagoras.Controls.Add(this.lblPythagorasN);
            this.grpPythagoras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPythagoras.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpPythagoras.Location = new System.Drawing.Point(0, 0);
            this.grpPythagoras.Name = "grpPythagoras";
            this.grpPythagoras.Padding = new System.Windows.Forms.Padding(5);
            this.grpPythagoras.Size = new System.Drawing.Size(473, 692);
            this.grpPythagoras.TabIndex = 0;
            this.grpPythagoras.TabStop = false;
            this.grpPythagoras.Text = "Дерево Пифагора";
            // 
            // nudPythagoras
            // 
            this.nudPythagoras.Location = new System.Drawing.Point(100, 19);
            this.nudPythagoras.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nudPythagoras.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudPythagoras.Name = "nudPythagoras";
            this.nudPythagoras.Size = new System.Drawing.Size(55, 23);
            this.nudPythagoras.TabIndex = 1;
            this.nudPythagoras.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblPythagorasN
            // 
            this.lblPythagorasN.AutoSize = true;
            this.lblPythagorasN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPythagorasN.Location = new System.Drawing.Point(8, 22);
            this.lblPythagorasN.Name = "lblPythagorasN";
            this.lblPythagorasN.Size = new System.Drawing.Size(76, 15);
            this.lblPythagorasN.TabIndex = 0;
            this.lblPythagorasN.Text = "Глубина (N):";
            // 
            // grpSierpinski
            // 
            this.grpSierpinski.Controls.Add(this.pnlSierpinski);
            this.grpSierpinski.Controls.Add(this.nudSierpinski);
            this.grpSierpinski.Controls.Add(this.lblSierpinskiN);
            this.grpSierpinski.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpSierpinski.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.grpSierpinski.Location = new System.Drawing.Point(0, 0);
            this.grpSierpinski.Name = "grpSierpinski";
            this.grpSierpinski.Padding = new System.Windows.Forms.Padding(5);
            this.grpSierpinski.Size = new System.Drawing.Size(470, 692);
            this.grpSierpinski.TabIndex = 0;
            this.grpSierpinski.TabStop = false;
            this.grpSierpinski.Text = "Салфетка Серпинского";
            // 
            // nudSierpinski
            // 
            this.nudSierpinski.Location = new System.Drawing.Point(100, 19);
            this.nudSierpinski.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.nudSierpinski.Name = "nudSierpinski";
            this.nudSierpinski.Size = new System.Drawing.Size(55, 23);
            this.nudSierpinski.TabIndex = 1;
            this.nudSierpinski.Value = new decimal(new int[] {
            6,
            0,
            0,
            0});
            // 
            // lblSierpinskiN
            // 
            this.lblSierpinskiN.AutoSize = true;
            this.lblSierpinskiN.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSierpinskiN.Location = new System.Drawing.Point(8, 22);
            this.lblSierpinskiN.Name = "lblSierpinskiN";
            this.lblSierpinskiN.Size = new System.Drawing.Size(76, 15);
            this.lblSierpinskiN.TabIndex = 0;
            this.lblSierpinskiN.Text = "Глубина (N):";
            // 
            // lblStatus
            // 
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblStatus.ForeColor = System.Drawing.Color.Gray;
            this.lblStatus.Location = new System.Drawing.Point(0, 692);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(947, 22);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Готово";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlPythagoras
            // 
            this.pnlPythagoras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPythagoras.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlPythagoras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlPythagoras.Location = new System.Drawing.Point(5, 50);
            this.pnlPythagoras.Name = "pnlPythagoras";
            this.pnlPythagoras.Size = new System.Drawing.Size(463, 637);
            this.pnlPythagoras.TabIndex = 2;
            // 
            // pnlSierpinski
            // 
            this.pnlSierpinski.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSierpinski.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.pnlSierpinski.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSierpinski.Location = new System.Drawing.Point(5, 50);
            this.pnlSierpinski.Name = "pnlSierpinski";
            this.pnlSierpinski.Size = new System.Drawing.Size(460, 637);
            this.pnlSierpinski.TabIndex = 2;
            // 
            // Lab3FractalControl
            // 
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.lblStatus);
            this.Name = "Lab3FractalControl";
            this.Size = new System.Drawing.Size(947, 714);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.grpPythagoras.ResumeLayout(false);
            this.grpPythagoras.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudPythagoras)).EndInit();
            this.grpSierpinski.ResumeLayout(false);
            this.grpSierpinski.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSierpinski)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.GroupBox grpPythagoras;
        private System.Windows.Forms.Label lblPythagorasN;
        private System.Windows.Forms.NumericUpDown nudPythagoras;
        private DoubleBufferedPanel pnlPythagoras;
        private System.Windows.Forms.GroupBox grpSierpinski;
        private System.Windows.Forms.Label lblSierpinskiN;
        private System.Windows.Forms.NumericUpDown nudSierpinski;
        private DoubleBufferedPanel pnlSierpinski;
        private System.Windows.Forms.Label lblStatus;
    }
}
