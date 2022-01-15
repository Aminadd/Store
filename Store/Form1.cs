using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Store
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amina\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
        public Form1()
        {
            InitializeComponent();
        
        }

        public void dispay_data()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from[Table]";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the Product name");

            }
            else if (textBox6.Text == "")
            {
                MessageBox.Show("Please Enter the Product Brand");
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Please Enter the price");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Please Enter the quantity");
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into[Table] values('" + textBox1.Text + "', '" + textBox6.Text + "', '" + textBox2.Text + "', '" + textBox3.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                dispay_data();
                MessageBox.Show("Inserted sucessfully");
                textBox1.Focus();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the product name to delete");
            }
            else {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from[Table] where Product = ('" + textBox1.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                dispay_data();
                textBox1.Text = "";
                MessageBox.Show("Delete sucessfully");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dispay_data();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dispay_data();
            label21.Text = DateTime.Now.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please Enter the product name to update quantity");

            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update[Table] set Quantity = '" + textBox3.Text + "' where Product = '" + textBox1.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                dispay_data();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox1.Focus();
                MessageBox.Show("Quantity updated sucessfully");
            }
        }

        void dispcmb()
        {
            string constring = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amina\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
            string Query = "select * from[Table]";
            SqlConnection conDatabase = new SqlConnection(constring);
            SqlCommand cmdDatabase = new SqlCommand(Query, conDatabase);
            SqlDataReader myReader;
            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    string strn = myReader.GetString(0);
                    comboBox1.Items.Add(strn);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dispcmb();
            MessageBox.Show("Database connected");
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.Text.Length > 0)
            {
                label10.Text = (Convert.ToInt32(label7.Text) * Convert.ToInt32(comboBox3.Text)).ToString();
            }
            comboBox2.Focus();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text.Length > 0)
            {
                label9.Text = (Convert.ToInt32(label10.Text) - Convert.ToInt32(comboBox2.Text)).ToString();
            }
            button6.Focus();
                
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string constring = (@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Amina\Documents\Store.mdf;Integrated Security=True;Connect Timeout=30");
            string Query = "select * from[Table] where Product = '" + comboBox1.Text + "'";
            SqlConnection conDatabase = new SqlConnection(constring);
            SqlCommand cmdDatabase = new SqlCommand(Query, conDatabase);
            SqlDataReader Reader;
            try
            {
                conDatabase.Open();
                Reader = cmdDatabase.ExecuteReader();
                while (Reader.Read())
                {
                    string Brand = Reader.GetString(1);
                    string Price = Reader.GetInt32(2).ToString();
                    string Quantity = Reader.GetInt32(3).ToString();
                    
                    label7.Text = Price;
                    label8.Text = Quantity;
                    label24.Text = Brand;
                }
                comboBox2.Focus();
                comboBox2.Text = "";
                comboBox3.Text = "";
                label9.Text = "";
                label10.Text = "";
                comboBox3.Focus();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string[] arr = new string[7];
            arr[0] = comboBox1.SelectedItem.ToString();
            arr[1] = label24.Text;
            arr[2] = label7.Text;
            arr[3] = comboBox3.Text;
            arr[4] = label10.Text;
            arr[5] = comboBox2.Text;
            arr[6] = label9.Text;

            ListViewItem listitems = new ListViewItem(arr);
            listView1.Items.Add(listitems);
            label17.Text = (Convert.ToInt32(label9.Text) + Convert.ToInt32(label17.Text)).ToString();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if(label17.Text.Length > 0)
                {
                    textBox5.Text = (Convert.ToInt32(textBox4.Text) - Convert.ToInt32(label17.Text)).ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                textBox5.Text = "";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            for(int i = 0; i <listView1.SelectedItems.Count;)
            {
                if (listView1.SelectedItems[i].Selected)
                    label17.Text = (Convert.ToInt32(label17.Text) - Convert.ToInt32(listView1.SelectedItems[i].SubItems[5].Text)).ToString();
                    listView1.SelectedItems[i].Remove();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            printDialog1.Document = printDocument1;
            DialogResult result = printDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                printDocument1.Print();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Times New Roman", 14);
            float fontHeight = font.GetHeight();
            int startx = 90;
            int starty = 40;
            int offset = 30;
            float leftmargin = e.MarginBounds.Left;
            float topmargin = e.MarginBounds.Top;
            string top = "DATE:" + label21.Text.PadRight(5);
            graphics.DrawString(top, font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + (int)fontHeight;
            graphics.DrawString("---------------------------------------------------------------", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 30;
            graphics.DrawString("ITEMS BRAND PRICE QTY DISCOUNT SUBTOTAL", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 30;
            for (int x = 0; x < listView1.Items.Count; x++)
            {
                graphics.DrawString(listView1.Items[x].SubItems[0].Text + "\t" + listView1.Items[x].SubItems[1].Text + "\t" + listView1.Items[x].SubItems[2].Text + "\t" + listView1.Items[x].SubItems[3].Text + "\t" + listView1.Items[x].SubItems[5].Text + "\t\t" + listView1.Items[x].SubItems[6].Text, new Font("Times New Roman", 12), new SolidBrush(Color.Black), startx, starty + offset);
                offset = offset + 20;
            }
            offset = offset + (int)fontHeight + 5;
            graphics.DrawString("---------------------------------------------------------------", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 15;

            offset = offset + (int)fontHeight;
            graphics.DrawString("TOTAL -" + label17.Text + ".00", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 20;
            offset = offset + (int)fontHeight;
            graphics.DrawString("PAID AMOUNT -" + textBox4.Text + ".00", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 20;
            offset = offset + (int)fontHeight;
            graphics.DrawString("REFUND -" + textBox5.Text + ".00", font, new SolidBrush(Color.Black), startx, starty + offset);
            offset = offset + 20;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
