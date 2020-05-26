using System.Windows.Forms;

namespace EmployeeRecordsDesktop
{
    public partial class Main : Form
    {
        

        public Main()
        {
            InitializeComponent();
        }

        private void CreateEmpMenuItem_Click(object sender, System.EventArgs e)
        {
            FormCreateEmployee frmCreate = new FormCreateEmployee();
            frmCreate.MdiParent = this;
            frmCreate.Show();
        }

        private void EmpStatusMenuItem_Click(object sender, System.EventArgs e)
        {
            FormEmployeeStatus frmstat = new FormEmployeeStatus();
            frmstat.MdiParent = this;
            frmstat.Show();
        }

        private void Main_Load(object sender, System.EventArgs e)
        {
            FormEmployeeList frmlist = new FormEmployeeList();
            frmlist.MdiParent = this;
            frmlist.Show();
        }

        private void EmployeeList_Click(object sender, System.EventArgs e)
        {
            FormEmployeeList frmlist = new FormEmployeeList();
            frmlist.MdiParent = this;
            frmlist.Show();
        }
    }
}
