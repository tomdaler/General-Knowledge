namespace GPS.List
{
    partial class Summarycs
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
            this.DT2 = new System.Windows.Forms.DateTimePicker();
            this.DT1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.dGrid1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).BeginInit();
            this.SuspendLayout();
            // 
            // DT2
            // 
            this.DT2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT2.Location = new System.Drawing.Point(174, 65);
            this.DT2.Name = "DT2";
            this.DT2.Size = new System.Drawing.Size(99, 22);
            this.DT2.TabIndex = 149;
            // 
            // DT1
            // 
            this.DT1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DT1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DT1.Location = new System.Drawing.Point(173, 30);
            this.DT1.Name = "DT1";
            this.DT1.Size = new System.Drawing.Size(100, 22);
            this.DT1.TabIndex = 148;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(106, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 16);
            this.label2.TabIndex = 147;
            this.label2.Text = "To";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(106, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 16);
            this.label1.TabIndex = 146;
            this.label1.Text = "From";
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(435, 31);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 40);
            this.button2.TabIndex = 152;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(317, 31);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(112, 40);
            this.button3.TabIndex = 151;
            this.button3.Text = "Search";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // dGrid1
            // 
            this.dGrid1.AllowUserToAddRows = false;
            this.dGrid1.AllowUserToDeleteRows = false;
            this.dGrid1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGrid1.Location = new System.Drawing.Point(109, 115);
            this.dGrid1.Name = "dGrid1";
            this.dGrid1.Size = new System.Drawing.Size(496, 269);
            this.dGrid1.TabIndex = 153;
            this.dGrid1.Visible = false;
            // 
            // Summarycs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 739);
            this.Controls.Add(this.dGrid1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.DT2);
            this.Controls.Add(this.DT1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Summarycs";
            this.Text = "Summarycs";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dGrid1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker DT2;
        private System.Windows.Forms.DateTimePicker DT1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.DataGridView dGrid1;
    }
}