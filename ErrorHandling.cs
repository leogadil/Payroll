using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApp
{
    class ErrorHandling
    {
        public void report(string error)
        {
            MessageBox.Show(error, "Payroool", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public bool askagain(string error)
        {
            DialogResult dia = MessageBox.Show(error, "Payroool", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Error);
            if(dia == DialogResult.Yes)
            {
                return true;
            } else
            {
                return true;
            }
        }
    }
}
