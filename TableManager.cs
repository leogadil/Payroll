using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApp
{
    class TableManager
    {
        public void updateTable(DataGridView table, Dictionary<long, Employee> EmployeeList, bool payroll = false)
        {
            table.AutoResizeColumns();
            table.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataTable datatable = new DataTable();
            datatable.Columns.Add("ID", typeof(long));
            datatable.Columns.Add("First Name", typeof(string));
            datatable.Columns.Add("Last Name", typeof(string));
            datatable.Columns.Add("Position", typeof(string));
            datatable.Columns.Add("Pay/Day", typeof(long));
            datatable.Columns.Add("SSS", typeof(bool));
            datatable.Columns.Add("PhilHealth", typeof(bool));
            datatable.Columns.Add("Pag-Ibig", typeof(bool));
            if (payroll)
            {
                datatable.Columns.Add("Current Salary", typeof(double));
            }

            foreach (KeyValuePair<long, Employee> employee in EmployeeList)
            {
                var index = employee.Value;
                DataRow datarow = datatable.NewRow();
                datarow["ID"] = employee.Key;
                datarow["First Name"] = index.firstname;
                datarow["Last Name"] = index.lastname;
                datarow["Position"] = index.position;
                datarow["Pay/Day"] = index.baserate;
                datarow["SSS"] = index.haveSSS;
                datarow["PhilHealth"] = index.havePhilHealth;
                datarow["Pag-Ibig"] = index.havePagIbig;
                if (payroll)
                {
                    datarow["Current Salary"] = index.totalsalary;
                }
                    datatable.Rows.Add(datarow);
            };

            table.DataSource = datatable;
            table.CurrentCell = null;
        }
    }
}
