namespace Planilla.RepDetalle
{
    partial class SindicatoMes
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
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.fechas = new Rep1.Desde();
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgObs
            // 
            this.dgObs.AllowUserToAddRows = false;
            this.dgObs.AllowUserToDeleteRows = false;
            this.dgObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObs.Location = new System.Drawing.Point(261, 12);
            this.dgObs.Name = "dgObs";
            this.dgObs.Size = new System.Drawing.Size(610, 626);
            this.dgObs.TabIndex = 86;
            this.dgObs.Visible = false;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(57, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 39);
            this.button1.TabIndex = 89;
            this.button1.Text = "BUSCAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label6.Location = new System.Drawing.Point(12, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 18);
            this.label6.TabIndex = 163;
            this.label6.Text = "Escoja Mes";
            // 
            // fechas
            // 
            this.fechas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.fechas.Location = new System.Drawing.Point(12, 63);
            this.fechas.Name = "fechas";
            this.fechas.Size = new System.Drawing.Size(219, 109);
            this.fechas.TabIndex = 90;
            // 
            // SindicatoMes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.fechas);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgObs);
            this.Name = "SindicatoMes";
            this.Text = "SindicatoMes";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgObs;
        private System.Windows.Forms.Button button1;
        private Rep1.Desde fechas;
        private System.Windows.Forms.Label label6;
    }
}