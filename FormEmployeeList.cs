using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

namespace EmployeeRecordsDesktop
{
    public partial class FormEmployeeList : Form
    {
        DataHelper DataAccess = new DataHelper();
        DataTable EmpData = new DataTable();
        System.Timers.Timer timer;
        public FormEmployeeList()
        {
            InitializeComponent();
        }

        private void FormEmployeeList_Load(object sender, EventArgs e)


        {
            timer = new System.Timers.Timer();
            timer.Interval = 60000;

            timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();

            cmbSearchby.SelectedItem = "Employee ID";

            //this.WindowState = FormWindowState.Maximized;
            //dtaGrdEmp.Height = this.Height - 120;


            LoadEmployees();
        }

        public void LoadEmployees()
        {
            try
            {

               EmpData = DataAccess.GetAllDataTable(Constant.GetEmployeeListStoredProc, 0).Tables[0];

                dtaGrdEmp.Invoke(new Action(() => this.dtaGrdEmp.DataSource = EmpData));// DataAccess.GetAllDataTable(Constant.GetEmployeeListStoredProc, 0).Tables[0]));

               // this.dtaGrdEmp.DataSource = EmpData;// DataAccess.GetAllDataTable(Constant.GetEmployeeListStoredProc, 0).Tables[0];
            }

            catch
            {

            }


        }
        private  void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            LoadEmployees();
        }

        private void btnSearch_Click(object sender, EventArgs e)


        {
           
             
            
            if(cmbSearchby.SelectedItem.ToString()=="Employee ID")
             Filter(string.Format("{0} = '{1}'", "EmployeeID", txtSearch.Text.Trim()));

            if (cmbSearchby.SelectedItem.ToString() == "Last Name")
                Filter(string.Format("{0} LIKE '%{1}%'", "LastName", txtSearch.Text.Trim()));

            if (cmbSearchby.SelectedItem.ToString() == "First Name")
                Filter(string.Format("{0} LIKE '%{1}%'", "FirstName", txtSearch.Text.Trim()));
 


        }

        private void Filter(string Value)
        {
            EmpData.DefaultView.RowFilter = Value;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbSearchby.Text = "";
            txtSearch.Text = "";
            EmpData.DefaultView.RowFilter = string.Empty;
        }

        private void dtaGrdEmp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==1)
            {
               var id = Convert.ToString(dtaGrdEmp.Rows[e.RowIndex].Cells["EmployeeID"].Value);

                FormCreateEmployee fcreate = new FormCreateEmployee(this);
                fcreate.Employee_ID =  id;
                fcreate.Show(this);

            }

            if (e.ColumnIndex == 0)
            {
                var id = Convert.ToString(dtaGrdEmp.Rows[e.RowIndex].Cells["EmployeeID"].Value);

                if(MessageBox.Show("You are about to delete this record, continue anyway?","Delete Record",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
                {
                    DataHelper DataAcess = new DataHelper();

                    DataAccess.Execute(Constant.DeleteEmployeeStoredProc,"@Empid",  id);
                    LoadEmployees();
                    MessageBox.Show("Employee ID: " + id + " has been successfully deleted!");
                }
                

            }
        }
    }
}
