namespace labs_prog.Views
{
    partial class Lab3Control
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            _canvas?.Dispose();
            _gCanvas?.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Lab3Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "Lab3Control";
            this.Size = new System.Drawing.Size(786, 518);
            this.ResumeLayout(false);
        }
    }
}
