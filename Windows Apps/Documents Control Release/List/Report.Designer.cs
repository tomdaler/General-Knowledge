namespace GPS.List
{
    partial class Report
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
            this.ddlEfficacy = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DT2 = new System.Windows.Forms.DateTimePicker();
            this.DT1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dGrid1 = new System.Windows.Forms.DataGridView();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbMSG = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ddlEfficacyOwner = new System.Windows.Forms.ComboBox();
            this.filtros1 = new GPS.List.Filtros();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // ddlEfficacy
            // 
            this.ddlEfficacy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEfficacy.FormattingEnabled = true;
            this.ddlEfficacy.Location = new System.Drawing.Point(182, 169);
            this.ddlEfficacy.Name = "ddlEfficacy";
            this.ddlEfficacy.Size = new System.Drawing.Size(178, 24);
            this.ddlEfficacy.TabIndex = 147;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(55, 172);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(63, 16);
            this.label9.TabIndex = 146;
            this.label9.Text = "Efficacy";
            // 
            // DT2
            // 
            this.DT2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT2.Location = new System.Drawing.Point(642, 171);
            this.DT2.Name = "DT2";
            this.DT2.Size = new System.Drawing.Size(99, 22);
            this.DT2.TabIndex = 145;
            // 
            // DT1
            // 
            this.DT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT1.Location = new System.Drawing.Point(641, 136);
            this.DT1.Name = "DT1";
            this.DT1.Size = new System.Drawing.Size(100, 22);
            this.DT1.TabIndex = 144;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(574, 172);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 143;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(480, 137);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 16);
            this.label1.TabIndex = 142;
            this.label1.Text = "Creation From";
            // 
            // dGrid1
            // 
            this.dGrid1.AllowUserToAddRows = false;
            this.dGrid1.AllowUserToDeleteRows = false;
            this.dGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid1.Location = new System.Drawing.Point(34, 265);
            this.dGrid1.Name = "dGrid1";
            this.dGrid1.Size = new System.Drawing.Size(1027, 352);
            this.dGrid1.TabIndex = 141;
            this.dGrid1.Visible = false;
            this.dGrid1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGrid1_CellDoubleClick);
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(182, 128);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(178, 24);
            this.ddlStatus.TabIndex = 140;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 131);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 139;
            this.label3.Text = "Status";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(886, 137);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 40);
            this.button2.TabIndex = 134;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(768, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 40);
            this.button1.TabIndex = 133;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.UseWaitCursor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(426, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 24);
            this.label4.TabIndex = 128;
            this.label4.Text = "SEARCH DOCUMENTS";
            // 
            // lbMSG
            // 
            this.lbMSG.AutoSize = true;
            this.lbMSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMSG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMSG.Location = new System.Drawing.Point(55, 236);
            this.lbMSG.Name = "lbMSG";
            this.lbMSG.Size = new System.Drawing.Size(356, 16);
            this.lbMSG.TabIndex = 149;
            this.lbMSG.Text = "Review all the information by double clicking a row";
            this.lbMSG.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(55, 212);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 16);
            this.label6.TabIndex = 150;
            this.label6.Text = "Efficacy Owner";
            // 
            // ddlEfficacyOwner
            // 
            this.ddlEfficacyOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlEfficacyOwner.FormattingEnabled = true;
            this.ddlEfficacyOwner.Location = new System.Drawing.Point(182, 209);
            this.ddlEfficacyOwner.Name = "ddlEfficacyOwner";
            this.ddlEfficacyOwner.Size = new System.Drawing.Size(178, 24);
            this.ddlEfficacyOwner.TabIndex = 151;
            // 
            // filtros1
            // 
            this.filtros1.coverage = "0";
            this.filtros1.dpto = "0";
            this.filtros1.Location = new System.Drawing.Point(-2, 34);
            this.filtros1.Name = "filtros1";
            this.filtros1.orgs = "0";
            this.filtros1.Size = new System.Drawing.Size(1003, 96);
            this.filtros1.TabIndex = 148;
            this.filtros1.textoIni = null;
            this.filtros1.types = "0";
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 742);
            this.Controls.Add(this.ddlEfficacyOwner);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbMSG);
            this.Controls.Add(this.filtros1);
            this.Controls.Add(this.ddlEfficacy);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.DT2);
            this.Controls.Add(this.DT1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dGrid1);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Name = "Report";
            this.Text = "REPORT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Report_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ddlEfficacy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker DT2;
        private System.Windows.Forms.DateTimePicker DT1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dGrid1;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private Filtros filtros1;
        private System.Windows.Forms.Label lbMSG;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ddlEfficacyOwner;
    }
}