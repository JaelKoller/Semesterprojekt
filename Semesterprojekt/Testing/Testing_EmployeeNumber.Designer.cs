namespace Semesterprojekt.Testing
{
    partial class Testing_EmployeeNumber
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TxtBxTest = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TxtBxTest
            // 
            this.TxtBxTest.Location = new System.Drawing.Point(13, 13);
            this.TxtBxTest.Name = "TxtBxTest";
            this.TxtBxTest.Size = new System.Drawing.Size(100, 26);
            this.TxtBxTest.TabIndex = 0;
            this.TxtBxTest.Text = "TEST";
            // 
            // Testing_EmployeeNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TxtBxTest);
            this.Name = "Testing_EmployeeNumber";
            this.Text = "Testing_EmployeeNumber";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtBxTest;
    }
}