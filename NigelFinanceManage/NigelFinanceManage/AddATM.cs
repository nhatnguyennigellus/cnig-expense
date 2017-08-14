using NigelFinanceManage.Data;
using NigelFinanceManage.Entity;
using NigelFinanceManage.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NigelFinanceManage
{
    public partial class AddATM : Form
    {
        Account account = new Account();
        DiaryService service;
        AdminService admin;
        Main main;
        public AddATM()
        {
            InitializeComponent();
        }

        public AddATM(DiaryService service, AdminService admin, Account account, Main main)
        {
            InitializeComponent();
            this.account = account;
            this.service = service;
            this.admin = admin;
            this.main = main;

            lbBank.Text = account.Bank;
        }

        private void AddATM_Load(object sender, EventArgs e)
        {
            loadATMList();
        }

        private void loadATMList()
        {
            List<String> listATMACB = service.getATMListByBank(lbBank.Text);
            lvOtherATM.Clear();
            lvACBATM.Clear();
            foreach (String item in listATMACB)
            {
                lvACBATM.Items.Add(item);
            }
            List<String> listATMOther = service.getOtherATMList(account);
            foreach (String item in listATMOther)
            {
                lvOtherATM.Items.Add(item);
            }
        }

        private void btnATMAdd_Click(object sender, EventArgs e)
        {
            // Validation
            if (txtATM.Text == "")
            {
                errorMessage(ErrorCodes.e0004);
                txtATM.Focus();
                return;
            }
            // Add
            bool isOK = false;
            if (cbACB.Checked)
            {
                isOK = service.addATM(txtATM.Text, lbBank.Text);
            }
            else
            {
                isOK = service.addATM(txtATM.Text, account);
            }
            if (isOK)
            {
                successMessage(ErrorCodes.m0003);
                loadATMList();
                txtATM.Text = "";
                main.loadATMACB();
                main.loadATMOther();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnATMDelete_Click(object sender, EventArgs e)
        {
            // Validation
            if (txtATM.Text == "")
            {
                errorMessage(ErrorCodes.e0004);
                txtATM.Focus();
                return;
            }
            // Remove
            bool isOK = false;
            if (cbACB.Checked)
            {
                isOK = service.removeATM(txtATM.Text, lbBank.Text);
            }
            else
            {
                isOK = service.removeATM(txtATM.Text, account);
            }
            if (isOK)
            {
                successMessage(ErrorCodes.m0023);
                loadATMList();
                txtATM.Text = "";
                main.loadATMACB();
                main.loadATMOther();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void successMessage(string message)
        {
            sttATM.Text = admin.getError(message);
            sttATM.ForeColor = Color.Green;
            statusStrip1.Refresh();
        }

        private void errorMessage(string message)
        {
            sttATM.Text = admin.getError(message);
            sttATM.ForeColor = Color.DarkRed;
            statusStrip1.Refresh();

        }

        private void lvOtherATM_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbACB.Checked = false;
            txtATM.Text = lvOtherATM.SelectedItems.Count > 0 ? lvOtherATM.SelectedItems[0].Text : "";
        }

        private void lvACBATM_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbACB.Checked = true;
            txtATM.Text = lvACBATM.SelectedItems.Count > 0 ? lvACBATM.SelectedItems[0].Text : "";
        }

        private void cbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadATMACB();
        }

        internal void loadATMACB()
        {
            if (cbACB.Checked)
            {
                lvACBATM.Items.Clear();
                foreach (string atm in service.getATMListByBank(lbBank.Text))
                {
                    lvACBATM.Items.Add(atm);
                }
            }
        }

        private void cbACB_CheckedChanged(object sender, EventArgs e)
        {
            if (cbACB.Checked)
            {
                cbACB.Text = account.Bank;
            }
            else
            {
                cbACB.Text = "Others";
            }
        }
    }
}
