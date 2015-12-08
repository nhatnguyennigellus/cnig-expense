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
    public partial class Login : Form
    {
        DiaryService service;
        public Login()
        {
            service = new DiaryService();
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string id = txtUsername.Text;
            string pin = txtPassword.Text;
            if (service.isAuthenticated(id, pin))
            {
                Main frm = new Main(service.getAccountById(id), this, service,
                    cbDB.SelectedIndex);
                frm.Show();
            }
            else
            {
                sttLogin.Text = "Invalid ID or PIN!";
                sttLogin.ForeColor = Color.DarkRed;
                statusStrip1.Refresh();
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            cbDB.SelectedIndex = 0;
        }

        private void cbDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            service.chooseDatabase(cbDB.SelectedIndex);
        }
    }
}
