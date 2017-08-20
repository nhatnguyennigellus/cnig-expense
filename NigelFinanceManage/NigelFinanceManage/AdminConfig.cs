using NigelFinanceManage.Service;
using NigelFinanceManage.DAO;
using NigelFinanceManage.Data;
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
using System.IO;

namespace NigelFinanceManage
{
    public partial class AdminConfig : Form
    {
        Login frmLogin = new Login();
        AdminService service;
        ExcelOutput excel;
        public AdminConfig()
        {
            InitializeComponent();
        }

        public AdminConfig(Login frmLogin, AdminService service)
        {
            InitializeComponent();
            this.frmLogin = frmLogin;
            this.service = service;
            frmLogin.Hide();
            excel = new ExcelOutput();

        }

        private void AdminConfig_Load(object sender, EventArgs e)
        {
            loadXmlDbPath();
            cbDAOList.SelectedIndex = 0;
            rbError.Checked = true;
            loadExcelConfig();
        }

        private void loadExcelConfig()
        {
            excel = service.getExcelConfig();
            txtExcelOut.Text = excel.Path;
            txtExtn.Text = excel.Extension;
            txtDayRnRpFIleName.Text = excel.DayRangeFilename;
            txtDayRnRpParams.Text = excel.DayRangeParams.ToString();
            txtDayRnRpTitle.Text = excel.DayRangeTitle;
            txtDateRpFileName.Text = excel.DateFilename;
            txtDateRpParams.Text = excel.DateParams.ToString();
            txtDateRpTitle.Text = excel.DateTitle;
            txtMthRpFileName.Text = excel.MonthlyFilename;
            txtMthRpParams.Text = excel.MonthlyParams.ToString();
            txtMthRpTitle.Text = excel.MonthlyTitle;
            txtDateFormat.Text = excel.DateFormat;
        }

        private void loadXmlDbPath()
        {
            txtXMLSand.Text = service.getXmlDbPath(XMLXpathConfig.SANDBOX);
            txtXMLHouse.Text = service.getXmlDbPath(XMLXpathConfig.HOUSE);
        }

        private void errorMessage(string message)
        {
            tsttAdmin.Text = service.getError(message);
            tsttAdmin.ForeColor = Color.DarkRed;
            sttAdmin.Refresh();
        }

        private void successMessage(string message)
        {
            tsttAdmin.Text = service.getError(message);
            tsttAdmin.ForeColor = Color.Green;
            sttAdmin.Refresh();
        }

        private void btnXmlSandChk_Click(object sender, EventArgs e)
        {
            checkFileExists(txtXMLSand.Text);
        }

        private bool checkFileExists(string filePath)
        {
            if (File.Exists(filePath))
            {
                successMessage(ErrorCodes.m0029);
                return true;
            }
            else
            {
                errorMessage(ErrorCodes.e0031);
                return false;
            }
        }

        private void btnXmlSave_Click(object sender, EventArgs e)
        {
            if(txtXMLSand.Text == "" || txtXMLHouse.Text == "") {
                errorMessage(ErrorCodes.e0014);
                return;
            }
            if (!checkFileExists(txtXMLSand.Text) 
                || !checkFileExists(txtXMLHouse.Text))
            {
                return;
            }

            if (service.modifyXmlDbPath(txtXMLSand.Text, txtXMLHouse.Text))
            {
                successMessage(ErrorCodes.m0006);
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
                return;
            }
        }

