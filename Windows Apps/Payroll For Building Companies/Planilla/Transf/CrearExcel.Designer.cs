namespace Planilla.Transf
{
    partial class CrearExcel
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
            this.dgPla = new System.Windows.Forms.DataGridView();
            this.cbPla = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgPla)).BeginInit();
            this.SuspendLayout();
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
            this.dgPla.Location = new System.Drawing.Point(375, 22);
            this.dgPla.Name = "dgPla";
            this.dgPla.ReadOnly = true;
            this.dgPla.Size = new System.Drawing.Size(611, 555);
            this.dgPla.TabIndex = 127;
            this.dgPla.Visible = false;
            // 
            // cbPla
            // 
            this.cbPla.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPla.FormattingEnabled = true;
            this.cbPla.Location = new System.Drawing.Point(47, 22);
            this.cbPla.Name = "cbPla";
            this.cbPla.Size = new System.Drawing.Size(254, 26);
            this.cbPla.TabIndex = 126;
            this.cbPla.SelectedIndexChanged += new System.EventHandler(this.cbPla_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(47, 69);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 37);
            this.button1.TabIndex = 128;
            this.button1.Text = "Cargar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CrearExcel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 482);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgPla);
            this.Controls.Add(this.cbPla);
            this.Name = "CrearExcel";
            this.Text = "CrearExcel";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.CrearExcel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgPla)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgPla;
        private System.Windows.Forms.ComboBox cbPla;
        private System.Windows.Forms.Button button1;
    }
}