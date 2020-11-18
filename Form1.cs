using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApp
{
    public partial class Form1 : Form
    {
        private int currentid = 0;

        public static Random RandomGen = new Random();
        int clickPercentage = 90;

        EmployeeManager Emanager = new EmployeeManager();
        PayrollManager Pmanager = new PayrollManager();
        ErrorHandling ErrHandling = new ErrorHandling();

        public Form1()
        {
            InitializeComponent();
            Emanager.importCSV(statusText);
            Pmanager.importCSV(statusText);
            dataGridView1.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#6cb4c4");
            dataGridView2.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#6cb4c4");
            linkLabel1.LinkColor = ColorTranslator.FromHtml("#6cb4c4");
            linkLabel2.LinkColor = ColorTranslator.FromHtml("#6cb4c4");
            linkLabel3.LinkColor = ColorTranslator.FromHtml("#6cb4c4");
            button7.FlatAppearance.BorderColor = Color.WhiteSmoke;
            button8.FlatAppearance.BorderColor = Color.WhiteSmoke;
            button9.FlatAppearance.BorderColor = Color.WhiteSmoke;
            button10.FlatAppearance.BorderColor = Color.WhiteSmoke;
        }

        private void EmployeeButton_Click(object sender, EventArgs e)
        {
            this.TabControl.SelectedIndex = 1;
        }

        private void PayrollButton_Click(object sender, EventArgs e)
        {
            this.TabControl.SelectedIndex = 2;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.TabControl.SelectedIndex = 3;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.TabControl.SelectedIndex = 4;
        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                Emanager.createEmployee(
                    Convert.ToInt64(id_textbox.Text),
                    firstname_textbox.Text,
                    lastnam_textbox.Text,
                    position_textbox.Text,
                    Convert.ToDouble(basesalary_textbox.Text),
                    sss_checkbox.Checked,
                    pagibig_checbox.Checked,
                    philhealth_checkbox.Checked
                    );

                id_textbox.Clear();
                firstname_textbox.Clear();
                lastnam_textbox.Clear();
                position_textbox.Clear();
                basesalary_textbox.Clear();
                sss_checkbox.Checked = false;
                pagibig_checbox.Checked = false;
                philhealth_checkbox.Checked = false;
            }
            catch (Exception err)
            {
                ErrHandling.report(err.Message);
            }

            Emanager.update(dataGridView1);
        }

        private void groupBox2_Leave(object sender, EventArgs e)
        {
            //dataGridView1.ClearSelection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(viewerid.Text))
            {
                Emanager.destroyEmployee(viewerid.Text);
                Emanager.update(dataGridView1);

                viewerid.Clear();
                viewerfirstname.Clear();
                viewerlastname.Clear();
                viewerposition.Clear();
                viewersalary.Clear();
                viewersss.Checked = false;
                viewerphil.Checked = false;
                viewerlove.Checked = false;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                Emanager.updateEmployee(
                    Convert.ToInt64(viewerid.Text),
                    viewerfirstname.Text,
                    viewerlastname.Text,
                    viewerposition.Text,
                    Convert.ToDouble(viewersalary.Text),
                    viewersss.Checked,
                    viewerphil.Checked, 
                    viewerlove.Checked
                    );

                viewerid.Clear();
                viewerfirstname.Clear();
                viewerlastname.Clear();
                viewerposition.Clear();
                viewersalary.Clear();
                viewersss.Checked = false;
                viewerphil.Checked = false;
                viewerlove.Checked = false;
            }
            catch (Exception err)
            {
                ErrHandling.report(err.Message);
            }

            Emanager.update(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchint.Text = "No Result";
            dataGridView1.ClearSelection();
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value != null)
                {
                    for (int i = 0; i < dataGridView1.ColumnCount - 1; i++)
                    {
                        if (row.Cells[i].Value.ToString() != null && row.Cells[i].Value.ToString().ToLower().Equals(searchbar.Text))
                        {
                            rowIndex = row.Index;
                            dataGridView1.Rows[rowIndex].Selected = true;
                            searchint.Text = "Found it";
                            break;
                        }
                    }
                }
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if(TabControl.SelectedIndex == 3)
            {
                for (int i = 0; i < 100; i++)
                {
                    int randomValueBetween0And99 = RandomGen.Next(100);
                    if (randomValueBetween0And99 < clickPercentage)
                    {
                        label34.Text = "OH, I'M SORRY, DO YOU NEED ANYTHING? WE HAVE NOTHING HERE FOR YOU." + Environment.NewLine +  "SHU SHU";
                    } else
                    {
                        label34.Text = "OH, I'M SORRY, DO YOU NEED ANYTHING? WE HAVE NOTHING HERE FOR YOU." + Environment.NewLine + "SHITZU";
                    }
                }
            }
            if (TabControl.SelectedIndex <= 0)
            {
                Emanager.importCSV(statusText);
            }
            if(TabControl.SelectedIndex == 2)
            {
                Emanager.importCSV(statusText);
            }

            Emanager.update(dataGridView1);
            Pmanager.update(dataGridView2);
        }

        private void id_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void basesalary_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void viewerid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void viewersalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/leogadil/Payroll");
        }

        private void searchbar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button4.PerformClick();
                e.Handled = true;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            viewerfirstname.Enabled = false;
            viewerlastname.Enabled = false;
            viewerposition.Enabled = false;
            viewersalary.Enabled = false;
            viewersss.Enabled = false;
            viewerphil.Enabled = false;
            viewerlove.Enabled = false;
            if (dataGridView1.SelectedRows.Count == 1)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string id = row.Cells[0].Value.ToString();
                    Employee EmployeeData = Emanager.findEmployee(id);
                    viewerid.Text = EmployeeData.id.ToString();
                    viewerfirstname.Text = EmployeeData.firstname;
                    viewerlastname.Text = EmployeeData.lastname;
                    viewerposition.Text = EmployeeData.position;
                    viewersalary.Text = EmployeeData.baserate.ToString();
                    viewersss.Checked = EmployeeData.haveSSS;
                    viewerphil.Checked = EmployeeData.havePhilHealth;
                    viewerlove.Checked = EmployeeData.havePagIbig;

                    viewerfirstname.Enabled = true;
                    viewerlastname.Enabled = true;
                    viewerposition.Enabled = true;
                    viewersalary.Enabled = true;
                    viewersss.Enabled = true;
                    viewerphil.Enabled = true;
                    viewerlove.Enabled = true;
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                button1.PerformClick();
                e.Handled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label39.Text = "No Result";
            dataGridView2.ClearSelection();
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if(row.Cells[0].Value != null)
                {
                    Console.WriteLine(row.Cells);
                    for (int i = 0; i < dataGridView2.ColumnCount - 1; i++)
                    {
                        if (row.Cells[i].Value.ToString() != null && row.Cells[i].Value.ToString().ToLower().Equals(textBox1.Text))
                        {
                            rowIndex = row.Index;
                            dataGridView2.Rows[rowIndex].Selected = true;
                            label39.Text = "Found it";
                            break;
                        }
                    }
                }
                
            }
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            textBox2.Enabled = false;
            textBox4.Enabled = false;
            textBox7.Enabled = false;
            button2.Enabled = false;
            groupBox9.Enabled = false;
            if (dataGridView2.SelectedRows.Count == 1)
            {
                foreach (DataGridViewRow row in dataGridView2.SelectedRows)
                {
                    string id = row.Cells[0].Value.ToString();
                    currentid = Convert.ToInt32(id);
                    Employee EmployeeData = Pmanager.findEmployee(id);
                    salary_position.Text = EmployeeData.position;
                    salary_baserate.Text = EmployeeData.baserate.ToString();
                    textBox2.Text = EmployeeData.baserate.ToString();
                    salary_sss.Checked = EmployeeData.haveSSS;
                    salary_phil.Checked = EmployeeData.havePhilHealth;
                    salary_love.Checked = EmployeeData.havePagIbig;
                    textBox2.Enabled = true;
                    button2.Enabled = true;
                    groupBox9.Enabled = true;
                    textBox4.Enabled = true;
                    textBox7.Enabled = true;
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Pmanager.updateTotalDaysWorked(dateTimePicker1, dateTimePicker2, Convert.ToInt32(textBox7.Text), label37);
            label37.Visible = true;
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Pmanager.updateTotalDaysWorked(dateTimePicker1, dateTimePicker2, Convert.ToInt32(textBox7.Text), label37);
            label37.Visible = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                textBox2.Text = "0";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox4.Text))
            {
                textBox4.Text = "0";
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox7.Text))
            {
                textBox7.Text = "0";
            }
            Pmanager.updateTotalDaysWorked(dateTimePicker1, dateTimePicker2, Convert.ToInt32(textBox7.Text), label37);
        }

        private void basesalary_textbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(basesalary_textbox.Text))
            {
                basesalary_textbox.Text = "0";
            }
        }

        private void id_textbox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(id_textbox.Text))
            {
                id_textbox.Text = "0";
            }
        }

        private void viewersalary_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(viewersalary.Text))
            {
                viewersalary.Text = "0";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double deduction_percentage = 0;
            if(salary_sss.Checked)
            {
                deduction_percentage += 11.00;
            } else if (salary_phil.Checked)
            {
                deduction_percentage += 2.75;
            } else if (salary_love.Checked)
            {
                deduction_percentage += 2.00;
            } 

            Pmanager.computePayday( currentid,
                Convert.ToInt32(salary_baserate.Text),
                Convert.ToInt32(textBox2.Text),
                Convert.ToInt32(textBox4.Text),
                deduction_percentage,
                label32,
                label36,
                label38,
                button3);

            label32.Visible = true;
            label36.Visible = true;
            label37.Visible = true;
            label38.Visible = true;
        }

        private void RefreshEmployee_Click(object sender, EventArgs e)
        {
            Emanager.update(dataGridView1);
        }

        private void RefreshPayroll_Click(object sender, EventArgs e)
        {
            Pmanager.update(dataGridView2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pmanager.savePayday(button3);
            Pmanager.update(dataGridView2);
            label32.Visible = true;
            label36.Visible = true;
            label37.Visible = true;
            label38.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/dreiijeii");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/themightyguyoftheworld/");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/alexiskyleolympia");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/mawkwayandesibnida");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            EmployeeButton.PerformClick();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            PayrollButton.PerformClick();
        }

        private void label17_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hey, You found me!. Have fun with Life!", "Payroool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.MyDoc‌​uments), "PayRoool");
            DialogResult dr = MessageBox.Show("Do you want to delete 'Employee List'. There is no going back.", "Payroool", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dr == DialogResult.Yes)
            {
                if(Directory.Exists(path))
                    Directory.Delete(path, true);
                Emanager.importCSV(statusText);
                Pmanager.importCSV(statusText);
            }
        }
    }
}
