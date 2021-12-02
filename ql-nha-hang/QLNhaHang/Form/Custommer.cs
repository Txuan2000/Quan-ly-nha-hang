using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLNhaHang
{
    public partial class Form2 : Form
    {
        Utilities data = new Utilities();
        public Form2()
        {
            InitializeComponent();
        }
        public Boolean quyenhan { get; set; }
        private void Form2_Load(object sender, EventArgs e)
        {
            if (quyenhan == false)
            {
                button1.Visible = false;
                btnSua.Visible = false;
                button2.Visible = false;
            }
            txtMakh.Enabled = false;
            HienThi();
        }
        private void HienThi()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = data.danhsachKH();
            clearTextbox();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int i = e.RowIndex;
                txtMakh.Text = dataGridView1.Rows[i].Cells[0].Value.ToString();
                txtTenkh.Text = dataGridView1.Rows[i].Cells[1].Value.ToString();
                txtDiachi.Text = dataGridView1.Rows[i].Cells[2].Value.ToString();
                txtDienthoai.Text = dataGridView1.Rows[i].Cells[3].Value.ToString();
            }
            catch (Exception) { }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void clearTextbox()
        {
            txtMakh.Clear();
            txtTenkh.Clear();
            txtDiachi.Clear();
            txtDienthoai.Clear();
            txtTenkh.Focus();
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.makh = txtMakh.Text;
                kh.tenkh = txtTenkh.Text;
                kh.diachi = txtDiachi.Text;
                kh.dienthoai = txtDienthoai.Text;
                data.SuaKH(kh);
                HienThi();
            }
            catch (Exception)
            {
                MessageBox.Show("Mã khách hàn không được để trống");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            KhachHang kh = new KhachHang();
            kh.tenkh = txtTenkh.Text;
            kh.diachi = txtDiachi.Text;
            kh.dienthoai = txtDienthoai.Text;
            data.ThemKH(kh);
            HienThi();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                KhachHang kh = new KhachHang();
                kh.makh = txtMakh.Text;
                data.XoaKH(kh);
                HienThi();
            }
            catch (Exception)
            {
                MessageBox.Show("Mã khách hàn không được để trống");
            }

        }
    }
}
