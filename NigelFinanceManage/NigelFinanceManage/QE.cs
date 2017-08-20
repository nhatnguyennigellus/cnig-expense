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
    public partial class QE : Form
    {
        Account account = new Account();
        DiaryService service;
        AdminService admin;
        Main main;
        public QE()
        {
            InitializeComponent();
        }

        public QE(Account account, DiaryService service, AdminService admin, Main main)
        {
            InitializeComponent();
            this.account = account;
            this.service = service;
            this.admin = admin;
            cbQEType.SelectedIndex = 0;
            this.main = main;
        }

        private void errorMessage(string message)
        {
            sttQE.Text = admin.getError(message);
            sttQE.ForeColor = Color.DarkRed;
            statusStrip1.Refresh();

        }

        private void successMessage(string message)
        {
            sttQE.Text = admin.getError(message);
            sttQE.ForeColor = Color.Green;
            statusStrip1.Refresh();

        }

        private void btnQEViewAll_Click(object sender, EventArgs e)
        {
            DataTable dt = service.getQEData(account.Id);
            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
                return;
            }
            if (dt != null || dt.Rows.Count > 0)
            {
                dgvQE.DataSource = dt;

                dgvQE.Columns[0].Width = 20;
                dgvQE.Columns[1].Width = 60;
                dgvQE.Columns[2].Width = 120;
            }
        }

        private void btnQEAdd_Click(object sender, EventArgs e)
        {
            if (txtQEDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0009);
                txtQEDesc.Focus();
                return;
            }

            int id = service.generateQEId(account.Id);
            string desc = txtQEDesc.Text;
            string type = cbQEType.SelectedItem.ToString();
            QuickEntry qe = new QuickEntry
            {
                Id = id,
                Description = desc,
                Type = type
            };

            if (service.addQE(qe, account.Id))
            {
                successMessage(ErrorCodes.m0020);
                DataTable dt = service.getQEData(account.Id);
                dgvQE.DataSource = dt;

                txtQEDesc.Text = "";
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
            main.getIncPayTemp();
        }

        private void btnQEModify_Click(object sender, EventArgs e)
        {
            if (txtQEDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0009);
                txtQEDesc.Focus();
                return;
            }

            int id = int.Parse(dgvQE.CurrentRow.Cells[0].Value.ToString());
            string desc = txtQEDesc.Text;
            string type = cbQEType.SelectedItem.ToString();
            QuickEntry qe = service.getById(account.Id, id.ToString());
            qe.Type = type;
            qe.Description = desc;

            if (service.modifyQE(qe, account.Id))
            {
                successMessage(ErrorCodes.m0021);
                DataTable dt = service.getQEData(account.Id);
                dgvQE.DataSource = dt;

                txtQEDesc.Text = "";
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
            main.getIncPayTemp();
        }

        private void dgvQE_SelectionChanged(object sender, EventArgs e)
        {
            btnQEModify.Enabled = true;
            btnQERemove.Enabled = true;

            if (dgvQE.CurrentCell != null)
            {
                txtQEDesc.Text = dgvQE.CurrentRow.Cells[2].Value.ToString();
                cbQEType.SelectedItem = dgvQE.CurrentRow.Cells[1].Value.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void btnQERemove_Click(object sender, EventArgs e)
        {
            int id = int.Parse(dgvQE.CurrentRow.Cells[0].Value.ToString());
            QuickEntry qe = service.getById(account.Id, id.ToString());
            if (service.removeQE(qe, account.Id))
            {
                successMessage(ErrorCodes.m0022);

                DataTable dt = service.getQEData(account.Id);
                dgvQE.DataSource = dt;
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
            main.getIncPayTemp();
        }
    }
}
