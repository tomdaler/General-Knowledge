namespace Planilla.RepEmpl
{
    partial class Descuentos
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
            this.dgObs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgObs
            // 
            this.dgObs.AllowUserToAddRows = false;
            this.dgObs.AllowUserToDeleteRows = false;
            this.dgObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObs.Location = new System.Drawing.Point(51, 57);
            this.dgObs.Name = "dgObs";
            this.dgObs.Size = new System.Drawing.Size(915, 449);
            this.dgObs.TabIndex = 85;
            this.dgObs.Visible = false;
            // 
            // Descuentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgObs);
            this.Name = "Descuentos";
            this.Text = "Descuentos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Descuentos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgObs;
    }
}