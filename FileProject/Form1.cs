using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileProject
{
    public partial class Form1 : Form
    {
        public  DataSet tables = new DataSet();
        public Form1()
        {
            InitializeComponent();
            ReadWriteFiles.Read(tables);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1)
            {
                //clear all strings in select table 
                comboBox1.Items.Clear();
                //meke select table is ready to enput new string(table name)
                comboBox1.Text = "";
                //the screen that print the table 
                dataGridView2.DataSource = null;
                //add the operation that will be excuted
                comboBox3.Items.Add("Sum");
                comboBox3.Items.Add("Avg");
                comboBox3.Items.Add("Count");
                comboBox3.Items.Add("Minimum");
                comboBox3.Items.Add("Maximum");
                foreach (DataTable t in tables.Tables)
                {
                    //add the tabels in combo box 1 (select table)
                    comboBox1.Items.Add(t.TableName);
                }
                //make the box that will recevie the querys is empty
                textBox2.Text = "";
            }
            //make the form(creat new table) is empty for now 
            else
            {
                comboBox3.Items.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                textBox1.Text = "";
                textBox3.Text = "";
                comboBox2.SelectedIndex = 0;
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {
           
        }
        // the add colomn button
        private void button1_Click(object sender, EventArgs e)
        {
            //if the enter colomn name is not empty
            //fill the combo box with the next data tybes 
            if (textBox1.Text != "")
            {
                DataGridViewColumn column = new DataGridViewColumn();
                column.HeaderText = textBox1.Text;
                if (comboBox2.SelectedIndex == 0)
                    column.ValueType = typeof(String);
                else if (comboBox2.SelectedIndex == 1)
                    column.ValueType = typeof(Int32);
                else
                    column.ValueType = typeof(Double);
                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            //add the row in the data gride view
            if (dataGridView1.Rows.Count == 0)
                dataGridView1.Rows.Add();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Enter table name");
                return;
            }
            try
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    DataColumn column = new DataColumn();
                    column.ColumnName = col.HeaderText;
                    column.DataType = col.ValueType;
                    dt.Columns.Add(column);
                }
                if (dataGridView1.Rows.Count > 1)
                {
                    int count = dataGridView1.Rows.Count;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (count == 1)
                            break;
                        DataRow dRow = dt.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                        }
                        dt.Rows.Add(dRow);
                        count--;
                    }
                }

                dt.TableName = textBox3.Text;
                tables.Tables.Add(dt);
            }
            catch(DuplicateNameException)
            {
                MessageBox.Show("table name already exist");
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try {
                DataTable tab = tables.Tables[comboBox1.SelectedItem.ToString()];
                DataRow[]rows= tab.Select(textBox2.Text);
                dataGridView2.Rows.Clear();
                
                
                foreach(DataRow row in rows)
                {
                    dataGridView2.Rows.Add(row.ItemArray);
                }
            }
            catch(Exception ee)
            {
                MessageBox.Show("incorrect query");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
            foreach (DataColumn c in tables.Tables[comboBox1.Text].Columns)
            {
                comboBox4.Items.Add(c.ColumnName);
                DataGridViewColumn cc = new DataGridViewColumn();
                cc.HeaderText = c.ColumnName;
                cc.ValueType = c.DataType;
                cc.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView2.Columns.Add(cc);
            }
            foreach (DataRow row in tables.Tables[comboBox1.Text].Rows)
            {
                dataGridView2.Rows.Add(row.ItemArray);
            }
            textBox2.Text = "";
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Double sum = 0;
            if (comboBox3.Text == "" || comboBox4.Text == "")
                MessageBox.Show("fill empty");
            else
            {
                if (comboBox3.SelectedIndex == 0)
                {
                    foreach (DataRow r in tables.Tables[comboBox1.Text].Rows)
                    {
                        sum += Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString());

                    }
                    MessageBox.Show("Sum = " + sum.ToString());
                }
                else if (comboBox3.SelectedIndex == 1)
                {
                    foreach (DataRow r in tables.Tables[comboBox1.Text].Rows)
                    {
                        sum += Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString());

                    }
                    sum /= (tables.Tables[comboBox1.Text].Rows.Count);
                    MessageBox.Show("Avg = " + sum.ToString());
                }
                else if (comboBox3.SelectedIndex == 2)
                {
                    if (textBox4.Text == "")
                        MessageBox.Show("enter value to count");
                    else
                    {
                        foreach (DataRow r in tables.Tables[comboBox1.Text].Rows)
                        {
                            if (r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString() == textBox4.Text)
                                sum++;
                        }
                        MessageBox.Show("the count of item : " + textBox4.Text + " in column : " + comboBox4.Text + " = " + ((int)sum).ToString());
                    }

                }
                else if (comboBox3.SelectedIndex == 3)
                {
                    sum = Double.MaxValue;
                    foreach (DataRow r in tables.Tables[comboBox1.Text].Rows)
                    {
                        if (Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString()) < sum)
                            sum = Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString());
                    }
                    MessageBox.Show("Minimum = " + sum.ToString());
                }
                else
                {
                    sum = Double.MinValue;
                    foreach (DataRow r in tables.Tables[comboBox1.Text].Rows)
                    {
                        if (Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString()) > sum)
                            sum = Double.Parse(r[tables.Tables[comboBox1.Text].Columns[comboBox4.Text].Ordinal].ToString());
                    }
                    MessageBox.Show("Maximum = " + sum.ToString());
                }
                textBox4.Visible = false;
                label8.Visible = false;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.SelectedIndex == 2)
            {
                textBox4.Visible = true;
                label8.Visible = true;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ReadWriteFiles.write(tables);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
