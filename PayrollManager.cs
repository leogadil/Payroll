using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApp
{
    class PayrollManager : ErrorHandling
    {

        private int totalnumberofdaysofworked = 0;
        private double totalgross = 0;
        private double netsalary = 0;
        private double totaldeduction = 0;
        private int employeeid = 0;
        private int overtime = 0;
        private int totalworkedpay = 0;

        private bool computed = false;

        string path = System.IO.Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.MyDoc‌​uments), "PayRoool");
        Dictionary<long, Employee> EmployeeList = new Dictionary<long, Employee>();

        public void importCSV(Label status = null)
        {
            EmployeeList.Clear();
            if (File.Exists(Path.Combine(path, "employeelist.csv")))
            {
                if(status != null)
                {
                    status.Text = "Employee List found";
                    status.ForeColor = Color.WhiteSmoke;
                    status.Visible = true;
                }
                

                using (StreamReader sr = new StreamReader(Path.Combine(path, "employeelist.csv")))
                {
                    EmployeeList.Clear();
                    string headerLine = sr.ReadLine();
                    string currentLine;

                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] dataSplit = currentLine.Split(',');
                        if (dataSplit.Length >= 0)
                        {
                            EmployeeList.Add(Convert.ToInt64(dataSplit[0]), new Employee()
                            {
                                id = Convert.ToInt64(dataSplit[0]),
                                firstname = dataSplit[1],
                                lastname = dataSplit[2],
                                position = dataSplit[3],
                                baserate = Convert.ToInt64(dataSplit[4]),
                                haveSSS = Convert.ToBoolean(dataSplit[5]),
                                havePhilHealth = Convert.ToBoolean(dataSplit[6]),
                                havePagIbig = Convert.ToBoolean(dataSplit[7]),
                                totalsalary = Convert.ToInt64(dataSplit[8])
                            });
                        }

                    }
                }

            }
            else
            {
                if (status != null)
                {
                    status.Text = "Employee List not found";
                    status.ForeColor = Color.Firebrick;
                    status.Visible = true;
                }
            }
        }

        private void exportCSV()
        {
            StringBuilder sb = new StringBuilder();

            String csv = String.Join(
                Environment.NewLine,
               $"id,firstname,lastname,position,baserate,haveSSS,havePhilHealth,havePagIbig,totalsalary"
            );

            sb.Append(csv + Environment.NewLine);

            String csv2 = String.Join(
                Environment.NewLine,
                EmployeeList.Select(d => $"{d.Key},{d.Value.firstname},{d.Value.lastname},{d.Value.position},{d.Value.baserate},{d.Value.haveSSS},{d.Value.havePhilHealth},{d.Value.havePagIbig},{d.Value.totalsalary}")
            );
            sb.Append(csv2);

            bool exist = Directory.Exists(path);

            if (!exist)
                Directory.CreateDirectory(path);

            FileInfo emlist = new FileInfo(Path.Combine(path, "employeelist.csv"));
            StreamWriter namewriter = emlist.CreateText();
            namewriter.Write(sb.ToString());
            namewriter.Close();
        }

        public void updateTotalDaysWorked(DateTimePicker d1, DateTimePicker d2, int leave, Label text)
        {
            DateTime firstdate = d1.Value;
            DateTime enddate = d2.Value;

            TimeSpan tspan = enddate - firstdate;

            int total = (tspan.Days - leave) + 1;
            if(total < 0) { total = 0; }
            totalnumberofdaysofworked = total;
            text.Text = "Total Days Worked: " + total.ToString() + " Days";
        }

        public void computePayday(int id, int payrate, int overtimerate, int totalovertimehours, double deduction, Label text1, Label text2, Label text3, Button computebutton)
        {
            if (employeeid != id && computed)
            {
                if (askagain("You didn't save your last computation. ignore it?")) return;
            }

            employeeid = id;
            int totalworkedpay = payrate * totalnumberofdaysofworked;
            overtime = overtimerate * totalovertimehours;
            double grosstemp = totalworkedpay + overtime;
            totalgross = grosstemp;
            text1.Text = "Gross Salary: P " + totalgross.ToString();
            totaldeduction = (deduction * grosstemp) / 100;
            text2.Text = "Total Deduction: P " + Math.Round(totaldeduction, 2).ToString();
            netsalary = totalworkedpay - totaldeduction;
            text3.Text = "Net Salary: P " + Math.Round(netsalary, 2).ToString();
            computed = true;
            computebutton.Enabled = true;
        }


        public void savePayday(Button btn)
        {
            if(computed)
            {
                EmployeeList[Convert.ToInt64(employeeid)].totalsalary = Math.Ceiling(netsalary);
                EmployeeList[Convert.ToInt64(employeeid)].totalovertime = overtime;
                computed = false;
                btn.Enabled = false;
                exportCSV();
            }
        }

        public Employee findEmployee(string key)
        {
            return EmployeeList[Convert.ToInt64(key)];
        }

        public void update(DataGridView table)
        {
            importCSV();
            TableManager tm = new TableManager();
            tm.updateTable(table, EmployeeList, true);
        }
    }
}
