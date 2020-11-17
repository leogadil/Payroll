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
    }
}
