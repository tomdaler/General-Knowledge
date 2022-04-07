namespace GPS.Codes
{
    partial class Coverages
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
            this.components = new System.ComponentModel.Container();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.dtCoverages = new GPS.Codes.dtCoverages();
            this.tsCoverageBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.tsCoverageTableAdapter = new GPS.Codes.dtCoveragesTableAdapters.tsCoverageTableAdapter();
            this.coverageCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.coverageDescrDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCoverages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsCoverageBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(521, 194);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 48);
            this.button2.TabIndex = 8;
            this.button2.Text = "Update";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(521, 77);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 48);
            this.button1.TabIndex = 7;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.coverageCodeDataGridViewTextBoxColumn,
            this.coverageDescrDataGridViewTextBoxColumn});
            this.dataGridView1.DataSource = this.tsCoverageBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(74, 72);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(325, 496);
            this.dataGridView1.TabIndex = 6;
            // 
            // dtCoverages
            // 
            this.dtCoverages.DataSetName = "dtCoverages";
            this.dtCoverages.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsCoverageBindingSource
            // 
            this.tsCoverageBindingSource.DataMember = "tsCoverage";
            this.tsCoverageBindingSource.DataSource = this.dtCoverages;
            // 
            // tsCoverageTableAdapter
            // 
            this.tsCoverageTableAdapter.ClearBeforeFill = true;
            // 
            // coverageCodeDataGridViewTextBoxColumn
            // 
            this.coverageCodeDataGridViewTextBoxColumn.DataPropertyName = "CoverageCode";
            this.coverageCodeDataGridViewTextBoxColumn.HeaderText = "Code";
            this.coverageCodeDataGridViewTextBoxColumn.Name = "coverageCodeDataGridViewTextBoxColumn";
            this.coverageCodeDataGridViewTextBoxColumn.Width = 50;
            // 
            // coverageDescrDataGridViewTextBoxColumn
            // 
            this.coverageDescrDataGridViewTextBoxColumn.DataPropertyName = "CoverageDescr";
            this.coverageDescrDataGridViewTextBoxColumn.HeaderText = "Coverage";
            this.coverageDescrDataGridViewTextBoxColumn.Name = "coverageDescrDataGridViewTextBoxColumn";
            this.coverageDescrDataGridViewTextBoxColumn.Width = 150;
            // 
            // Coverages
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1372, 737);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Coverages";
            this.Text = "COVERAGES";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Coverages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtCoverages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tsCoverageBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private dtCoverages dtCoverages;
        private System.Windows.Forms.BindingSource tsCoverageBindingSource;
        private dtCoveragesTableAdapters.tsCoverageTableAdapter tsCoverageTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn coverageCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn coverageDescrDataGridViewTextBoxColumn;
    }
}