        private void cbDAOList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
                dgvXpath.DataSource = dt;
            }
            else
            {
                successMessage(ErrorCodes.m0001);
                dgvXpath.DataSource = dt;
                dgvXpath.Columns[0].Width = 100;
                dgvXpath.Columns[1].Width = 300;
                dgvXpath.Columns[2].Width = 75;
            }

            
        }

        private void btnSaveXpath_Click(object sender, EventArgs e)
        {
            if (txtMethod.Text == "")
            {
                errorMessage(ErrorCodes.e0017);
                txtMethod.Focus();
                return;
            }

            if (txtPath.Text == "")
            {
                errorMessage(ErrorCodes.e0030);
                txtPath.Focus();
                return;
            }

            if (txtParams.Text == "")
            {
                errorMessage(ErrorCodes.e0021);
                txtParams.Focus();
                return;
            }

            XMLXpathConfig xmlXpath = new XMLXpathConfig()
            {
                Method = txtMethod.Text,
                Path = txtPath.Text,
                NofParams = int.Parse(txtParams.Text),
                Dao = cbDAOList.SelectedItem.ToString()
            };

            if (!service.methodExist(xmlXpath))
            {
                if (service.addXpath(xmlXpath))
                {
                    successMessage(ErrorCodes.m0027);
                    DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
                    dgvXpath.DataSource = dt;
                    dgvXpath.Columns[0].Width = 100;
                    dgvXpath.Columns[1].Width = 300;
                    dgvXpath.Columns[2].Width = 75;

                    clearXpathFields();
                }
                else
                {
                    errorMessage(ErrorCodes.e0011);
                }
            }
            else
            {
                if (service.modifyXpath(xmlXpath))
                {
                    successMessage(ErrorCodes.m0028);
                    DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
                    dgvXpath.DataSource = dt;
                    dgvXpath.Columns[0].Width = 100;
                    dgvXpath.Columns[1].Width = 300;
                    dgvXpath.Columns[2].Width = 75;

                    clearXpathFields();
                }
                else
                {
                    errorMessage(ErrorCodes.e0011);
                }
            }
        }

        private void clearXpathFields()
        {
            txtMethod.Clear();
            txtPath.Clear();
            txtParams.Clear();
        }

        private void txtParams_KeyPress(object sender, KeyPressEventArgs e)
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
        }

        private void btnRemoveXpath_Click(object sender, EventArgs e)
        {
            string method = dgvXpath.CurrentRow.Cells[0].Value.ToString();

            XMLXpathConfig xmlXpath = new XMLXpathConfig();
            xmlXpath.Method = method;

            if (service.removeXpath(xmlXpath))
            {
                successMessage(ErrorCodes.e0031);
                DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
                dgvXpath.DataSource = dt;

                clearXpathFields();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }

        }

        private void dgvXpath_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvXpath.CurrentCell != null)
            {
                txtMethod.Text = dgvXpath.CurrentRow.Cells[0].Value.ToString();
                txtPath.Text = dgvXpath.CurrentRow.Cells[1].Value.ToString();
                txtParams.Text = dgvXpath.CurrentRow.Cells[2].Value.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void btnSaveErr_Click(object sender, EventArgs e)
        {
            if (txtErrDesc.Text == "")
            {
                errorMessage(ErrorCodes.e0010);
                txtMethod.Focus();
                return;
            }

            Error error = new Error()
            {
                Code = txtErrCde.Text,
                Description = txtErrDesc.Text
            };

            if (!service.errorExist(txtErrCde.Text))
            {
                if (service.addError(error))
                {
                    successMessage(ErrorCodes.m0007);
                    DataTable dt = searchError(txtErrCodeSrch.Text,
                        txtErrDescSrch.Text);
                    dgvError.DataSource = dt;

                    clearXpathFields();
                }
                else
                {
                    errorMessage(ErrorCodes.e0022);
                }
            }
            else
            {
                if (service.modifyError(error))
                {
                    successMessage(ErrorCodes.m0009);
                    DataTable dt = searchError(txtErrCodeSrch.Text, 
                        txtErrDescSrch.Text);
                    dgvError.DataSource = dt;

                    clearXpathFields();
                }
                else
                {
                    errorMessage(ErrorCodes.e0022);
                }
            }
        }

        private DataTable searchError(string code, string desc)
        {
            DataTable dt = service.getErrorList();
            dt.DefaultView.RowFilter = "Code like '%" + code +
                "%' AND Description like '%" + desc + "%'";
            return dt;
        }

        private void btnErrSearch_Click(object sender, EventArgs e)
        {
            DataTable dt = searchError(txtErrCodeSrch.Text,
                        txtErrDescSrch.Text);
            
            if (dt.Rows.Count == 0)
            {
                errorMessage(ErrorCodes.e0001);
            }
            else
            {
                successMessage(ErrorCodes.m0001);
            }

            dgvError.DataSource = dt;
            dgvError.Columns[0].Width = 50;
            dgvError.Columns[1].Width = 500;
        }

        private void btnRmvErr_Click(object sender, EventArgs e)
        {
            string code = dgvError.CurrentRow.Cells[0].Value.ToString();

            if (service.removeError(code))
            {
                successMessage(ErrorCodes.m0009);
                DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
                dgvError.DataSource = dt;

                clearErrorFields();
            }
            else
            {
                errorMessage(ErrorCodes.e0011);
            }
        }

        private void clearErrorFields()
        {
            txtErrCde.Clear();
            txtErrDesc.Clear();
        }

        private void rbError_CheckedChanged(object sender, EventArgs e)
        {
            if (rbError.Checked)
            {
                rbMes.Checked = false;
                txtErrCde.Text = service.generateErrorCode(Error.ERROR_PREFIX);
            }
        }

        private void rbMes_CheckedChanged(object sender, EventArgs e)
        {
            if (rbMes.Checked)
            {
                rbError.Checked = false;
                txtErrCde.Text = service.generateErrorCode(Error.MESSAGE_PREFIX);
            }
        }

        private void dgvError_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvError.CurrentCell != null)
            {
                txtErrCde.Text = dgvError.CurrentRow.Cells[0].Value.ToString();
                txtErrDesc.Text = dgvError.CurrentRow.Cells[1].Value.ToString();
            }
            else
            {
                errorMessage(ErrorCodes.e0022);
            }
        }

        private void AdminConfig_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmLogin.Show();
        }

        private void AdminConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmLogin.Show();
        }

        private void btnOutChk_Click(object sender, EventArgs e)
        {
            checkFolderExist(txtExcelOut.Text);
        }

        private bool checkFolderExist(string folderPath)
        {
            if (Directory.Exists(folderPath))
            {
                successMessage(ErrorCodes.m0013);
                return true;
            }
            else
            {
                errorMessage(ErrorCodes.e0025);
                return false;
            }
        }

        private void btnXmlHouseChk_Click(object sender, EventArgs e)
        {
            checkFileExists(txtXMLHouse.Text);
        }

        private void btnExcelSave_Click(object sender, EventArgs e)
        {
            if (checkFolderExist(txtExcelOut.Text))
            {
                ExcelOutput mdfExcel = new ExcelOutput()
                {
                    Path = txtExcelOut.Text,
                    Extension = txtExtn.Text,
                    DayRangeFilename = txtDayRnRpFIleName.Text,
                    DayRangeParams = int.Parse(txtDayRnRpParams.Text),
                    DayRangeTitle = txtDayRnRpTitle.Text,
                    DateFilename = txtDateRpFileName.Text,
                    DateParams = int.Parse(txtDateRpParams.Text),
                    DateTitle = txtDateRpTitle.Text,
                    MonthlyFilename = txtMthRpFileName.Text,
                    MonthlyParams = int.Parse(txtMthRpParams.Text),
                    MonthlyTitle = txtMthRpTitle.Text,
                    DateFormat = txtDateFormat.Text
                };

                if (service.modifyExcelConfig(mdfExcel))
                {
                    successMessage(ErrorCodes.m0010);
                }
                else
                {
                    errorMessage(ErrorCodes.e0012);
                }
            }
        }

        private void btnImportErr_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Choose File to import";
            openFileDialog1.Filter = "CSV File (*.csv)| *.csv";
            DialogResult dlgRes = openFileDialog1.ShowDialog();
            if (dlgRes == DialogResult.OK)
            {
                int count = 0;
                int countImp = 0;
                string inputPath = openFileDialog1.FileName;
                StreamReader reader = new StreamReader(inputPath);
                string line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    if(data.Length != 2) {
                        continue;
                    }

                    if(data.Length == 2 && (data[0] == "" || data[1] == "")) {
                        continue;
                    }

                    Error error = new Error()
                    {
                        Code = service.generateErrorCode(data[1]),
                        Description = data[0]
                    };

                    if (service.addError(error))
                    {
                        countImp++;
                    }
                    count++;
                }

                DataTable dt = service.getXpathList(cbDAOList.SelectedItem.ToString());
                dgvError.DataSource = dt;

                successMessage("Imported " + countImp + "/" + count + " errors successfully!");
            }
        }

        private void btnExportAllErr_Click(object sender, EventArgs e)
        {
            string outputFile = txtExcelOut.Text + "//errorexportAll.txt";
            DataTable dt = service.getErrorList();
            dt.DefaultView.Sort = "Code ASC"; 
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                foreach (DataRow error in dt.DefaultView.ToTable().Rows)
                {
                    writer.WriteLine("/// <summary>");
                    writer.WriteLine("///\t" + error["Description"]);
                    writer.WriteLine("/// </summary>");
                    writer.WriteLine("public static string " + error["Code"].ToString().ToLower()
                        + " = \"" + error["Code"].ToString().ToUpper() + "\"; ");
                    writer.WriteLine(" ");
                }
            }
        }

        private void btnExportSelect_Click(object sender, EventArgs e)
        {
            string outputFile = txtExcelOut.Text + "//errorexportSel.txt";
            if (dgvError.SelectedRows == null || dgvError.SelectedRows.Count == 0)
            {
                errorMessage(ErrorCodes.e0022);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (DataGridViewRow error in dgvError.SelectedRows)
                    {
                        writer.WriteLine("/// <summary>");
                        writer.WriteLine("///\t" + error.Cells[1].Value.ToString());
                        writer.WriteLine("/// </summary>");
                        writer.WriteLine("public static string " + error.Cells[0].Value.ToString().ToLower()
                        + " = \"" + error.Cells[0].Value.ToString().ToUpper() + "\";");
                        writer.WriteLine(" ");
                    }
                }
            }
        }

        private void btnXpathExportSel_Click(object sender, EventArgs e)
        {
            string outputFile = txtExcelOut.Text + "//xpathexportSel_" + cbDAOList.SelectedItem.ToString() + ".txt";
            if (dgvXpath.SelectedRows == null || dgvXpath.SelectedRows.Count == 0)
            {
                errorMessage(ErrorCodes.e0022);
            }
            else
            {
                using (StreamWriter writer = new StreamWriter(outputFile))
                {
                    foreach (DataGridViewRow xpath in dgvXpath.SelectedRows)
                    {
                        writer.WriteLine("string xpath = AdminConfigDAO.getXpath(");
                        writer.WriteLine("\t\t\tXmlAdminConfig.getInstance(), this.GetType().Name,");
                        int noParam = int.Parse(xpath.Cells[2].Value.ToString());
                        if (noParam == 0)
                        {
                            writer.WriteLine("\t\t\t\"" + xpath.Cells[0].Value.ToString() + "\").Path;");
                        }
                        else
                        {
                            writer.WriteLine("\t\t\t\"" + xpath.Cells[0].Value.ToString() + "\").Path");
                            for (int i = 0; i < noParam; i++)
                            {
                                if (i == noParam - 1)
                                {
                                    writer.WriteLine("\t\t\t.Replace(\"{" + i + "}\", p);");
                                }
                                else
                                {
                                    writer.WriteLine("\t\t\t.Replace(\"{" + i + "}\", p)");
                                }
                            }
                        }
                        
                        writer.WriteLine(" ");
                    }
                }
            }
        }

    }
}
