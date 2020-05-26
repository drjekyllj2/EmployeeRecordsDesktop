using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeRecordsDesktop
{
    public class MyErrorProvider : ErrorProvider
    {

        public int ErrorCount { get; set; }

        public void SetError(Control control)
        {
            SetError(control, "Field required");
        }

        new public void SetError(Control control, string value)
        {
            ErrorCount++;
            base.SetError(control, value);
        }

        public void SetError(Control control, string value, ErrorIconAlignment iconAlignment)
        {
            SetError(control, value);
            base.SetIconAlignment(control, iconAlignment);
        }
        public void SetError(Control control, string value, ErrorIconAlignment iconAlignment, bool inside)
        {
            SetError(control, value, iconAlignment);
            if (inside)
                base.SetIconPadding(control, -30);
        }
        new public void Clear()
        {
            ErrorCount = 0;
            base.Clear();
        }
    }
}
