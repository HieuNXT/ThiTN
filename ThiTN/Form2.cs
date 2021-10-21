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
    public partial class Form2 : Form
    {
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ThiTN");
        public Form2()
        {
            InitializeComponent();
        }
        private void btnAddQuestion_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                if (btnAddQuestion.Text == "Thêm")
                {
                    SqlCommand cmd = new SqlCommand("AddAndEditQuestion", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@moded", "Adding");
                    cmd.Parameters.AddWithValue("@qID", 0);
                    cmd.Parameters.AddWithValue("@CauHoi", txtQuestion.Text);
                    cmd.Parameters.AddWithValue("@Mon", textBox4.Text);
                    cmd.Parameters.AddWithValue("@A", txtA.Text);
                    cmd.Parameters.AddWithValue("@B", txtB.Text);
                    cmd.Parameters.AddWithValue("@C", txtC.Text);
                    cmd.Parameters.AddWithValue("@D", txtD.Text);
                    cmd.Parameters.AddWithValue("@DapAn", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pic", textBox2.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AddAndEditQuestion", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@moded", "Editting");
                    cmd.Parameters.AddWithValue("@qID", textBox1.Text);
                    cmd.Parameters.AddWithValue("@CauHoi", txtQuestion.Text);
                    cmd.Parameters.AddWithValue("@Mon", textBox4.Text);
                    cmd.Parameters.AddWithValue("@A", txtA.Text);
                    cmd.Parameters.AddWithValue("@B", txtB.Text);
                    cmd.Parameters.AddWithValue("@C", txtC.Text);
                    cmd.Parameters.AddWithValue("@D", txtD.Text);
                    cmd.Parameters.AddWithValue("@DapAn", textBox3.Text);
                    cmd.Parameters.AddWithValue("@pic", textBox2.Text);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                sqlConnection.Close();
                FillDataGridView2(); reset();
            }
        }
        void FillDataGridView2()
        {
            sqlConnection.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("View", sqlConnection);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@Mon", textBox4.Text);
            DataTable dtb = new DataTable();
            sqlDa.Fill(dtb);
            dataGridView1.DataSource = dtb;

            dataGridView1.Columns[0].HeaderCell.Value = "Câu hỏi";
            dataGridView1.Columns[1].HeaderCell.Value = "Môn";
            dataGridView1.Columns[6].HeaderCell.Value = "Đáp án";
            dataGridView1.Columns[7].HeaderCell.Value = "Ảnh";
            //DSSV.Columns[4].HeaderCell.Value = "Lớp";
            //DSSV.Columns[5].HeaderCell.Value = "Password";
            //DSSV.Columns[6].HeaderCell.Value = "Ảnh thẻ";
            sqlConnection.Close();
        }

        private void btnRefreshInfo_Click(object sender, EventArgs e)
        {
            FillDataGridView2();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            FillDataGridView2();
        }

        private void radioButton5_CheckedChanged_1(object sender, EventArgs e)
        {

            if (radioButton5.Checked)
            {
                textBox3.Text = "B";
            }
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {

            if (radioButton2.Checked)
            {
                textBox3.Text = "A";
            }
        }

        private void radioButton4_CheckedChanged_1(object sender, EventArgs e)
        {

            if (radioButton4.Checked)
            {
                textBox3.Text = "C";
            }
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {

            if (radioButton3.Checked)
            {
                textBox3.Text = "D";
            }
        }

        private void rdoQuestionType1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rdoQuestionType1.Checked)
            {
                textBox4.Text = "Toán";
            }
        }

        private void rdoQuestionType3_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rdoQuestionType3.Checked)
            {
                textBox4.Text = "Hóa";
            }
        }

        private void rdoQuestionType2_CheckedChanged_1(object sender, EventArgs e)
        {

            if (rdoQuestionType2.Checked)
            {
                textBox4.Text = "Lý";
            }
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

            if (radioButton1.Checked)
            {
                textBox4.Text = "Anh";
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {

            sqlConnection.Open();
            txtQuestion.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtA.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtB.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtC.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtD.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            if (textBox2.Text==null)
            {
                picImage.Image = Image.FromFile("T:\\Downloads\\Documents\\1.jpg");
            }
            else
            {
                picImage.ImageLocation = textBox2.Text;
            }
            textBox1.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();

            if(textBox4.Text=="Toán")
            {
                rdoQuestionType1.Checked = true;
            }
            if (textBox4.Text == "Lý")
            {
                rdoQuestionType2.Checked = true;
            }
            if (textBox4.Text == "Hóa")
            {
                rdoQuestionType3.Checked = true;
            }
            if (textBox4.Text == "Anh")
            {
                radioButton1.Checked = true;
            }

            btnAddQuestion.Text = "Cập nhật";
            button6.Enabled = true;

            sqlConnection.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("DeleteQuestion", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CauHoi", txtQuestion.Text);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                FillDataGridView2();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            reset();
        }

        private void radioButton13_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Text = "Toán";
            FillDataGridView2();
            textBox4.Clear();
        }

        private void radioButton11_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Text = "Hóa";
            FillDataGridView2();
            textBox4.Clear();
        }

        private void radioButton12_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Text = "Lý";
            FillDataGridView2();
            textBox4.Clear();
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Text = "Anh";
            FillDataGridView2();
            textBox4.Clear();
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            FillDataGridView2();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void picImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = o.FileName;
                picImage.ImageLocation = o.FileName;
            }
        }
        void reset()
        {
            txtQuestion.Clear();
            textBox4.Clear();
            txtA.Clear();
            txtB.Clear();
            txtC.Clear();
            txtD.Clear();
            picImage.Image = Image.FromFile("T:\\Downloads\\Documents\\1.jpg");
            btnAddQuestion.Text = "Thêm";
            textBox3.Clear();
            textBox2.Clear();
            textBox4.Clear();
            FillDataGridView2();
            radioButton3.Checked = false;
            radioButton4.Checked = false;
            radioButton5.Checked = false;
            radioButton2.Checked = false;
            radioButton1.Checked = false;
            rdoQuestionType1.Checked = false;
            rdoQuestionType2.Checked = false;
            rdoQuestionType3.Checked = false;

        }

        private void quảnLíSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Admin a = new Admin();
            a.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
