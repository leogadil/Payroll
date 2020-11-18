using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace PayrollApp
{
    class EmployeeManager : ErrorHandling
    {

        string path = Path.Combine(Environment.GetFolderPath(
        Environment.SpecialFolder.MyDoc‌​uments), "PayRoool");
        Dictionary<long, Employee> EmployeeList = new Dictionary<long, Employee>();

        public void importCSV(Label status)
        {
            if (File.Exists(Path.Combine(path, "employeelist.csv")))
            {
                status.Text = "Employee List found";
                status.ForeColor = Color.WhiteSmoke;
                status.Visible = true;

                using (StreamReader sr = new StreamReader(Path.Combine(path,"employeelist.csv")))
                {
                    EmployeeList.Clear();
                    string headerLine = sr.ReadLine();
                    string currentLine;

                    while ((currentLine = sr.ReadLine()) != null)
                    {
                        string[] dataSplit = currentLine.Split(',');
                        if (dataSplit.Length >= 0)
                        {
                            EmployeeList.Add(Convert.ToInt64(dataSplit[0]), new Employee() {
                                id = Convert.ToInt64(dataSplit[0]),
                                firstname = dataSplit[1],
                                lastname = dataSplit[2],
                                position = dataSplit[3],
                                baserate = Convert.ToInt64(dataSplit[4]),
                                haveSSS = Convert.ToBoolean(dataSplit[5]),
                                havePhilHealth = Convert.ToBoolean(dataSplit[6]),
                                havePagIbig = Convert.ToBoolean(dataSplit[7]) });
                        }

                    }
                }

            } else
            {
                status.Text = "Employee List not found";
                status.ForeColor = Color.Firebrick;
                status.Visible = true;
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

        public Employee findEmployee(string key)
        {
            return EmployeeList[Convert.ToInt64(key)];
        }

        public void destroyEmployee(string key)
        {
            EmployeeList.Remove(Convert.ToInt64(key));
            exportCSV();
        }

        public void update(DataGridView table)
        {
            TableManager tm = new TableManager();
            tm.updateTable(table, EmployeeList);
        }

        public void createEmployee(
            long ID, string FirstName, string LastName, string Position,
            double BaseSalary, bool HaveSSS, bool HavePhilHealth, bool HavePagIbig, double TotalSalary = 0 )
        {
            if (!EmployeeList.ContainsKey(ID)) {
                EmployeeList.Add(ID, new Employee() { 
                    id = ID,
                    firstname = FirstName,
                    lastname = LastName,
                    position = Position,
                    baserate = BaseSalary,
                    haveSSS = HaveSSS,
                    havePhilHealth = HavePhilHealth,
                    havePagIbig = HavePagIbig,
                    totalsalary = TotalSalary});
            } else {
                report("Id has a duplicate on the table.");
            }
            exportCSV();
        }

        public void updateEmployee(
            long ID, string FirstName, string LastName, string Position,
            double BaseRate, bool HaveSSS, bool HavePhilHealth, bool HavePagIbig)
        {
            if (EmployeeList.ContainsKey(ID))
            {
                EmployeeList[ID] = new Employee() { 
                    id = ID,
                    firstname = FirstName,
                    lastname = LastName,
                    position = Position,
                    baserate = BaseRate,
                    haveSSS = HaveSSS,
                    havePhilHealth = HavePhilHealth,
                    havePagIbig = HavePagIbig };
            }
            else
            {
                report("Id is not on the table.");
            }
            exportCSV();
        }

    }

    public class Employee
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string position { get; set; }
        public double baserate { get; set; }
        public bool haveSSS { get; set ; }
        public bool havePhilHealth { get; set; }
        public bool havePagIbig { get; set; }
        public double totalsalary { get; set; }
        public int totalovertime { get; set; }
        public int totalleave { get; set; }
    }
}
