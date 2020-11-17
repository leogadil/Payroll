using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PayrollApp
{
    public partial class Form1 : Form
    {
        EmployeeManager Emanager = new EmployeeManager();
        ErrorHandling ErrHandling = new ErrorHandling();

        public Form1()
        {
            InitializeComponent();
            Emanager.importCSV(statusText);
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

            Emanager.updateTable(dataGridView1);
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            UpdaterBox.Enabled = true;
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
            }
        }

        private void groupBox2_Leave(object sender, EventArgs e)
        {
            dataGridView1.ClearSelection();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(viewerid.Text))
            {
                Emanager.destroyEmployee(viewerid.Text);
                Emanager.updateTable(dataGridView1);

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

            Emanager.updateTable(dataGridView1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            searchint.Text = "No Result";
            dataGridView1.ClearSelection();
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i < dataGridView1.ColumnCount-1; i++)
                {
                    if (row.Cells[i].Value.ToString().Equals(searchbar.Text))
                    {
                        rowIndex = row.Index;
                        dataGridView1.Rows[rowIndex].Selected = true;
                        searchint.Text = "Found it";
                        break;
                    }
                }
            }
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TabControl.SelectedIndex <= 0)
            {
                Emanager.importCSV(statusText);
            } else
            {
                Emanager.updateTable(dataGridView1);
            }
        }

        private void id_textbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
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
    }
}
