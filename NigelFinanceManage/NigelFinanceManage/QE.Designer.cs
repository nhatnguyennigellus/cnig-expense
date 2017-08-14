namespace NigelFinanceManage
{
    partial class QE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QE));
            this.cbQEType = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQEDesc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.btnQERemove = new System.Windows.Forms.Button();
            this.btnQEViewAll = new System.Windows.Forms.Button();
            this.btnQEModify = new System.Windows.Forms.Button();
            this.btnQEAdd = new System.Windows.Forms.Button();
            this.dgvQE = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sttQE = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQE)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbQEType
            // 
            this.cbQEType.FormattingEnabled = true;
            this.cbQEType.Items.AddRange(new object[] {
            "Income",
            "Payment",
            "Plan",
            "Withdrawal"});
            this.cbQEType.Location = new System.Drawing.Point(103, 44);
            this.cbQEType.MaxDropDownItems = 50;
            this.cbQEType.Name = "cbQEType";
            this.cbQEType.Size = new System.Drawing.Size(140, 23);
            this.cbQEType.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 15);
            this.label5.TabIndex = 5;
            this.label5.Text = "Description";
            // 
            // txtQEDesc
            // 
            this.txtQEDesc.Location = new System.Drawing.Point(103, 14);
            this.txtQEDesc.Name = "txtQEDesc";
            this.txtQEDesc.Size = new System.Drawing.Size(363, 23);
            this.txtQEDesc.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Type";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panel5.Controls.Add(this.btnQERemove);
            this.panel5.Controls.Add(this.btnQEViewAll);
            this.panel5.Controls.Add(this.btnQEModify);
            this.panel5.Controls.Add(this.btnQEAdd);
            this.panel5.Location = new System.Drawing.Point(3, 76);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(582, 39);
            this.panel5.TabIndex = 16;
            // 
            // btnQERemove
            // 
            this.btnQERemove.Enabled = false;
            this.btnQERemove.Location = new System.Drawing.Point(278, 6);
            this.btnQERemove.Name = "btnQERemove";
            this.btnQERemove.Size = new System.Drawing.Size(87, 27);
            this.btnQERemove.TabIndex = 3;
            this.btnQERemove.Text = "Remove";
            this.btnQERemove.UseVisualStyleBackColor = true;
            this.btnQERemove.Click += new System.EventHandler(this.btnQERemove_Click);
            // 
            // btnQEViewAll
            // 
            this.btnQEViewAll.Location = new System.Drawing.Point(6, 6);
            this.btnQEViewAll.Name = "btnQEViewAll";
            this.btnQEViewAll.Size = new System.Drawing.Size(87, 27);
            this.btnQEViewAll.TabIndex = 0;
            this.btnQEViewAll.Text = "View All";
            this.btnQEViewAll.UseVisualStyleBackColor = true;
            this.btnQEViewAll.Click += new System.EventHandler(this.btnQEViewAll_Click);
            // 
            // btnQEModify
            // 
            this.btnQEModify.Enabled = false;
            this.btnQEModify.Location = new System.Drawing.Point(187, 6);
            this.btnQEModify.Name = "btnQEModify";
            this.btnQEModify.Size = new System.Drawing.Size(87, 27);
            this.btnQEModify.TabIndex = 2;
            this.btnQEModify.Text = "Modify";
            this.btnQEModify.UseVisualStyleBackColor = true;
            this.btnQEModify.Click += new System.EventHandler(this.btnQEModify_Click);
            // 
            // btnQEAdd
            // 
            this.btnQEAdd.Location = new System.Drawing.Point(96, 6);
            this.btnQEAdd.Name = "btnQEAdd";
            this.btnQEAdd.Size = new System.Drawing.Size(87, 27);
            this.btnQEAdd.TabIndex = 1;
            this.btnQEAdd.Text = "Add";
            this.btnQEAdd.UseVisualStyleBackColor = true;
            this.btnQEAdd.Click += new System.EventHandler(this.btnQEAdd_Click);
            // 
            // dgvQE
            // 
            this.dgvQE.AllowUserToAddRows = false;
            this.dgvQE.AllowUserToDeleteRows = false;
            this.dgvQE.AllowUserToResizeRows = false;
            this.dgvQE.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvQE.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQE.Location = new System.Drawing.Point(3, 122);
            this.dgvQE.MultiSelect = false;
            this.dgvQE.Name = "dgvQE";
            this.dgvQE.ReadOnly = true;
            this.dgvQE.RowHeadersVisible = false;
            this.dgvQE.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvQE.Size = new System.Drawing.Size(582, 188);
            this.dgvQE.TabIndex = 17;
            this.dgvQE.SelectionChanged += new System.EventHandler(this.dgvQE_SelectionChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sttQE});
            this.statusStrip1.Location = new System.Drawing.Point(0, 317);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(588, 22);
            this.statusStrip1.TabIndex = 18;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sttQE
            // 
            this.sttQE.Name = "sttQE";
            this.sttQE.Size = new System.Drawing.Size(0, 17);
            // 
            // QE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 339);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvQE);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbQEType);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtQEDesc);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "QE";
            this.Text = "Quick Entry";
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQE)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbQEType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtQEDesc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btnQERemove;
        private System.Windows.Forms.Button btnQEViewAll;
        private System.Windows.Forms.Button btnQEModify;
        private System.Windows.Forms.Button btnQEAdd;
        private System.Windows.Forms.DataGridView dgvQE;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sttQE;
    }
}