namespace Planilla.RepDetalle
{
    partial class Adelantos
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
            this.rangoCtl1 = new WindowsFormsControlLibrary1.RangoCtl();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgObs
            // 
            this.dgObs.AllowUserToAddRows = false;
            this.dgObs.AllowUserToDeleteRows = false;
            this.dgObs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgObs.Location = new System.Drawing.Point(249, 38);
            this.dgObs.Name = "dgObs";
            this.dgObs.Size = new System.Drawing.Size(824, 529);
            this.dgObs.TabIndex = 85;
            this.dgObs.Visible = false;
            // 
            // rangoCtl1
            // 
            this.rangoCtl1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rangoCtl1.Location = new System.Drawing.Point(12, 38);
            this.rangoCtl1.Name = "rangoCtl1";
            this.rangoCtl1.Size = new System.Drawing.Size(219, 226);
            this.rangoCtl1.TabIndex = 86;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(46, 303);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 39);
            this.button1.TabIndex = 106;
            this.button1.Text = "BUSCAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Adelantos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rangoCtl1);
            this.Controls.Add(this.dgObs);
            this.Name = "Adelantos";
            this.Text = "Adelantos, Descuentos, Bonos";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Adelantos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgObs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgObs;
        private WindowsFormsControlLibrary1.RangoCtl rangoCtl1;
        private System.Windows.Forms.Button button1;
    }
}