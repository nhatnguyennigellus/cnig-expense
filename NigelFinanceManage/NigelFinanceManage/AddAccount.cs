using NigelFinanceManage.DAO;
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
    public partial class AddAccount : Form
    {
        DiaryService service;
        AdminService admin;
        int database = -1;
        Login frmLogin;
        public AddAccount()
        {
            InitializeComponent();
            resetFields();
        }

        public AddAccount(Login frmLogin, DiaryService service, AdminService admin, int database)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
            this.service = service;
            this.admin = admin;
            frmLogin.Hide();
            resetFields();
            this.database = database;
            service.chooseDatabase(database);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetFields();
        }

        private void resetFields()
        {
            txtUsername.Clear();
            txtName.Clear();
            txtPassword.Clear();
            txtCfmPass.Clear();
            txtCash.Clear();
            txtBank.Clear();
            txtBal.Clear();
            txtInFee.Clear();
            txtOutFee.Clear();
            txtUsername.Clear();
            statusStrip1.Refresh();
            cbCurrency.SelectedIndex = 0;
        }

        private void errorMessage(string message)
        {
            sttAcc.Text = admin.getError(message); ;
            sttAcc.ForeColor = Color.DarkRed;
            statusStrip1.Refresh();
        }

        private void successMessage(string message)
        {
            sttAcc.Text = admin.getError(message); ;
            sttAcc.ForeColor = Color.Green;
            statusStrip1.Refresh();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Validation
            if (txtUsername.Text == "")
            {
                errorMessage(ErrorCodes.e0029);
                txtUsername.Focus();
                return;
            }

            if (service.existedAccount(txtUsername.Text))
            {
                errorMessage(ErrorCodes.e0013);
                txtUsername.Focus();
                return;
            }

            if (txtName.Text == "")
            {
                errorMessage(ErrorCodes.e0020);
                txtName.Focus();
                return;
            }

            if (txtPassword.Text == "")
            {
                errorMessage(ErrorCodes.e0026);
                txtUsername.Focus();
                return;
            }

            if (txtPassword.Text != txtCfmPass.Text)
            {
                errorMessage(ErrorCodes.e0027);
                txtCfmPass.Focus();
                return;
            }

            if (txtBank.Text == "")
            {
                errorMessage(ErrorCodes.e0006);
                txtBank.Focus();
                return;
            }

            if (txtCash.Text == "")
            {
                errorMessage(ErrorCodes.e0008);
                txtCash.Focus();
                return;
            }

            if (txtBal.Text == "")
            {
                errorMessage(ErrorCodes.e0005);
                txtBal.Focus();
                return;
            }

            if (txtInFee.Text == "")
            {
                errorMessage(ErrorCodes.e0015);
                txtInFee.Focus();
                return;
            }

            if (txtOutFee.Text == "")
            {
                errorMessage(ErrorCodes.e0024);
                txtOutFee.Focus();
                return;
            }

            int cash = 0;
            int bal = 0;
            int inner = 0;
            int outer = 0;
            try
            {
                cash = int.Parse(txtCash.Text);
                bal = int.Parse(txtBal.Text);
                inner = int.Parse(txtInFee.Text);
                outer = int.Parse(txtOutFee.Text);

                if (cash < 0)
                {
                    errorMessage(ErrorCodes.e0018);
                    txtCash.Focus();
                    return;
                }

                if (bal < 0)
                {
                    errorMessage(ErrorCodes.e0018);
                    txtBal.Focus();
                    return;
                }

                if (inner <= 0)
                {
                    errorMessage(ErrorCodes.e0019);
                    txtInFee.Focus();
                    return;
                }

                if (outer <= 0)
                {
                    errorMessage(ErrorCodes.e0019);
                    txtOutFee.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message);
                return;
            }


            // Add bank
            if (!service.existedBank(txtBank.Text))
            {
                Bank bank = new Bank()
                {
                    Name = txtBank.Text,
                    Currency = cbCurrency.SelectedItem.ToString(),
                    InnerWdhFee = int.Parse(txtInFee.Text),
                    OuterWdhFee = int.Parse(txtOutFee.Text)
                };
                if (service.addBank(bank))
                {
                    successMessage(ErrorCodes.m0004);
                }
                else
                {
                    errorMessage(ErrorCodes.e0011);
                }
            }

            // Add account
            Account account = new Account() {
                Id = txtUsername.Text,
                Pin = txtPassword.Text,
                Name = txtName.Text,
                Bank = txtBank.Text,
                CashWithdraw = int.Parse(txtCash.Text),
                Balance = int.Parse(txtBal.Text),
                Currency = cbCurrency.SelectedItem.ToString()
            };
            if (service.addAccount(account) 
                && service.addProfile(account))
            {
                successMessage(ErrorCodes.m0002);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }


        private void AddAccount_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin.Show();
            resetFields();
            this.Hide();
        }
    }
}
