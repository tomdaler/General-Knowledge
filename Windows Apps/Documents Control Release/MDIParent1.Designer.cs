namespace GPS
{
    partial class MDIParent1
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.codesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contactsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.departmentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.responsibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.organizationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.efficacyOwnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.coveragesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listDocumentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byEfficacyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byContactToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byOwnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byEfficacyOwnerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byResponsibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byNextReviewDateToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.byNextReviewDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCreationDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byRetirementDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.summaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateReviewDateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateContributorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.version10ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.efficacyDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 709);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.Size = new System.Drawing.Size(1357, 26);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(49, 20);
            this.toolStripStatusLabel.Text = "Status";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codesToolStripMenuItem,
            this.documentsToolStripMenuItem,
            this.importToolStripMenuItem,
            this.version10ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 24);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1357, 28);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // codesToolStripMenuItem
            // 
            this.codesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contactsToolStripMenuItem,
            this.departmentsToolStripMenuItem,
            this.responsibleToolStripMenuItem,
            this.organizationsToolStripMenuItem,
            this.efficacyOwnerToolStripMenuItem,
            this.documentTypesToolStripMenuItem,
            this.coveragesToolStripMenuItem});
            this.codesToolStripMenuItem.Name = "codesToolStripMenuItem";
            this.codesToolStripMenuItem.Size = new System.Drawing.Size(64, 24);
            this.codesToolStripMenuItem.Text = "Codes";
            // 
            // contactsToolStripMenuItem
            // 
            this.contactsToolStripMenuItem.Name = "contactsToolStripMenuItem";
            this.contactsToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.contactsToolStripMenuItem.Text = "Contacts";
            this.contactsToolStripMenuItem.Click += new System.EventHandler(this.contactsToolStripMenuItem_Click);
            // 
            // departmentsToolStripMenuItem
            // 
            this.departmentsToolStripMenuItem.Name = "departmentsToolStripMenuItem";
            this.departmentsToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.departmentsToolStripMenuItem.Text = "Departments";
            this.departmentsToolStripMenuItem.Click += new System.EventHandler(this.departmentsToolStripMenuItem_Click);
            // 
            // responsibleToolStripMenuItem
            // 
            this.responsibleToolStripMenuItem.Name = "responsibleToolStripMenuItem";
            this.responsibleToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.responsibleToolStripMenuItem.Text = "Responsible Groups";
            this.responsibleToolStripMenuItem.Click += new System.EventHandler(this.responsibleToolStripMenuItem_Click);
            // 
            // organizationsToolStripMenuItem
            // 
            this.organizationsToolStripMenuItem.Name = "organizationsToolStripMenuItem";
            this.organizationsToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.organizationsToolStripMenuItem.Text = "Organizations";
            this.organizationsToolStripMenuItem.Click += new System.EventHandler(this.organizationsToolStripMenuItem_Click);
            // 
            // efficacyOwnerToolStripMenuItem
            // 
            this.efficacyOwnerToolStripMenuItem.Name = "efficacyOwnerToolStripMenuItem";
            this.efficacyOwnerToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.efficacyOwnerToolStripMenuItem.Text = "Efficacy Owner";
            this.efficacyOwnerToolStripMenuItem.Click += new System.EventHandler(this.efficacyOwnerToolStripMenuItem_Click);
            // 
            // documentTypesToolStripMenuItem
            // 
            this.documentTypesToolStripMenuItem.Name = "documentTypesToolStripMenuItem";
            this.documentTypesToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.documentTypesToolStripMenuItem.Text = "Document Types";
            this.documentTypesToolStripMenuItem.Click += new System.EventHandler(this.documentTypesToolStripMenuItem_Click);
            // 
            // coveragesToolStripMenuItem
            // 
            this.coveragesToolStripMenuItem.Name = "coveragesToolStripMenuItem";
            this.coveragesToolStripMenuItem.Size = new System.Drawing.Size(223, 26);
            this.coveragesToolStripMenuItem.Text = "Coverages";
            this.coveragesToolStripMenuItem.Click += new System.EventHandler(this.coveragesToolStripMenuItem_Click);
            // 
            // documentsToolStripMenuItem
            // 
            this.documentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createDocumentToolStripMenuItem,
            this.searchDocumentToolStripMenuItem,
            this.listDocumentsToolStripMenuItem});
            this.documentsToolStripMenuItem.Name = "documentsToolStripMenuItem";
            this.documentsToolStripMenuItem.Size = new System.Drawing.Size(98, 24);
            this.documentsToolStripMenuItem.Text = "Documents";
            this.documentsToolStripMenuItem.Click += new System.EventHandler(this.documentsToolStripMenuItem_Click);
            // 
            // createDocumentToolStripMenuItem
            // 
            this.createDocumentToolStripMenuItem.Name = "createDocumentToolStripMenuItem";
            this.createDocumentToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.createDocumentToolStripMenuItem.Text = "Create Document";
            this.createDocumentToolStripMenuItem.Click += new System.EventHandler(this.createDocumentToolStripMenuItem_Click);
            // 
            // searchDocumentToolStripMenuItem
            // 
            this.searchDocumentToolStripMenuItem.Name = "searchDocumentToolStripMenuItem";
            this.searchDocumentToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.searchDocumentToolStripMenuItem.Text = "Update Document";
            this.searchDocumentToolStripMenuItem.Click += new System.EventHandler(this.searchDocumentToolStripMenuItem_Click);
            // 
            // listDocumentsToolStripMenuItem
            // 
            this.listDocumentsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byStatusToolStripMenuItem,
            this.byEfficacyToolStripMenuItem,
            this.byContactToolStripMenuItem,
            this.byOwnerToolStripMenuItem,
            this.byEfficacyOwnerToolStripMenuItem,
            this.byResponsibleToolStripMenuItem,
            this.byNextReviewDateToolStripMenuItem1,
            this.byNextReviewDateToolStripMenuItem,
            this.byCreationDateToolStripMenuItem,
            this.byRetirementDateToolStripMenuItem,
            this.summaryToolStripMenuItem,
            this.efficacyDetailsToolStripMenuItem});
            this.listDocumentsToolStripMenuItem.Name = "listDocumentsToolStripMenuItem";
            this.listDocumentsToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.listDocumentsToolStripMenuItem.Text = "List Documents";
            // 
            // byStatusToolStripMenuItem
            // 
            this.byStatusToolStripMenuItem.Name = "byStatusToolStripMenuItem";
            this.byStatusToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byStatusToolStripMenuItem.Text = "By Status";
            this.byStatusToolStripMenuItem.Click += new System.EventHandler(this.byStatusToolStripMenuItem_Click);
            // 
            // byEfficacyToolStripMenuItem
            // 
            this.byEfficacyToolStripMenuItem.Name = "byEfficacyToolStripMenuItem";
            this.byEfficacyToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byEfficacyToolStripMenuItem.Text = "By Efficacy";
            this.byEfficacyToolStripMenuItem.Click += new System.EventHandler(this.byEfficacyToolStripMenuItem_Click);
            // 
            // byContactToolStripMenuItem
            // 
            this.byContactToolStripMenuItem.Name = "byContactToolStripMenuItem";
            this.byContactToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byContactToolStripMenuItem.Text = "By Contributor";
            this.byContactToolStripMenuItem.Click += new System.EventHandler(this.byContactToolStripMenuItem_Click);
            // 
            // byOwnerToolStripMenuItem
            // 
            this.byOwnerToolStripMenuItem.Name = "byOwnerToolStripMenuItem";
            this.byOwnerToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byOwnerToolStripMenuItem.Text = "By Document Owner";
            this.byOwnerToolStripMenuItem.Click += new System.EventHandler(this.byOwnerToolStripMenuItem_Click);
            // 
            // byEfficacyOwnerToolStripMenuItem
            // 
            this.byEfficacyOwnerToolStripMenuItem.Name = "byEfficacyOwnerToolStripMenuItem";
            this.byEfficacyOwnerToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byEfficacyOwnerToolStripMenuItem.Text = "By Efficacy Owner";
            this.byEfficacyOwnerToolStripMenuItem.Click += new System.EventHandler(this.byEfficacyOwnerToolStripMenuItem_Click);
            // 
            // byResponsibleToolStripMenuItem
            // 
            this.byResponsibleToolStripMenuItem.Name = "byResponsibleToolStripMenuItem";
            this.byResponsibleToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byResponsibleToolStripMenuItem.Text = "By Responsible";
            this.byResponsibleToolStripMenuItem.Click += new System.EventHandler(this.byResponsibleToolStripMenuItem_Click);
            // 
            // byNextReviewDateToolStripMenuItem1
            // 
            this.byNextReviewDateToolStripMenuItem1.Name = "byNextReviewDateToolStripMenuItem1";
            this.byNextReviewDateToolStripMenuItem1.Size = new System.Drawing.Size(230, 26);
            this.byNextReviewDateToolStripMenuItem1.Text = "By Next Review Date";
            this.byNextReviewDateToolStripMenuItem1.Click += new System.EventHandler(this.byNextReviewDateToolStripMenuItem1_Click);
            // 
            // byNextReviewDateToolStripMenuItem
            // 
            this.byNextReviewDateToolStripMenuItem.Name = "byNextReviewDateToolStripMenuItem";
            this.byNextReviewDateToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byNextReviewDateToolStripMenuItem.Text = "By Complete Date";
            this.byNextReviewDateToolStripMenuItem.Click += new System.EventHandler(this.byNextReviewDateToolStripMenuItem_Click);
            // 
            // byCreationDateToolStripMenuItem
            // 
            this.byCreationDateToolStripMenuItem.Name = "byCreationDateToolStripMenuItem";
            this.byCreationDateToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byCreationDateToolStripMenuItem.Text = "By Creation Date";
            this.byCreationDateToolStripMenuItem.Click += new System.EventHandler(this.byCreationDateToolStripMenuItem_Click);
            // 
            // byRetirementDateToolStripMenuItem
            // 
            this.byRetirementDateToolStripMenuItem.Name = "byRetirementDateToolStripMenuItem";
            this.byRetirementDateToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.byRetirementDateToolStripMenuItem.Text = "By Retire Date";
            this.byRetirementDateToolStripMenuItem.Click += new System.EventHandler(this.byRetirementDateToolStripMenuItem_Click);
            // 
            // summaryToolStripMenuItem
            // 
            this.summaryToolStripMenuItem.Name = "summaryToolStripMenuItem";
            this.summaryToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.summaryToolStripMenuItem.Text = "Summary";
            this.summaryToolStripMenuItem.Click += new System.EventHandler(this.summaryToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importDataToolStripMenuItem,
            this.updateReviewDateToolStripMenuItem,
            this.updateContributorToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.importToolStripMenuItem.Text = "Import Data";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // importDataToolStripMenuItem
            // 
            this.importDataToolStripMenuItem.Name = "importDataToolStripMenuItem";
            this.importDataToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.importDataToolStripMenuItem.Text = "Import Data";
            this.importDataToolStripMenuItem.Click += new System.EventHandler(this.importDataToolStripMenuItem_Click);
            // 
            // updateReviewDateToolStripMenuItem
            // 
            this.updateReviewDateToolStripMenuItem.Name = "updateReviewDateToolStripMenuItem";
            this.updateReviewDateToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.updateReviewDateToolStripMenuItem.Text = "Update Review  Date";
            this.updateReviewDateToolStripMenuItem.Click += new System.EventHandler(this.updateReviewDateToolStripMenuItem_Click);
            // 
            // updateContributorToolStripMenuItem
            // 
            this.updateContributorToolStripMenuItem.Name = "updateContributorToolStripMenuItem";
            this.updateContributorToolStripMenuItem.Size = new System.Drawing.Size(232, 26);
            this.updateContributorToolStripMenuItem.Text = "Update Contributor";
            this.updateContributorToolStripMenuItem.Click += new System.EventHandler(this.updateContributorToolStripMenuItem_Click);
            // 
            // version10ToolStripMenuItem
            // 
            this.version10ToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.version10ToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.version10ToolStripMenuItem.Name = "version10ToolStripMenuItem";
            this.version10ToolStripMenuItem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.version10ToolStripMenuItem.RightToLeftAutoMirrorImage = true;
            this.version10ToolStripMenuItem.Size = new System.Drawing.Size(101, 24);
            this.version10ToolStripMenuItem.Text = "Version 4.0";
            this.version10ToolStripMenuItem.Click += new System.EventHandler(this.version10ToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1357, 24);
            this.menuStrip2.TabIndex = 6;
            this.menuStrip2.Text = "menuStrip2";
            this.menuStrip2.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip2_ItemClicked);
            // 
            // efficacyDetailsToolStripMenuItem
            // 
            this.efficacyDetailsToolStripMenuItem.Name = "efficacyDetailsToolStripMenuItem";
            this.efficacyDetailsToolStripMenuItem.Size = new System.Drawing.Size(230, 26);
            this.efficacyDetailsToolStripMenuItem.Text = "Efficacy - Details";
            this.efficacyDetailsToolStripMenuItem.Click += new System.EventHandler(this.efficacyDetailsToolStripMenuItem_Click);
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 735);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip2;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "MDIParent1";
            this.Text = "G P S";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem codesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem contactsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem departmentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem responsibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem organizationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listDocumentsToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem byStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byEfficacyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byContactToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byOwnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byResponsibleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNextReviewDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byNextReviewDateToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem byCreationDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem efficacyOwnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentTypesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem coveragesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byEfficacyOwnerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem version10ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byRetirementDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem summaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateReviewDateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateContributorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem efficacyDetailsToolStripMenuItem;
    }
}



