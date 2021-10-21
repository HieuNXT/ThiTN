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

namespace ThiTN
{
    public partial class FormThi : Form
    {
        static SqlConnection sql = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ThiTN");
        int imgindex;
        int timeCountDown = 2400;
        int a = 1;
        public FormThi()
        {
            InitializeComponent();
        }

        private void FormThi_Load(object sender, EventArgs e)
        {
            list();
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            userAnswer();
            listBox1.SelectedIndex = imgindex;
            if (imgindex < listBox1.Items.Count - 1)
            {
                imgindex += 1;
                if (imgindex == listBox1.Items.Count - 1)
                    btnNext.Enabled = false;
                if (imgindex > 0)
                    btnPrevious.Enabled = true;
                listBox1.SelectedIndex = imgindex;
            }
            AA.Checked = false;
            B.Checked = false;
            C.Checked = false;
            D.Checked = false;
            textBox7.Clear();
            a = a+1;
            textBox11.Text = a.ToString();
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            listBox1.SelectedIndex = imgindex;
            if (imgindex > 0)
            {
                imgindex -= 1;
                if (imgindex == 0)
                {
                    btnPrevious.Enabled = false;
                }
                if (imgindex < listBox1.Items.Count - 1)
                    btnNext.Enabled = true;
                listBox1.SelectedIndex = imgindex;
            }
            textBox7.Clear();

            a = a-1;
            textBox11.Text = a.ToString();
        }

        void list()
        {
            SqlDataAdapter a = new SqlDataAdapter("Select qID, CauHoi, A, B, C, D, DapAn, pic from Question where Mon = 'Toán'",sql);
            DataTable dataTable = new DataTable();
            a.Fill(dataTable);

            listBox1.DisplayMember = "qID";
            listBox1.ValueMember = "CauHoi";
            listBox1.DataSource = dataTable;
            listBox1.SelectedIndex = imgindex = 0;
            textBox11.Text = "1";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox10.Text = (((DataRowView)listBox1.SelectedItem).Row[0]).ToString();
            string sql = "Select CauHoi, A, B, C, D, DapAn, pic from Question where qID = '" + textBox10.Text + "'";
            DataTable dataTable = DBConnect.executeQuery(sql);
            textBox1.Text = Convert.ToString(dataTable.Rows[0]["CauHoi"]);
            textBox2.Text = Convert.ToString(dataTable.Rows[0]["A"]);
            textBox3.Text = Convert.ToString(dataTable.Rows[0]["B"]);
            textBox5.Text = Convert.ToString(dataTable.Rows[0]["C"]);
            textBox4.Text = Convert.ToString(dataTable.Rows[0]["D"]);
            label4.Text = Convert.ToString(dataTable.Rows[0]["DapAn"]);
            textBox6.Text = Convert.ToString(dataTable.Rows[0]["pic"]);
            pictureBox1.ImageLocation = textBox6.Text;
            if (textBox6.Text == null)
            {
                pictureBox1.Image = Image.FromFile("H:\\Documents\\ThiTN\\Resources\\Avatar01-512.png");
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
                if (timeCountDown > 0)
                {
                    timeCountDown--;
                    TimeSpan time = TimeSpan.FromSeconds(timeCountDown);
                    lblTimer.Text = time.ToString(@"mm\:ss");
                }
                else
                {
                    timer.Stop();
                    MessageBox.Show("Phần thi đã hết giờ!", "Hết giờ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            
        }

        private void B_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "B";
        }

        private void A_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "A";
        }

        private void C_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "C";
        }

        private void D_CheckedChanged(object sender, EventArgs e)
        {
            textBox7.Text = "D";
        }
        void userAnswer()
        {
            if(AA.Checked)
            {
                listBox3.Items.Add(textBox7.Text);
            }
            if (B.Checked)
            {
                listBox3.Items.Add(textBox7.Text);
            }
            if (C.Checked)
            {
                listBox3.Items.Add(textBox7.Text);
            }
            if (D.Checked)
            {
                listBox3.Items.Add(textBox7.Text);
            }

            if (textBox7.Text==label4.Text)
            {
                listBox2.Items.Add(label4.Text);
            }
        }
        void diem()
        {
            int a = Int32.Parse(listBox2.Items.Count.ToString());
            double b = 0.25 * a;
            textBox9.Text = "Diem cua ban la " + b + " diem";
            textBox8.Text = "Ban duoc " + a + " diem";
            MessageBox.Show("Ban duoc " + b + " diem");
            string sql = "insert " + b + " into ThiSinh where Mon = 'Lý' ";

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            diem();
            Form1 f = new Form1();
            f.Show();
        }

    }
}
