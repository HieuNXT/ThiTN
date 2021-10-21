using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace ThiTN
{
    public partial class Form1 : Form
    {
        static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ThiTN");
        int imgindex;
        int timeCountDown = 2400;
        int a = 1;
        public Form1()
        {
            InitializeComponent();
        }

        void Login()
        {
            if((txtUsername.Text == "admin") && (txtPassword.Text == "admin"))
            {
                this.Hide();
                Admin a = new Admin();
                a.Show();
            }
            else
            {
                con.Open();
                SqlCommand command = new SqlCommand();
                command.CommandText = "SELECT masv From ThiSinh WHERE masv = @masv AND pass =@pass";
                command.Parameters.AddWithValue("@masv", txtUsername.Text);
                command.Parameters.AddWithValue("@pass", txtPassword.Text);
                command.Connection = con;
                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    txtUserID.Text = obj.ToString();
                    textBox14.Text = obj.ToString();
                    ShowInformation();
                    panel1.Hide();
                    panel2.Show();
                    con.Close();
                }
                else
                {
                    con.Close();
                    MessageBox.Show("Invalid credentials!\nPlease enter a valid username and password to continue.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

  //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////    

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void ShowInformation()
        {
            string sql = "Select HoTen, NgaySinh, GioiTinh, Lop, pic from ThiSinh where masv = '" + txtUserID.Text + "'";
            DataTable dataTable = DBConnect.executeQuery(sql);
            txtName.Text = Convert.ToString(dataTable.Rows[0]["HoTen"]);
            textBox13.Text = Convert.ToString(dataTable.Rows[0]["HoTen"]);
            txtAddress.Text = Convert.ToString(dataTable.Rows[0]["Lop"]);
            textBox12.Text = Convert.ToString(dataTable.Rows[0]["Lop"]);
            txtSex.Text = Convert.ToString(dataTable.Rows[0]["GioiTinh"]);
            txtBornDate.Text = Convert.ToString(dataTable.Rows[0]["NgaySinh"]);
            txtP.Text = Convert.ToString(dataTable.Rows[0]["pic"]);
            pictureBox1.ImageLocation = txtP.Text;
            if (txtP.Text == null)
            {
                pictureBox1.Image = Image.FromFile("H:\\Documents\\ThiTN\\Resources\\Avatar01-512.png");
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(rdrT.Checked==true)
            {
                FormThi tt = new FormThi();
                tt.Show();
            }
            if (rdrL.Checked == true)
            {
                ThiSinh ts = new ThiSinh();
                ts.Show();
            }
            if (rdrH.Checked == true)
            {
                gbxAskAnswer.Visible = true;
                panel2.Visible = false;
                list();
            }
            if (rdrA.Checked == true)
            {
                ThiAnh ts = new ThiAnh();
                ts.Show();
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void list()
        {
            SqlDataAdapter a = new SqlDataAdapter("Select qID, CauHoi, A, B, C, D, DapAn, pic from Question where Mon = 'Hóa'", con);
            DataTable dataTable = new DataTable();
            a.Fill(dataTable);

            listBox1.DisplayMember = "qID";
            listBox1.ValueMember = "CauHoi";
            listBox1.DataSource = dataTable;
            listBox1.SelectedIndex = imgindex = 0;
            textBox11.Text = "1";
        }



        void userAnswer()
        {
            if (A.Checked)
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

            if (textBox7.Text == label4.Text)
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
            string sql = "insert " + b + " into ThiSinh where Mon = 'Hóa' ";

        }

        private void btnSubmit_Click_1(object sender, EventArgs e)
        {
            diem();
            Form1 f = new Form1();
            f.Show();
        }

        private void A_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox7.Text = "A";
        }

        private void B_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox7.Text = "B";
        }

        private void C_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox7.Text = "C";
        }

        private void D_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox7.Text = "D";
        }

        private void timer_Tick_1(object sender, EventArgs e)
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

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
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
            pictureBox2.ImageLocation = textBox6.Text;
            if (textBox6.Text == null)
            {
                pictureBox2.Image = Image.FromFile("H:\\Documents\\ThiTN\\Resources\\Avatar01-512.png");
            }
        }

        private void btnNext_Click_1(object sender, EventArgs e)
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
            A.Checked = false;
            B.Checked = false;
            C.Checked = false;
            D.Checked = false;
            textBox7.Clear();
            a = a + 1;
            textBox11.Text = a.ToString();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
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

            a = a - 1;
            textBox11.Text = a.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            gbxAskAnswer.Visible = false;
            panel2.Visible = true;
            list();
        }
    }
}
