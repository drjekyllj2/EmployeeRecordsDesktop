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
    public partial class FormEmployeeStatus : Form
    {
        MyErrorProvider err = new MyErrorProvider();
        public FormEmployeeStatus()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool Save()
        {
            DataHelper InsertData = new DataHelper();

           

            if (txtstatus.Text == "")
                err.SetError(txtstatus, "Status is Required!");
            


            if (err.ErrorCount > 0)
            {
                //if any error preset, abort
                err.SetError(btnSave, "Record not saved", ErrorIconAlignment.MiddleLeft);

                return false;
            }


            


            InsertData.Execute( Constant.AddNewStatusStoredProc,"@statusid",txtstatus.Text);


           
            return true;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            err.Clear();
            //Save();
            if (Save())
            {
                MessageBox.Show("New Status successfully save.");
            }
            txtstatus.Clear();
        }
    }
}
