using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRecordsDesktop
{
    public partial class FormCreateEmployee : Form
    {
        MyErrorProvider err = new MyErrorProvider();
        DataHelper DataAccess = new DataHelper();
        public string Employee_ID=string.Empty;
        private readonly FormEmployeeList _frmemplist;
        public FormCreateEmployee()
        {
            InitializeComponent();
            
            
        }

        public FormCreateEmployee(FormEmployeeList frmemplist)

        {
            InitializeComponent();
            _frmemplist = frmemplist;
            
        }

    private void btnAdd_Click(object sender, EventArgs e)
        {
            err.Clear();

            if (Save())
            {
                MessageBox.Show("Record successfully save!");


                if (Employee_ID == string.Empty)
                {
                    this.txtFirstName.Clear();
                    this.txtLastName.Clear();
                    this.txtMiddleName.Clear();
                    this.txtID.Text = EmployeeID();
                    this.txtAddress.Clear();
                    this.dtehired.Value = DateTime.Now;
                    this.dtedob.Value = DateTime.Now;
                    this.cmbStatus.SelectedText = "";
                }

                else
                {
                    _frmemplist.LoadEmployees();
                    Close();
                }
            }


        }



        private bool Save()
        {
           // DataHelper InsertData = new DataHelper();

            

            if (txtFirstName.Text == "")
                err.SetError(txtFirstName, "First Name is Required!");
            if (txtMiddleName.Text == "")
                err.SetError(txtMiddleName, "Middle Name is Required!");
            if (txtLastName.Text == "")
                err.SetError(txtLastName, "Last Name is Required!");
            if (txtAddress.Text == "")
                err.SetError(txtAddress, "Address is Required!");

            var DateNotFuture = dtehired.Value.CompareTo(DateTime.Now);
            var DobQualified = DateTime.Now.Year - dtedob.Value.Year;

            if (DateNotFuture == 1)
                err.SetError(dtehired, "Date Hired should not be a future date!");

            if (DobQualified < 18)
                err.SetError(dtedob, "Age should be at least 18 years old!");

            if (err.ErrorCount > 0)
            {
                //if any error preset, abort
                err.SetError(btnAdd, "Record not saved", ErrorIconAlignment.MiddleLeft);

                return false;
            }


            string[] StoredProcParam = new string[9];

            

            StoredProcParam[0] = this.txtID.Text;
            StoredProcParam[1] = this.txtFirstName.Text;
            StoredProcParam[2] = this.txtMiddleName.Text;
            StoredProcParam[3] = this.txtLastName.Text;
            StoredProcParam[4] = GetGender();
            StoredProcParam[5] = this.dtedob.Value.ToString();
            StoredProcParam[6] = this.dtehired.Value.ToString();
            StoredProcParam[7] = this.cmbStatus.SelectedValue.ToString();
            StoredProcParam[8] = this.txtAddress.Text;


            if (Employee_ID==string.Empty)
                DataAccess.Execute(Constant.AddNewRecordStoredProc, StoredProcParam);
            else
                DataAccess.Execute(Constant.UpdateEmployeeStoredProc, StoredProcParam);



            return true;

        }


        private string GetGender()
        {
            if (optMale.Checked)
                return "M";
            else
                return "F";
        }
        private bool GetGenderMale(string Val)
        {
            if (Val == "M")
                return true;
            else return false;

        }
        private bool GetGenderFemale(string Val)
        {
            if (Val == "F")
                return true;
            else return false;

        }
        private string EmployeeID()

        {
            


            var EmpID = DataAccess.GetData(Constant.GetEmployeeIDStoredProc);

            return EmpID.ToString();

        }

        private void FormCreateEmployee_Load(object sender, EventArgs e)
        {


            if (Employee_ID == string.Empty)
            {
                this.txtID.Text = EmployeeID();
                optMale.Checked = true;
                LoadStatus();
            }

            else
            {
                LoadData();
                cmbStatus.Focus();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void LoadStatus()
        {
            cmbStatus.DataSource = DataAccess.GetAllDataTable(Constant.GetStatusStoredProc, 0).Tables[0];
            cmbStatus.DisplayMember = "EmpStatusDescription";
            cmbStatus.ValueMember = "EmpStatusCode";
        }
        private void LoadData()
        {
            var Statusval = string.Empty;
            
            foreach (DataRow row in DataAccess.GetAllDataTable(Constant.GetEmployeeListStoredProc,Employee_ID).Tables[0].Rows)
            {
                 txtID.Text = row["EmployeeID"].ToString();
               txtFirstName.Text= row["FirstName"].ToString();
               txtLastName.Text = row["LastName"].ToString();
                txtMiddleName.Text = row["MiddleName"].ToString();
                txtAddress.Text = row["Address"].ToString();
                dtedob.Value = Convert.ToDateTime( row["DateofBirth"].ToString());
                dtehired.Value = Convert.ToDateTime(row["DateHired"].ToString());
                optMale.Checked = GetGenderMale(row["Gender"].ToString());
                optFemale.Checked = GetGenderFemale(row["Gender"].ToString());
                Statusval = row["Status"].ToString();
                //cmbStatus.SelectedText = row["Status"].ToString();
                //cmbStatus.DisplayMember = row["Status"].ToString();
                //cmbStatus.ValueMember = row["EmpStatusCode"].ToString();


            }
            LoadStatus();
            cmbStatus.SelectedIndex = cmbStatus.FindString(Statusval);


        }

        private void cmbStatus_Click(object sender,EventArgs e)
        {
            LoadStatus();
        }
    }
}
