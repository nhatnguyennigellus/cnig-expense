namespace NigelFinanceManage
{
    partial class AddATM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddATM));
            this.cbACB = new System.Windows.Forms.CheckBox();
            this.txtATM = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvACBATM = new System.Windows.Forms.ListView();
            this.lvOtherATM = new System.Windows.Forms.ListView();
            this.btnATMAdd = new System.Windows.Forms.Button();
            this.btnATMDelete = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.sttATM = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbBank = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbACB
            // 
            this.cbACB.AutoSize = true;
            this.cbACB.Location = new System.Drawing.Point(277, 21);
            this.cbACB.Name = "cbACB";
            this.cbACB.Size = new System.Drawing.Size(57, 19);
            this.cbACB.TabIndex = 0;
            this.cbACB.Text = "Other";
            this.cbACB.UseVisualStyleBackColor = true;
            this.cbACB.CheckedChanged += new System.EventHandler(this.cbACB_CheckedChanged);
            // 
            // txtATM
            // 
            this.txtATM.Location = new System.Drawing.Point(62, 17);
            this.txtATM.Name = "txtATM";
            this.txtATM.Size = new System.Drawing.Size(209, 23);
            this.txtATM.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "ATM";
            // 
            // lvACBATM
            // 
            this.lvACBATM.Location = new System.Drawing.Point(18, 101);
            this.lvACBATM.MultiSelect = false;
            this.lvACBATM.Name = "lvACBATM";
            this.lvACBATM.Size = new System.Drawing.Size(235, 162);
            this.lvACBATM.TabIndex = 3;
            this.lvACBATM.UseCompatibleStateImageBehavior = false;
            this.lvACBATM.View = System.Windows.Forms.View.List;
            this.lvACBATM.SelectedIndexChanged += new System.EventHandler(this.lvACBATM_SelectedIndexChanged);
            // 
            // lvOtherATM
            // 
            this.lvOtherATM.Location = new System.Drawing.Point(259, 101);
            this.lvOtherATM.MultiSelect = false;
            this.lvOtherATM.Name = "lvOtherATM";
            this.lvOtherATM.Size = new System.Drawing.Size(240, 162);
            this.lvOtherATM.TabIndex = 4;
            this.lvOtherATM.UseCompatibleStateImageBehavior = false;
            this.lvOtherATM.View = System.Windows.Forms.View.List;
            this.lvOtherATM.SelectedIndexChanged += new System.EventHandler(this.lvOtherATM_SelectedIndexChanged);
            // 
            // btnATMAdd
            // 
            this.btnATMAdd.Location = new System.Drawing.Point(345, 18);
            this.btnATMAdd.Name = "btnATMAdd";
            this.btnATMAdd.Size = new System.Drawing.Size(45, 23);
            this.btnATMAdd.TabIndex = 5;
            this.btnATMAdd.Text = "Add";
            this.btnATMAdd.UseVisualStyleBackColor = true;
            this.btnATMAdd.Click += new System.EventHandler(this.btnATMAdd_Click);
            // 
            // btnATMDelete
            // 
            this.btnATMDelete.Location = new System.Drawing.Point(393, 18);
            this.btnATMDelete.Name = "btnATMDelete";
            this.btnATMDelete.Size = new System.Drawing.Size(63, 23);
            this.btnATMDelete.TabIndex = 6;
            this.btnATMDelete.Text = "Remove";
            this.btnATMDelete.UseVisualStyleBackColor = true;
            this.btnATMDelete.Click += new System.EventHandler(this.btnATMDelete_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sttATM});
            this.statusStrip1.Location = new System.Drawing.Point(0, 300);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(539, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sttATM
            // 
            this.sttATM.Name = "sttATM";
            this.sttATM.Size = new System.Drawing.Size(118, 17);
            this.sttATM.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbBank);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnATMDelete);
            this.panel1.Controls.Add(this.cbACB);
            this.panel1.Controls.Add(this.txtATM);
            this.panel1.Controls.Add(this.lvOtherATM);
            this.panel1.Controls.Add(this.lvACBATM);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnATMAdd);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(515, 275);
            this.panel1.TabIndex = 9;
            // 
            // lbBank
            // 
            this.lbBank.AutoSize = true;
            this.lbBank.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBank.Location = new System.Drawing.Point(58, 50);
            this.lbBank.Name = "lbBank";
            this.lbBank.Size = new System.Drawing.Size(59, 23);
            this.lbBank.TabIndex = 10;
            this.lbBank.Text = "label6";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 15);
            this.label2.TabIndex = 9;
            this.label2.Text = "Bank";
            // 
            // AddATM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 322);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddATM";
            this.Text = "ATM";
            this.Load += new System.EventHandler(this.AddATM_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbACB;
        private System.Windows.Forms.TextBox txtATM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvACBATM;
        private System.Windows.Forms.ListView lvOtherATM;
        private System.Windows.Forms.Button btnATMAdd;
        private System.Windows.Forms.Button btnATMDelete;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sttATM;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbBank;
    }
}