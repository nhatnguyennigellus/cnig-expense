﻿using NigelFinanceManage.Entity;
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
            cbWdhMonth.SelectedIndex = 0;
            cbWdhYear.SelectedIndex = 0;
            cbIncMonth.SelectedIndex = 0;
            cbIncYear.SelectedIndex = 0;
            cbPayMonth.SelectedIndex = 0;
            cbPayYear.SelectedIndex = 0;

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
                    (account.Id, dtpIncFrom.Value.Month, dtpIncTo.Value.Year);
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
            dgvPayment.Columns[1].DefaultCellStyle.Alignment = 
                DataGridViewContentAlignment.MiddleRight;
            dgvPayment.Columns[0].Width = 60;
            dgvPayment.Columns[1].Width = 60;
            dgvPayment.Columns[2].Width = 140;

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

                txtIncAmount.Text = "";
                txtIncDesc.Text = "";

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

                txtPayAmount.Text = "";
                txtPayDesc.Text = "";
            }
            else
            {
                errorMessage("Error occurred!");
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
                errorMessage("No data");
                return;
            }
            else
            {
                if (sttMain.Text.Length > 0)
                {
                    successMessage(sttMain.Text + " Data loaded!");

                }
                else
                {
                    successMessage("Data loaded");
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
                errorMessage("No data");
            }
            else
            {
                successMessage("Data loaded");
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
                errorMessage("No data");
            }
            else
            {
                successMessage("Data loaded");
            }

            dgvWdh.DataSource = dt;
        }

        private void btnPlanAdd_Click(object sender, EventArgs e)
        {
            if (txtPlanAmount.Text == "")
            {
                errorMessage("Amount is required!");
                txtPlanAmount.Focus();
                return;
            }
            if (txtPlanDesc.Text == "")
            {
                errorMessage("Description is required!");
                txtPlanDesc.Focus();
                return;
            }

            int amount = int.Parse(txtPlanAmount.Text);

            string id = service.generateId(Plan.PREFIX, service.getPaymentList(lbID.Text));
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
                successMessage("Plan added!");

                DataTable dt = service.getPlanData(lbID.Text);
                dgvPlan.DataSource = dt;

                refreshPlanList(dt);
            }
            else
            {
                errorMessage("Error occurred!");
            }
        }

        private void btnWdhAdd_Click(object sender, EventArgs e)
        {
            if (txtWdhAmount.Text == "")
            {
                errorMessage("Amount is required!");
                txtPlanAmount.Focus();
                return;
            }
            if (txtATMOther.Text == "" && rbATMOther.Checked == true)
            {
                errorMessage("ATM is required!");
                return;
            }

            int amount = int.Parse(txtWdhAmount.Text);
            if (int.Parse(txtBank.Text) - amount < 100000)
            {
                errorMessage("Not enough budget!");
                return;
            }

            string id = service.generateId(Withdrawal.PREFIX, 
                service.getWithdrawalList(lbID.Text));
            string description = "";
            int extraWdh = 0;
            if (rbATMACB.Checked == true)
            {
                description = cbATMACB.SelectedItem.ToString();
                extraWdh += 1100;
            }
            else if (rbATMOther.Checked == true)
            {
                description = txtATMOther.Text;
                extraWdh += 3300;
            }
            amount += extraWdh;
            Withdrawal wdh = new Withdrawal
            {
                Id = id,
                Amount = amount,
                Currency = account.Currency,
                DateExpense = dtpBizDate.Value,
                Description = description
            };

            if (service.addWithdrawalLog(wdh, lbID.Text))
            {
                successMessage("Withdrawal Log added! Data loaded!");

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
                errorMessage("Error occurs!");
            }
        }

        private void rbATMACB_CheckedChanged(object sender, EventArgs e)
        {
            if (rbATMACB.Checked)
            {
                cbATMACB.Items.Clear();
                foreach (string atm in service.getATMACBList())
                {
                    
                    cbATMACB.Items.Add(atm);
                }
            }
        }
    }
}
