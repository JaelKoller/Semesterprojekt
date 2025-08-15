namespace Semesterprojekt
{
    partial class Dashboard
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
            this.BtnDashMaNew = new System.Windows.Forms.Button();
            this.BtnDashKndNew = new System.Windows.Forms.Button();
            this.BtnDashAllKntkt = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnDashMaNew
            // 
            this.BtnDashMaNew.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BtnDashMaNew.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDashMaNew.Location = new System.Drawing.Point(109, 71);
            this.BtnDashMaNew.Name = "BtnDashMaNew";
            this.BtnDashMaNew.Size = new System.Drawing.Size(160, 85);
            this.BtnDashMaNew.TabIndex = 0;
            this.BtnDashMaNew.Text = "Mitarbeiter hinzufügen";
            this.BtnDashMaNew.UseVisualStyleBackColor = false;
            this.BtnDashMaNew.Click += new System.EventHandler(this.BtnDashMaNew_Click);
            // 
            // BtnDashKndNew
            // 
            this.BtnDashKndNew.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BtnDashKndNew.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDashKndNew.Location = new System.Drawing.Point(109, 162);
            this.BtnDashKndNew.Name = "BtnDashKndNew";
            this.BtnDashKndNew.Size = new System.Drawing.Size(160, 85);
            this.BtnDashKndNew.TabIndex = 1;
            this.BtnDashKndNew.Text = "Kunde hinzufügen";
            this.BtnDashKndNew.UseVisualStyleBackColor = false;
            this.BtnDashKndNew.Click += new System.EventHandler(this.BtnDashKndNew_Click);
            // 
            // BtnDashAllKntkt
            // 
            this.BtnDashAllKntkt.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BtnDashAllKntkt.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnDashAllKntkt.Location = new System.Drawing.Point(275, 71);
            this.BtnDashAllKntkt.Name = "BtnDashAllKntkt";
            this.BtnDashAllKntkt.Size = new System.Drawing.Size(160, 85);
            this.BtnDashAllKntkt.TabIndex = 2;
            this.BtnDashAllKntkt.Text = "Alle Kontakte";
            this.BtnDashAllKntkt.UseVisualStyleBackColor = false;
            this.BtnDashAllKntkt.Click += new System.EventHandler(this.BtnDashAllKntkt_Click);
            // 
            // Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(533, 450);
            this.Controls.Add(this.BtnDashAllKntkt);
            this.Controls.Add(this.BtnDashKndNew);
            this.Controls.Add(this.BtnDashMaNew);
            this.Name = "Dashboard";
            this.Text = "Dashboard";
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button BtnDashMaNew;
        internal System.Windows.Forms.Button BtnDashKndNew;
        internal System.Windows.Forms.Button BtnDashAllKntkt;
    }
}

