namespace labs_prog.Views
{
    partial class Lab1Control
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBitwiseAdd = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOddDivisors = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnRectangles = new System.Windows.Forms.Button();
            this.txtArea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBitwiseAdd);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtSurname);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(15, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(350, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поразрядное сложение";
            // 
            // btnBitwiseAdd
            // 
            this.btnBitwiseAdd.Location = new System.Drawing.Point(120, 90);
            this.btnBitwiseAdd.Name = "btnBitwiseAdd";
            this.btnBitwiseAdd.Size = new System.Drawing.Size(200, 25);
            this.btnBitwiseAdd.TabIndex = 4;
            this.btnBitwiseAdd.Text = "Выполнить";
            this.btnBitwiseAdd.UseVisualStyleBackColor = true;
            this.btnBitwiseAdd.Click += new System.EventHandler(this.btnBitwiseAdd_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 55);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 20);
            this.txtName.TabIndex = 3;
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(120, 25);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(200, 20);
            this.txtSurname.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Имя:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Фамилия:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOddDivisors);
            this.groupBox2.Controls.Add(this.txtNumber);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(385, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(350, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Сумма нечетных делителей";
            // 
            // btnOddDivisors
            // 
            this.btnOddDivisors.Location = new System.Drawing.Point(120, 50);
            this.btnOddDivisors.Name = "btnOddDivisors";
            this.btnOddDivisors.Size = new System.Drawing.Size(200, 25);
            this.btnOddDivisors.TabIndex = 2;
            this.btnOddDivisors.Text = "Вычислить";
            this.btnOddDivisors.UseVisualStyleBackColor = true;
            this.btnOddDivisors.Click += new System.EventHandler(this.btnOddDivisors_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(120, 25);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(200, 20);
            this.txtNumber.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Число:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnRectangles);
            this.groupBox3.Controls.Add(this.txtArea);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(385, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(350, 90);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Равновеликие прямоугольники";
            // 
            // btnRectangles
            // 
            this.btnRectangles.Location = new System.Drawing.Point(120, 50);
            this.btnRectangles.Name = "btnRectangles";
            this.btnRectangles.Size = new System.Drawing.Size(200, 25);
            this.btnRectangles.TabIndex = 2;
            this.btnRectangles.Text = "Найти";
            this.btnRectangles.UseVisualStyleBackColor = true;
            this.btnRectangles.Click += new System.EventHandler(this.btnRectangles_Click);
            // 
            // txtArea
            // 
            this.txtArea.Location = new System.Drawing.Point(120, 25);
            this.txtArea.Name = "txtArea";
            this.txtArea.Size = new System.Drawing.Size(200, 20);
            this.txtArea.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Площадь S:";
            // 
            // txtResult
            // 
            this.txtResult.Font = new System.Drawing.Font("Consolas", 9F);
            this.txtResult.Location = new System.Drawing.Point(15, 220);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ReadOnly = true;
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResult.Size = new System.Drawing.Size(720, 250);
            this.txtResult.TabIndex = 3;
            // 
            // Lab1Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Lab1Control";
            this.Size = new System.Drawing.Size(750, 490);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBitwiseAdd;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnOddDivisors;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnRectangles;
        private System.Windows.Forms.TextBox txtArea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtResult;
    }
}
