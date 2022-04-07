namespace GPS.List
{
    partial class EffOwner
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
            this.dGrid1 = new System.Windows.Forms.DataGridView();
            this.ddlOwner = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.lbMSG = new System.Windows.Forms.Label();
            this.filtros1 = new GPS.List.Filtros();
            this.ddlStatus = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.CK1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DT2 = new System.Windows.Forms.DateTimePicker();
            this.DT1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dGrid1
            // 
            this.dGrid1.AllowUserToAddRows = false;
            this.dGrid1.AllowUserToDeleteRows = false;
            this.dGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid1.Location = new System.Drawing.Point(40, 289);
            this.dGrid1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dGrid1.Name = "dGrid1";
            this.dGrid1.RowHeadersWidth = 51;
            this.dGrid1.Size = new System.Drawing.Size(1584, 385);
            this.dGrid1.TabIndex = 88;
            this.dGrid1.Visible = false;
            // 
            // ddlOwner
            // 
            this.ddlOwner.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlOwner.FormattingEnabled = true;
            this.ddlOwner.Location = new System.Drawing.Point(229, 172);
            this.ddlOwner.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ddlOwner.Name = "ddlOwner";
            this.ddlOwner.Size = new System.Drawing.Size(328, 28);
            this.ddlOwner.TabIndex = 87;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 172);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 20);
            this.label3.TabIndex = 86;
            this.label3.Text = "Eff. Owner";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(1140, 172);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 49);
            this.button2.TabIndex = 85;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(960, 172);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 49);
            this.button1.TabIndex = 84;
            this.button1.Text = "Search";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(583, 11);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(289, 29);
            this.label4.TabIndex = 83;
            this.label4.Text = "SEARCH DOCUMENTS";
            // 
            // lbMSG
            // 
            this.lbMSG.AutoSize = true;
            this.lbMSG.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMSG.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbMSG.Location = new System.Drawing.Point(55, 252);
            this.lbMSG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMSG.Name = "lbMSG";
            this.lbMSG.Size = new System.Drawing.Size(435, 20);
            this.lbMSG.TabIndex = 150;
            this.lbMSG.Text = "Review all the information by double clicking a row";
            this.lbMSG.Visible = false;
            // 
            // filtros1
            // 
            this.filtros1.coverage = "0";
            this.filtros1.dpto = "0";
            this.filtros1.Location = new System.Drawing.Point(16, 15);
            this.filtros1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.filtros1.Name = "filtros1";
            this.filtros1.orgs = "0";
            this.filtros1.Size = new System.Drawing.Size(1337, 118);
            this.filtros1.TabIndex = 89;
            this.filtros1.textoIni = null;
            this.filtros1.types = "0";
            this.filtros1.Load += new System.EventHandler(this.filtros1_Load);
            // 
            // ddlStatus
            // 
            this.ddlStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ddlStatus.FormattingEnabled = true;
            this.ddlStatus.Location = new System.Drawing.Point(224, 212);
            this.ddlStatus.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ddlStatus.Name = "ddlStatus";
            this.ddlStatus.Size = new System.Drawing.Size(236, 28);
            this.ddlStatus.TabIndex = 156;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(55, 215);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 20);
            this.label5.TabIndex = 155;
            this.label5.Text = "Status";
            // 
            // CK1
            // 
            this.CK1.AutoSize = true;
            this.CK1.Location = new System.Drawing.Point(653, 164);
            this.CK1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.CK1.Name = "CK1";
            this.CK1.Size = new System.Drawing.Size(52, 20);
            this.CK1.TabIndex = 157;
            this.CK1.Text = "ALL";
            this.CK1.UseVisualStyleBackColor = true;
            this.CK1.CheckedChanged += new System.EventHandler(this.CK1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DT2);
            this.panel1.Controls.Add(this.DT1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(633, 192);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 90);
            this.panel1.TabIndex = 158;
            // 
            // DT2
            // 
            this.DT2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT2.Location = new System.Drawing.Point(173, 53);
            this.DT2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DT2.Name = "DT2";
            this.DT2.Size = new System.Drawing.Size(131, 26);
            this.DT2.TabIndex = 158;
            this.DT2.ValueChanged += new System.EventHandler(this.DT2_ValueChanged);
            // 
            // DT1
            // 
            this.DT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT1.Location = new System.Drawing.Point(172, 10);
            this.DT1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.DT1.Name = "DT1";
            this.DT1.Size = new System.Drawing.Size(132, 26);
            this.DT1.TabIndex = 157;
            this.DT1.ValueChanged += new System.EventHandler(this.DT1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 53);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 20);
            this.label2.TabIndex = 156;
            this.label2.Text = "To";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 20);
            this.label1.TabIndex = 155;
            this.label1.Text = "Assigned From";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // EffOwner
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1733, 745);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.CK1);
            this.Controls.Add(this.ddlStatus);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lbMSG);
            this.Controls.Add(this.filtros1);
            this.Controls.Add(this.dGrid1);
            this.Controls.Add(this.ddlOwner);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "EffOwner";
            this.Text = "EFFICACY OWNER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.EffOwner_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Filtros filtros1;
        private System.Windows.Forms.DataGridView dGrid1;
        private System.Windows.Forms.ComboBox ddlOwner;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lbMSG;
        private System.Windows.Forms.ComboBox ddlStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox CK1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker DT2;
        private System.Windows.Forms.DateTimePicker DT1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}