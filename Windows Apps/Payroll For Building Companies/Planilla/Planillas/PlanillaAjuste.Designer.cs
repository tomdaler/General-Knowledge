namespace Planilla
{
    partial class PlanillaAjuste
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.bt22 = new System.Windows.Forms.Button();
            this.dgPla = new System.Windows.Forms.DataGridView();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPla)).BeginInit();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.button3);
            this.panel4.Controls.Add(this.bt22);
            this.panel4.Location = new System.Drawing.Point(57, 30);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(153, 165);
            this.panel4.TabIndex = 127;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(21, 98);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(96, 36);
            this.button3.TabIndex = 60;
            this.button3.Text = "ELIMINAR";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // bt22
            // 
            this.bt22.Location = new System.Drawing.Point(21, 10);
            this.bt22.Name = "bt22";
            this.bt22.Size = new System.Drawing.Size(96, 36);
            this.bt22.TabIndex = 59;
            this.bt22.Text = "CREAR";
            this.bt22.UseVisualStyleBackColor = true;
            this.bt22.Click += new System.EventHandler(this.bt22_Click);
            // 
            // dgPla
            // 
            this.dgPla.AllowUserToAddRows = false;
            this.dgPla.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgPla.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgPla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPla.Location = new System.Drawing.Point(252, 30);
            this.dgPla.Name = "dgPla";
            this.dgPla.ReadOnly = true;
            this.dgPla.Size = new System.Drawing.Size(807, 555);
            this.dgPla.TabIndex = 124;
            this.dgPla.Visible = false;
            // 
            // PlanillaAjuste
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 566);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.dgPla);
            this.Name = "PlanillaAjuste";
            this.Text = "PlanillaAjuste";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.PlanillaAjuste_Load);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button bt22;
        private System.Windows.Forms.DataGridView dgPla;
    }
}