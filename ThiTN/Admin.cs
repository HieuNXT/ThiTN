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
    public partial class Admin : Form
    {
        static SqlConnection sqlConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=ThiTN");
        int qID = 0;
        public Admin()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            if (o.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = o.FileName;
                pictureBox1.ImageLocation = o.FileName;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sqlConnection.Open();
                if (button1.Text == "Thêm")
                {
                    SqlCommand cmd = new SqlCommand("AddOrEdit", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", "Add");
                    cmd.Parameters.AddWithValue("@masv", txtUserID.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtName.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", txtBornDate.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbxSex.Text.Equals("Male") ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@Lop", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pic", txtFilePath.Text);
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("AddOrEdit", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@mode", "Edit");
                    cmd.Parameters.AddWithValue("@masv", txtUserID.Text);
                    cmd.Parameters.AddWithValue("@HoTen", txtName.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", txtBornDate.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cbxSex.Text.Equals("Male") ? "Male" : "Female");
                    cmd.Parameters.AddWithValue("@Lop", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@pass", textBox1.Text);
                    cmd.Parameters.AddWithValue("@pic", txtFilePath.Text);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Saved!!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                sqlConnection.Close();
                FillDataGridView();
            }
        }


        void FillDataGridView()
        {
            sqlConnection.Open();
            SqlDataAdapter sqlDa = new SqlDataAdapter("ViewAndSearch", sqlConnection);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            sqlDa.SelectCommand.Parameters.AddWithValue("@ten", txtSearch.Text);
            DataTable dtb = new DataTable();
            sqlDa.Fill(dtb);
            DSSV.DataSource = dtb;

            DSSV.Columns[0].HeaderCell.Value = "MSV";
            DSSV.Columns[1].HeaderCell.Value = "Họ tên";
            DSSV.Columns[2].HeaderCell.Value = "Ngày sinh";
            DSSV.Columns[3].HeaderCell.Value = "Giới tính";
            DSSV.Columns[4].HeaderCell.Value = "Lớp";
            DSSV.Columns[5].HeaderCell.Value = "Password";
            DSSV.Columns[6].HeaderCell.Value = "Ảnh thẻ";
            DSSV.Columns[7].HeaderCell.Value = "Điểm Toán";
            DSSV.Columns[8].HeaderCell.Value = "Điểm Lý";
            DSSV.Columns[9].HeaderCell.Value = "Điểm Hóa";
            DSSV.Columns[10].HeaderCell.Value = "Điểm Anh";
            sqlConnection.Close();
        }


        private void btnSearchInfo_Click(object sender, EventArgs e)
        {
            FillDataGridView();
            txtSearch.Clear();
        }

        private void DSSV_DoubleClick(object sender, EventArgs e)
        {
            sqlConnection.Open();
            txtUserID.Text = DSSV.CurrentRow.Cells[0].Value.ToString();
            txtName.Text = DSSV.CurrentRow.Cells[1].Value.ToString();
            txtBornDate.Text = DSSV.CurrentRow.Cells[2].Value.ToString();
            cbxSex.Text = DSSV.CurrentRow.Cells[3].Value.ToString();
            txtAddress.Text = DSSV.CurrentRow.Cells[4].Value.ToString();
            textBox1.Text = DSSV.CurrentRow.Cells[5].Value.ToString();
            txtFilePath.Text = DSSV.CurrentRow.Cells[6].Value.ToString();
            if (txtFilePath.Text == null)
            {
                pictureBox1.Image = Image.FromFile("H:\\Documents\\ThiTN\\Resources\\Avatar01-512.png");
            }
            toan.Text = DSSV.CurrentRow.Cells[7].Value.ToString();
            ly.Text = DSSV.CurrentRow.Cells[8].Value.ToString();
            hoa.Text = DSSV.CurrentRow.Cells[9].Value.ToString();
            anh.Text = DSSV.CurrentRow.Cells[10].Value.ToString();

            button1.Text = "Cập nhật";
            button2.Enabled = true;

            sqlConnection.Close();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sqlConnection.Open();
            try
            {
                SqlCommand cmd = new SqlCommand("Delete", sqlConnection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@masv", txtUserID.Text);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
                FillDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtUserID.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtBornDate.Clear();
            txtFilePath.Clear();
            pictureBox1.Image = Image.FromFile("H:\\Documents\\ThiTN\\Resources\\Avatar01-512.png");
            button1.Text = "Thêm";
            textBox1.Clear();
        }


        private void button4_Click(object sender, EventArgs e)
        {

            EXP excel = new EXP();
            SqlDataAdapter sqlDa = new SqlDataAdapter("Exp", sqlConnection);
            sqlDa.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtb = new DataTable();
            sqlDa.Fill(dtb);
            DSSVexp.DataSource = dtb;
            excel.Export(dtb, "Danh sach", "DANH SÁCH THÍ SINH");
        }

        private void quảnLíĐềToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Form2 f = new Form2();
            f.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }


}

