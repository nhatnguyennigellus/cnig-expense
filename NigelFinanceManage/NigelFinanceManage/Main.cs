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
    public partial class Main : Form
    {
        Account account = new Account();
        Login frmLogin = new Login();
        DiaryService service = new DiaryService();
        public Main()
        {
            InitializeComponent();
        }

        public Main(Account account, Login frmLogin, DiaryService service, int database)
        {
            InitializeComponent();
            this.account = account;
            this.frmLogin = frmLogin;
            this.service = service;
            frmLogin.Hide();
            service.chooseDatabase(database);
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtCash.Text = account.CashWithdraw.ToString();
            txtBank.Text = account.Balance.ToString();
            lbID.Text = account.Id;
            cbAddTo.SelectedIndex = 0;
            cbIncSrchType.SelectedIndex = 0;
            getIncPayTemp();
            sttMain.Text = "Welcome, " + account.Name;
        }

        private void getIncPayTemp()
        {
            foreach (FinanceInfo info in service.getIncomeList(account.Id))
            {
                cbIncDescTemp.Items.Add(info.Description);
            }
            foreach (FinanceInfo info in service.getPaymentList(account.Id))
            {
                cbPayDescTemp.Items.Add(info.Description);
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin.Show();
            this.Hide();
        }

        private void cbIncDescTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIncDesc.Text = cbIncDescTemp.SelectedItem.ToString();
        }

        private void cbPayDescTemp_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPayDesc.Text = cbPayDescTemp.SelectedItem.ToString();
        }

        private void btnIncViewAll_Click(object sender, EventArgs e)
        {
            DataTable dtInc = service.getIncomeData(lbID.Text);
            if (dtInc != null)
            {
                dgvIncome.DataSource = dtInc;
            }
            txtIncTotal.Text = calculateTotal(dtInc).ToString();
        }

        private void btnIncSrch_Click(object sender, EventArgs e)
        {
            DataTable dtInc = new DataTable();
            if (cbIncSrchType.SelectedIndex == 0)
            {
                dtInc = service.getIncomeDataByRange
                    (account.Id, dtpIncFrom.Value, dtpIncTo.Value);
            }
            else if (cbIncSrchType.SelectedIndex == 2)
            {
                dtInc = service.getIncomeDataByMonth
                    (account.Id, dtpIncFrom.Value.Month, dtpIncTo.Value.Year);
            }
            else if (cbIncSrchType.SelectedIndex == 1)
            {
                dtInc = service.getIncomeDataByDate(account.Id, dtpIncFrom.Value);
            }
            dgvIncome.DataSource = dtInc;
            txtIncTotal.Text = calculateTotal(dtInc).ToString();
        }

        private int calculateTotal(DataTable dt)
        {
            int total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += int.Parse(row["Amount"].ToString());
            }

            return total;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void cbIncSrchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIncSrchType.SelectedIndex == 0) // Day range
            {
                dtpIncFrom.Enabled = true;
                dtpIncTo.Enabled = true;
                cbIncMonth.Enabled = false;
                cbIncYear.Enabled = false;
            }
            else if (cbIncSrchType.SelectedIndex == 2) // Month
            {
                dtpIncFrom.Enabled = false;
                dtpIncTo.Enabled = false;
                cbIncMonth.Enabled = true;
                cbIncYear.Enabled = true;
            }
            else if (cbIncSrchType.SelectedIndex == 1) // Date
            {
                dtpIncFrom.Enabled = true;
                dtpIncTo.Enabled = false;
                cbIncMonth.Enabled = false;
                cbIncYear.Enabled = false;
            }
        }

        private void btnPayViewAll_Click(object sender, EventArgs e)
        {
            DataTable dtPay = service.getPaymentData(lbID.Text);
            if (dtPay != null)
            {
                dgvPayment.DataSource = dtPay;
            }
            txtPayTotal.Text = calculateTotal(dtPay).ToString();
        }

        private void cbPaySrchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIncSrchType.SelectedIndex == 0) // Day range
            {
                dtpIncFrom.Enabled = true;
                dtpIncTo.Enabled = true;
                cbIncMonth.Enabled = false;
                cbIncYear.Enabled = false;
            }
            else if (cbIncSrchType.SelectedIndex == 2) // Month
            {
                dtpIncFrom.Enabled = false;
                dtpIncTo.Enabled = false;
                cbIncMonth.Enabled = true;
                cbIncYear.Enabled = true;
            }
            else if (cbIncSrchType.SelectedIndex == 1) // Date
            {
                dtpIncFrom.Enabled = true;
                dtpIncTo.Enabled = false;
                cbIncMonth.Enabled = false;
                cbIncYear.Enabled = false;
            }
        }

        private void btnPaySrch_Click(object sender, EventArgs e)
        {
            DataTable dtPay = new DataTable();
            if (cbPaySrchType.SelectedIndex == 0)
            {
                dtPay = service.getPaymentDataByRange
                    (account.Id, dtpPayFrom.Value, dtpPayTo.Value);
            }
            else if (cbPaySrchType.SelectedIndex == 2)
            {
                dtPay = service.getPaymentDataByMonth
                    (account.Id, dtpPayFrom.Value.Month, dtpPayTo.Value.Year);
            }
            else if (cbPaySrchType.SelectedIndex == 1)
            {
                dtPay = service.getPaymentDataByDate(account.Id, dtpPayFrom.Value);
            }
            dgvPayment.DataSource = dtPay;
            txtPayTotal.Text = calculateTotal(dtPay).ToString();
        }

        private void btnPlanImport_Click(object sender, EventArgs e)
        {
            dgvPayPlan.DataSource = service.getPlanData(lbID.Text);
            dgvPayPlan.Columns[0].Visible = false;
            dgvPayPlan.Columns[2].Visible = false;
            dgvPayPlan.Columns[3].Width = 170;
        }

        private void errorMessage(string message)
        {
            sttMain.Text = message;
            sttMain.ForeColor = Color.DarkRed;
            statusStrip1.Refresh();

        }

        private void successMessage(string message)
        {
            sttMain.Text = message;
            sttMain.ForeColor = Color.Green;
            statusStrip1.Refresh();

        }
        private void btnIncAdd_Click(object sender, EventArgs e)
        {
            if (txtIncAmount.Text == "")
            {
                errorMessage("Amount is required!");
                txtIncAmount.Focus();
                return;
            }
            if (txtIncDesc.Text == "")
            {
                errorMessage("Description is required!");
                txtIncDesc.Focus();
                return;
            }

            string id = service.generateId(Income.PREFIX, service.getIncomeList(lbID.Text));
            int amount = int.Parse(txtIncAmount.Text);
            string desc = txtIncDesc.Text;
            DateTime date = dtpBizDate.Value;
            Income income = new Income
            {
                Id = id,
                Amount = amount,
                Description = desc,
                DateExpense = date,
                Currency = account.Currency
            };
            if (service.addIncomeLog(income, lbID.Text))
            {
                successMessage("Income log added!");

                DataTable dt =  service.getIncomeData(lbID.Text);
                dgvIncome.DataSource = dt;
                txtIncTotal.Text = calculateTotal(dt).ToString();

                if (cbAddTo.SelectedItem.ToString().Equals("Bank"))
                {
                    account.Balance += amount;
                    txtBank.Text = account.Balance.ToString();
                    service.updateBudget(lbID.Text, account.Balance,
                        cbAddTo.SelectedItem.ToString());
                }
                else if (cbAddTo.SelectedItem.ToString().Equals("Cash"))
                {
                    account.CashWithdraw += amount;
                    txtCash.Text = account.CashWithdraw.ToString();
                    service.updateBudget(lbID.Text, account.CashWithdraw,
                        cbAddTo.SelectedItem.ToString());
                }
            }
            else
            {
                errorMessage("Error occurred!");
            }
        }

        private void txtIncAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumPress(e);
        }

        private void CheckNumPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnPayAdd_Click(object sender, EventArgs e)
        {
            if (txtPayAmount.Text == "")
            {
                errorMessage("Amount is required!");
                txtPayAmount.Focus();
                return;
            }
            if (txtPayDesc.Text == "")
            {
                errorMessage("Description is required!");
                txtPayDesc.Focus();
                return;
            }

            int amount = int.Parse(txtPayAmount.Text);
            if (int.Parse(txtCash.Text) - amount < 0)
            {
                errorMessage("Not enough budget!");
                txtPayAmount.Focus();
                return;
            }

            string id = service.generateId(Payment.PREFIX, service.getPaymentList(lbID.Text));
            
            string desc = txtPayDesc.Text;
            DateTime date = dtpBizDate.Value;
            Payment payment = new Payment
            {
                Id = id,
                Amount = amount,
                Description = desc,
                DateExpense = date,
                Currency = account.Currency
            };
            if (service.addPaymentLog(payment, lbID.Text))
            {
                successMessage("Payment log added!");

                DataTable dt = service.getPaymentData(lbID.Text);
                txtPayTotal.Text = calculateTotal(dt).ToString();
                account.CashWithdraw -= amount;
                txtCash.Text = account.CashWithdraw.ToString();
                service.updateBudget(lbID.Text, account.CashWithdraw, "Cash");
            }
            else
            {
                errorMessage("Error occurred!");
            }
        }
    }
}
