using Microsoft.Win32;
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
    public partial class Main : Form
    {
        Account account = new Account();
        Login frmLogin = new Login();
        AdminService admin;
        DiaryService service;
        int database = -1;
        public Main()
        {
            InitializeComponent();
        }

        public Main(Account account, Login frmLogin, DiaryService service, AdminService admin, int database)
        {
            InitializeComponent();
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("Nigel Finance Diary", "\"" + Application.ExecutablePath + "\"");
            }
            this.account = account;
            this.frmLogin = frmLogin;
            this.service = service;
            this.admin = admin;
            frmLogin.Hide();
            this.database = database;
            service.chooseDatabase(database);
            loadPayType();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            txtCash.Text = account.CashWithdraw.ToString();
            txtBank.Text = account.Balance.ToString();
            lbID.Text = account.Id;

            cbAddTo.SelectedIndex = 0;
            cbPayBy.SelectedIndex = 1;
            cbPayType.SelectedIndex = 0;
            cbIncSrchType.SelectedIndex = 0;
            cbPaySrchType.SelectedIndex = 0;
            cbWdhMonth.SelectedIndex = 0;
            cbWdhYear.SelectedIndex = 0;
            cbIncMonth.SelectedIndex = 0;
            cbIncYear.SelectedIndex = 0;
            cbPayMonth.SelectedIndex = 0;
            cbPayYear.SelectedIndex = 0;

            getIncPayTemp();
            sttMain.Text = "Welcome, " + account.Name;
        }

        public void getIncPayTemp()
        {
            cbIncDescTemp.Items.Clear();
            cbPayDescTemp.Items.Clear();
            foreach (QuickEntry qeInc in service.getQEDataByType(account.Id, "Income"))
            {
                cbIncDescTemp.Items.Add(qeInc.Description);
            }
            foreach (QuickEntry info in service.getQEDataByType(account.Id, "Payment"))
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
            if (dtInc.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
                return;
            }
            dgvIncome.Columns[1].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleRight;

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
                    (account.Id, int.Parse(cbIncMonth.SelectedItem.ToString()),
                        int.Parse(cbIncYear.SelectedItem.ToString()));
            }
            else if (cbIncSrchType.SelectedIndex == 1)
            {
                dtInc = service.getIncomeDataByDate(account.Id, dtpIncFrom.Value);
            }
            dgvIncome.DataSource = dtInc;
            dgvIncome.Columns[2].Width = 150;
            dgvIncome.Columns[3].Width = 250;
            dgvIncome.Columns[1].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleRight;

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
            if (dtPay.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
                return;
            }
            dgvPayment.Columns[1].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleRight;
            dgvPayment.Columns[0].Width = 60;
            dgvPayment.Columns[1].Width = 60;
            dgvPayment.Columns[2].Width = 140;

            txtPayTotal.Text = calculateTotal(dtPay).ToString();
        }

        private void cbPaySrchType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPaySrchType.SelectedIndex == 0) // Day range
            {
                dtpPayFrom.Enabled = true;
                dtpPayTo.Enabled = true;
                cbPayMonth.Enabled = false;
                cbPayYear.Enabled = false;
            }
            else if (cbPaySrchType.SelectedIndex == 2) // Month
            {
                dtpPayFrom.Enabled = false;
                dtpPayTo.Enabled = false;
                cbPayMonth.Enabled = true;
                cbPayYear.Enabled = true;
            }
            else if (cbPaySrchType.SelectedIndex == 1) // Date
            {
                dtpPayFrom.Enabled = true;
                dtpPayTo.Enabled = false;
                cbPayMonth.Enabled = false;
                cbPayYear.Enabled = false;
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
                    (account.Id, int.Parse(cbPayMonth.SelectedItem.ToString()),
                        int.Parse(cbPayYear.SelectedItem.ToString()));
            }
            else if (cbPaySrchType.SelectedIndex == 1)
            {
                dtPay = service.getPaymentDataByDate(account.Id, dtpPayFrom.Value);
            }
            dtPay.DefaultView.RowFilter = "Description like '%" + txtPaySrch.Text + "%'";
            dgvPayment.DataSource = dtPay;
            txtPayTotal.Text = calculateTotal(dtPay).ToString();
            dgvPayment.Columns[1].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleRight;

        }

        private void btnPlanImport_Click(object sender, EventArgs e)
        {
            dgvPayPlan.DataSource = service.getPlanData(lbID.Text);
            dgvPayPlan.Columns[0].Visible = false;
            dgvPayPlan.Columns[2].Visible = false;
            dgvPayPlan.Columns[3].Width = 170;
            dgvPayPlan.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

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
                errorMessage(ErrorCodes.e0002);
                txtIncAmount.Focus();
                return;
            }
            if (txtIncDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0009);
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
                successMessage(ErrorCodes.m0011);

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

                txtIncAmount.Text = "";
                txtIncDesc.Text = "";

                DataTable dt = service.getIncomeData(lbID.Text);
                txtIncTotal.Text = calculateTotal(dt).ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
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
                errorMessage(ErrorCodes.e0028);

            }
            else
            {
                errorMessage("");
            }
        }

        private void btnPayAdd_Click(object sender, EventArgs e)
        {
            if (txtPayAmount.Text == "")
            {
                errorMessage(ErrorCodes.e0002);
                txtPayAmount.Focus();
                return;
            }
            if (txtPayDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0009);
                txtPayDesc.Focus();
                return;
            }

            int amount = int.Parse(txtPayAmount.Text);
            switch (cbPayBy.SelectedIndex)
            {
                case 0:
                    if (int.Parse(txtBank.Text) - amount < 0)
                    {
                        errorMessage(ErrorCodes.e0023);
                        txtPayAmount.Focus();
                        return;
                    }
                    break;
                case 1:
                    if (int.Parse(txtCash.Text) - amount < 0)
                    {
                        errorMessage(ErrorCodes.e0023);
                        txtPayAmount.Focus();
                        return;
                    }
                    break;
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
                Currency = account.Currency,
                Budget = cbPayBy.SelectedIndex
            };
            if (service.addPaymentLog(payment, lbID.Text))
            {
                successMessage(ErrorCodes.m0015);

                DataTable dt = service.getPaymentData(lbID.Text);
                txtPayTotal.Text = calculateTotal(dt).ToString();

                if (cbPayBy.SelectedItem.ToString().Equals("Bank"))
                {
                    account.Balance -= amount;
                    txtBank.Text = account.Balance.ToString();
                    service.updateBudget(lbID.Text, account.Balance,
                        cbPayBy.SelectedItem.ToString());
                }
                else if (cbPayBy.SelectedItem.ToString().Equals("Cash"))
                {
                    account.CashWithdraw -= amount;
                    txtCash.Text = account.CashWithdraw.ToString();
                    service.updateBudget(lbID.Text, account.CashWithdraw,
                        cbPayBy.SelectedItem.ToString());
                }
                txtPayAmount.Text = "";
                txtPayDesc.Text = "";
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnPlanViewAll_Click(object sender, EventArgs e)
        {
            DataTable dt = service.getPlanData(lbID.Text);
            dgvPlan.DataSource = dt;
            dgvPlan.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvPlan.Columns[2].Width = 150;
            dgvPlan.Columns[3].Width = 250;
            refreshPlanList(dt);

        }

        private void refreshPlanList(DataTable dt)
        {
            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
                return;
            }
            else
            {
                if (sttMain.Text.Length > 0)
                {
                    successMessage(sttMain.Text + " " + ErrorCodes.m0001);

                }
                else
                {
                    successMessage(ErrorCodes.m0001);
                }
            }
            int total = 0;
            foreach (DataRow row in dt.Rows)
            {
                total += int.Parse(row["Amount"].ToString());
            }
            txtPlanEstSum.Text = total.ToString();
            int cash = int.Parse(txtCash.Text);
            int balance = int.Parse(txtBank.Text);
            txtPlanEstCash.Text = (cash - total).ToString();
            txtPlanEstBal.Text = (balance - total).ToString();

            if (cash - total < 0)
            {
                txtPlanEstCash.ForeColor = Color.DarkRed;
            }
            else
            {
                txtPlanEstCash.ForeColor = Color.Green;
            }

            if (balance - total < 100000)
            {
                txtPlanEstBal.ForeColor = Color.DarkRed;   
            }
            else
            {
                txtPlanEstBal.ForeColor = Color.Green;
            }

        }

        private void btnWdhViewAll_Click(object sender, EventArgs e)
        {
            DataTable dt = service.getWithdrawalData(lbID.Text);

            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
            }
            else
            {
                successMessage(ErrorCodes.m0001);
            }

            dgvWdh.DataSource = dt;
            dgvWdh.Columns[0].Width = 70;
            dgvWdh.Columns[1].Width = 100;
            dgvWdh.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvWdh.Columns[2].Width = 170;
            dgvWdh.Columns[3].Width = 200;
        }

        private void btnWdhSrch_Click(object sender, EventArgs e)
        {
            DataTable dt = service.getWithdrawalDataByMonth(lbID.Text,
                int.Parse(cbWdhMonth.Text), int.Parse(cbWdhYear.Text));
            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
            }
            else
            {
                successMessage(ErrorCodes.m0001);
            }

            dgvWdh.DataSource = dt;
        }

        private void btnPlanAdd_Click(object sender, EventArgs e)
        {
            if (txtPlanAmount.Text == "")
            {
                errorMessage(ErrorCodes.e0002);
                txtPlanAmount.Focus();
                return;
            }
            if (txtPlanDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0009);
                txtPlanDesc.Focus();
                return;
            }

            int amount = int.Parse(txtPlanAmount.Text);

            string id = service.generateId(Plan.PREFIX, service.getPlanList(lbID.Text));
            string description = txtPlanDesc.Text;

            Plan plan = new Plan()
            {
                Id = id,
                Amount = amount,
                Description = description,
                DateExpense = dtpBizDate.Value,
                Currency = account.Currency
            };

            if (service.addPlanLog(plan, lbID.Text))
            {
                successMessage(ErrorCodes.m0017);

                DataTable dt = service.getPlanData(lbID.Text);
                dgvPlan.DataSource = dt;

                refreshPlanList(dt);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnWdhAdd_Click(object sender, EventArgs e)
        {
            if (txtWdhAmount.Text == "")
            {
                errorMessage(ErrorCodes.e0002);
                txtPlanAmount.Focus();
                return;
            }
            if (cbOtherATM.SelectedItem.ToString().Equals("") && rbATMOther.Checked == true)
            {
                errorMessage(ErrorCodes.e0003);
                return;
            }

            int amount = int.Parse(txtWdhAmount.Text);
            if (int.Parse(txtBank.Text) - amount < 100000)
            {
                errorMessage(ErrorCodes.e0023);
                return;
            }

            string id = service.generateId(Withdrawal.PREFIX, 
                service.getWithdrawalList(lbID.Text));
            string description = "";
            int extraWdh = 0;
            if (rbATMACB.Checked == true)
            {
                description = cbATMACB.SelectedItem.ToString();
                extraWdh += service.getWdhFee(account.Bank, Withdrawal.INNER_FEE);
            }
            else if (rbATMOther.Checked == true)
            {
                description = cbOtherATM.SelectedItem.ToString();
                extraWdh += service.getWdhFee(account.Bank, Withdrawal.OUTER_FEE);
            }
            amount += extraWdh;
            Withdrawal wdh = new Withdrawal
            {
                Id = id,
                Amount = amount,
                Currency = account.Currency,
                DateExpense = dtpBizDate.Value,
                Description = description,
                Extra = extraWdh
            };

            if (service.addWithdrawalLog(wdh, lbID.Text))
            {
                successMessage(ErrorCodes.m0024 + " " + ErrorCodes.m0001);

                DataTable dt = service.getWithdrawalData(lbID.Text);
                dgvWdh.DataSource = dt;

                account.Balance = account.Balance - amount;
                account.CashWithdraw = account.CashWithdraw + amount - extraWdh;
                txtBank.Text = account.Balance.ToString();
                txtCash.Text = account.CashWithdraw.ToString();
                service.updateBudget(lbID.Text, int.Parse(txtBank.Text), "Bank");
                service.updateBudget(lbID.Text, int.Parse(txtCash.Text), "Cash");

            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void rbATMACB_CheckedChanged(object sender, EventArgs e)
        {
            loadATMACB();
        }

        private void dgvIncome_SelectionChanged(object sender, EventArgs e)
        {
            btnIncModify.Enabled = true;
            dtpIncDate.Enabled = true;

            if (dgvIncome.CurrentCell != null)
            {
                txtIncAmount.Text = dgvIncome.CurrentRow.Cells[1].Value.ToString();
                dtpIncDate.Value = DateTime.Parse(dgvIncome.CurrentRow.Cells[2].Value.ToString());
                txtIncDesc.Text = dgvIncome.CurrentRow.Cells[3].Value.ToString();
                cbAddTo.SelectedIndex = dgvIncome.CurrentRow.Cells[4].Value.ToString() == "Bank" ? 0 : 1;
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void btnIncModify_Click(object sender, EventArgs e)
        {
            int oldIncome = int.Parse(dgvIncome.CurrentRow.Cells[1].Value.ToString());
            string id = dgvIncome.CurrentRow.Cells[0].Value.ToString();

            FinanceInfo info = service.getIncomeById(account.Id, id);
            info.Amount = int.Parse(txtIncAmount.Text);
            info.Description = txtIncDesc.Text;
            info.DateExpense = dtpIncDate.Value;

            if (service.modifyIncome(info, account.Id))
            {
                successMessage(ErrorCodes.m0012);
                btnIncModify.Enabled = false;
                dtpIncDate.Enabled = false;
                int oldBalance = int.Parse(txtBank.Text);
                int newBalance = oldBalance - oldIncome
                    + int.Parse(txtIncAmount.Text);
                txtBank.Text = newBalance.ToString();

                if (cbAddTo.SelectedIndex == 0)
                    service.updateBudget(lbID.Text, int.Parse(txtBank.Text), "Bank");
                else
                    service.updateBudget(lbID.Text, int.Parse(txtCash.Text), "Cash");

                dtpIncDate.Enabled = false;
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnPayModify_Click(object sender, EventArgs e)
        {
            int oldPayment = int.Parse(dgvPayment.CurrentRow.Cells[1].Value.ToString());
            string id = dgvPayment.CurrentRow.Cells[0].Value.ToString();

            FinanceInfo info = service.getPaymentById(id, account.Id);
            info.Amount = int.Parse(txtPayAmount.Text);
            info.Description = txtPayDesc.Text;
            info.DateExpense = dtpPayDate.Value;

            if (service.modifyPayment(info, account.Id))
            {
                successMessage(ErrorCodes.m0016);
                btnPayModify.Enabled = false;
                dtpPayDate.Enabled = false;
                int oldBalance = int.Parse(txtCash.Text);
                int newBalance = oldBalance + oldPayment
                    - int.Parse(txtPayAmount.Text);
                txtCash.Text = newBalance.ToString();
                dtpPayDate.Enabled = false;

                if (cbPayBy.SelectedIndex == 0)
                    service.updateBudget(lbID.Text, int.Parse(txtBank.Text), "Bank");
                else
                    service.updateBudget(lbID.Text, int.Parse(txtCash.Text), "Cash");
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void dgvPayment_SelectionChanged(object sender, EventArgs e)
        {
            btnPayModify.Enabled = true;
            dtpPayDate.Enabled = true;

            if (dgvPayment.CurrentCell != null)
            {
                txtPayAmount.Text = dgvPayment.CurrentRow.Cells[1].Value.ToString();
                dtpPayDate.Value = DateTime.Parse(dgvPayment.CurrentRow.Cells[2].Value.ToString());
                txtPayDesc.Text = dgvPayment.CurrentRow.Cells[3].Value.ToString();
                cbPayBy.SelectedIndex = dgvPayment.CurrentRow.Cells[4].Value.ToString() == "Bank" ? 0 : 1;
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void txtPayAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            CheckNumPress(e);
        }

        private void btnPlanModify_Click(object sender, EventArgs e)
        {
            string id = dgvPlan.CurrentRow.Cells[0].Value.ToString();

            FinanceInfo info = service.getPlanById(account.Id, id);
            info.Amount = int.Parse(txtPlanAmount.Text);
            info.Description = txtPlanDesc.Text;

            if (service.modifyPlan(info, account.Id))
            {
                successMessage(ErrorCodes.m0019);
                DataTable dt = service.getPlanData(lbID.Text);
                dgvPlan.DataSource = dt;

                refreshPlanList(dt);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void dgvPlan_SelectionChanged(object sender, EventArgs e)
        {
            btnPlanModify.Enabled = true;

            if (dgvPlan.CurrentCell != null)
            {
                txtPlanAmount.Text = dgvPlan.CurrentRow.Cells[1].Value.ToString();
                txtPlanDesc.Text = dgvPlan.CurrentRow.Cells[3].Value.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void btnPlanRemove_Click(object sender, EventArgs e)
        {
            string id = dgvPlan.CurrentRow.Cells[0].Value.ToString();

            FinanceInfo info = service.getPlanById(account.Id, id);
            if (service.removePlan(info, lbID.Text))
            {
                successMessage(ErrorCodes.m0030);
                DataTable dt = service.getPlanData(lbID.Text);
                dgvPlan.DataSource = dt;

                refreshPlanList(dt);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnPlanToPay_Click(object sender, EventArgs e)
        {
            string planId = dgvPayPlan.CurrentRow.Cells[0].Value.ToString();
            FinanceInfo plan = service.getPlanById(lbID.Text, planId);
            Payment payment = new Payment();
            payment.Id = service.generateId(Payment.PREFIX, service.getPaymentList(lbID.Text));
            service.p2P(plan, payment);

            if (int.Parse(txtCash.Text) - plan.Amount < 0)
            {
                errorMessage(ErrorCodes.e0023);
                return;
            }

            if (service.addPaymentLog(payment, lbID.Text))
            {
                successMessage(ErrorCodes.m0018);
                DataTable dtPay = service.getPaymentData(lbID.Text);
                txtPayTotal.Text = calculateTotal(dtPay).ToString();
                account.CashWithdraw -= plan.Amount;
                txtCash.Text = account.CashWithdraw.ToString();
                service.updateBudget(lbID.Text, account.CashWithdraw, "Cash");
                txtPayAmount.Text = "";
                txtPayDesc.Text = "";

                service.removePlan(plan, lbID.Text);
                DataTable dtPlan = service.getPlanData(lbID.Text);
                dgvPayPlan.DataSource = dtPlan;
                dgvPayment.DataSource = dtPay;
                txtCash.Text = account.Balance.ToString();
                refreshPlanList(dtPlan);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnWdhModify_Click(object sender, EventArgs e)
        {
            int oldWdh = int.Parse(dgvWdh.CurrentRow.Cells[1].Value.ToString());
            int chgWdh =  int.Parse(txtWdhAmount.Text);
            string id = dgvWdh.CurrentRow.Cells[0].Value.ToString();

            Withdrawal info = service.getWithdrawalById(id, account.Id);
            info.Amount = int.Parse(txtWdhAmount.Text);
            info.Description = txtATMOther.Text;
            info.DateExpense = dtpIncDate.Value;

            if (service.modifyWithdrawal(info, lbID.Text))
            {
                successMessage(ErrorCodes.m0025);
                DataTable dt = service.getWithdrawalData(lbID.Text);
                dgvWdh.DataSource = dt;

                int oldBalance = int.Parse(txtBank.Text);
                int newBalance = oldBalance + oldWdh - chgWdh;
                int oldCash = int.Parse(txtCash.Text);
                int newCash = oldCash - oldWdh + chgWdh;

                txtBank.Text = newBalance + "";
                txtCash.Text = newCash + "";
                service.updateBudget(lbID.Text, int.Parse(txtBank.Text), "Bank");
                service.updateBudget(lbID.Text, int.Parse(txtCash.Text), "Cash");
                txtATMOther.Enabled = false;
            }
            else
            {
                errorMessage(ErrorCodes.m0011);
            }

            
        }

        private void dgvWdh_SelectionChanged(object sender, EventArgs e)
        {
            btnWdhModify.Enabled = true;
            dtpWdh.Enabled = true;
            txtATMOther.Enabled = true;

            if (dgvWdh.CurrentCell != null)
            {
                txtWdhAmount.Text = dgvWdh.CurrentRow.Cells[1].Value.ToString();
                dtpWdh.Value = DateTime.Parse(dgvWdh.CurrentRow.Cells[2].Value.ToString());
                txtATMOther.Text = dgvWdh.CurrentRow.Cells[3].Value.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void btnWdhRemove_Click(object sender, EventArgs e)
        {
            string id = dgvWdh.CurrentRow.Cells[0].Value.ToString();
            int oldWdh = int.Parse(dgvWdh.CurrentRow.Cells[1].Value.ToString());
            string oldATM = dgvWdh.CurrentRow.Cells[3].Value.ToString();
            Withdrawal info = service.getWithdrawalById(id, lbID.Text);

            if (service.removeWithdrawal(info, lbID.Text))
            {
                successMessage(ErrorCodes.m0026);

                DataTable dt = service.getWithdrawalData(lbID.Text);
                dgvWdh.DataSource = dt;

                int oldBalance = int.Parse(txtBank.Text);
                int oldCash = int.Parse(txtCash.Text);
                int newBalance = oldBalance + oldWdh;
                int extra = info.Extra;
                int newCash = oldCash - oldWdh + extra;
                txtCash.Text = newCash.ToString();
                txtBank.Text = newBalance.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void btnPayToPlan_Click(object sender, EventArgs e)
        {
            int delPayment = int.Parse(dgvPayment.CurrentRow.Cells[1].Value.ToString());
            string payId = dgvPayment.CurrentRow.Cells[0].Value.ToString();
            Payment payment = service.getPaymentById(payId, lbID.Text);

            if (service.removePayment(payment, lbID.Text))
            {
                successMessage(ErrorCodes.m0014);

                int oldCash = int.Parse(txtCash.Text);
                int newCash = oldCash + delPayment;
                account.CashWithdraw = newCash;
                txtCash.Text = newCash.ToString();
                service.updateBudget(lbID.Text, newCash, "Cash");

                Plan plan = new Plan();
                plan.Id = service.generateId(Plan.PREFIX, service.getPlanList(lbID.Text));
                service.p2P(payment, plan);
                service.addPlanLog(plan, lbID.Text);

                txtCash.Text = account.Balance.ToString();
                DataTable dtPlan = service.getPlanData(lbID.Text);
                dgvPayPlan.DataSource = dtPlan;
                refreshPlanList(dtPlan);
                DataTable dtPay = service.getPlanData(lbID.Text);
                dgvPayment.DataSource = dtPay;
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void miQE_Click(object sender, EventArgs e)
        {
            QE qeForm = new QE(account, service, admin, this);
            qeForm.Show();
        }

        private void dtpBizDate_ValueChanged(object sender, EventArgs e)
        {
            DataTable dt = service.getPaymentDataByDate(lbID.Text, dtpBizDate.Value);
            if (dt.Rows.Count > 0)
            {
                pnDate.BackColor = Color.Green;
            }
            else
            {
                pnDate.BackColor = Color.DarkRed;
            }
        }

        private void btnPayReport_Click(object sender, EventArgs e)
        {
            ReportTemplate report = null;
            DataTable dtPay = new DataTable();
            if (cbPaySrchType.SelectedIndex == 0)
            {
                dtPay = service.getPaymentDataByRange
                    (account.Id, dtpPayFrom.Value, dtpPayTo.Value);
                report = new DayRangeReport("Payment", dtpPayFrom.Value, dtpPayTo.Value, dtPay);
            }
            else if (cbPaySrchType.SelectedIndex == 2)
            {
                dtPay = service.getPaymentDataByMonth
                    (account.Id, int.Parse(cbPayMonth.SelectedItem.ToString()),
                        int.Parse(cbPayYear.SelectedItem.ToString()));
                report = new MonthlyReport("Payment",
                                        int.Parse(cbPayMonth.SelectedItem.ToString()),
                                        int.Parse(cbPayYear.SelectedItem.ToString()),
                                        dtPay);
            }
            else if (cbPaySrchType.SelectedIndex == 1)
            {
                dtPay = service.getPaymentDataByDate(account.Id, dtpPayFrom.Value);
                report = new DateReport("Payment", dtpPayFrom.Value, dtPay);
            }
            report.generateReport();
        }

        private void btnIncReport_Click(object sender, EventArgs e)
        {
            DataTable dtInc = null;
            ReportTemplate report = null;
            if (cbIncSrchType.SelectedIndex == 2)
            {
                dtInc = service.getIncomeDataByMonth
                    (account.Id, int.Parse(cbIncMonth.SelectedItem.ToString()),
                        int.Parse(cbIncYear.SelectedItem.ToString()));
                report = new MonthlyReport("Income",
                                        int.Parse(cbIncMonth.SelectedItem.ToString()),
                                        int.Parse(cbIncYear.SelectedItem.ToString()),
                                        dtInc);
            }
            else if (cbIncSrchType.SelectedIndex == 0)
            {
                dtInc = service.getIncomeDataByRange(account.Id, dtpIncFrom.Value, dtpIncTo.Value);
                report = new DayRangeReport("Income", dtpIncFrom.Value, dtpIncTo.Value, dtInc);
            }
            else if (cbIncSrchType.SelectedIndex == 1)
            {
                dtInc = service.getIncomeDataByDate(account.Id, dtpIncFrom.Value);
                report = new DateReport("Income", dtpIncFrom.Value, dtInc);
            }
            report.generateReport();
        }

        private void btnWdhReport_Click(object sender, EventArgs e)
        {
            DataTable dtWdh = service.getWithdrawalDataByMonth
                    (account.Id, int.Parse(cbWdhMonth.SelectedItem.ToString()),
                        int.Parse(cbWdhYear.SelectedItem.ToString()));
            MonthlyReport report = new MonthlyReport("Withdraw",
                                        int.Parse(cbWdhMonth.SelectedItem.ToString()),
                                        int.Parse(cbWdhYear.SelectedItem.ToString()),
                                        dtWdh);
            report.generateReport();
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin.Show();
            this.Hide();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void rbATMOther_CheckedChanged(object sender, EventArgs e)
        {
            loadATMOther();
        }

        internal void loadATMOther()
        {
            if (rbATMOther.Checked)
            {
                cbOtherATM.Items.Clear();
                foreach (string atm in service.getOtherATMList(account))
                {
                    cbOtherATM.Items.Add(atm);
                }
            }
        }

        internal void loadATMACB()
        {
            if (rbATMACB.Checked)
            {
                cbATMACB.Items.Clear();
                foreach (string atm in service.getATMListByBank(account.Bank))
                {
                    cbATMACB.Items.Add(atm);
                }
            }
        }

        private void loadPayType()
        {
            cbPayType.Items.Clear();
            foreach(string type in service.getPayType())
            {
                cbPayType.Items.Add(type);
            }
        }

        private void addBankToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddATM addAtmForm = new AddATM(service, admin, account, this);
            addAtmForm.Show();
        }
    }
}
