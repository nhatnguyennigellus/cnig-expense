using NigelFinanceManage.Entity;
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
    public partial class Main : Form
    {
        Account account = new Account();
        Login frmLogin = new Login();
        public Main()
        {
            InitializeComponent();
        }

        public Main(Account account, Login frmLogin)
        {
            InitializeComponent();
            this.account = account;
            this.frmLogin = frmLogin;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtCash.Text = account.CashWithdraw.ToString();
            txtBank.Text = account.Balance.ToString();
            lbID.Text = account.Id;
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin.Show();
            this.Close();
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

    }
}
