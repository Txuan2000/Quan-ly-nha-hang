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
    public partial class ChangePass : Form
    {
        public string manhanvien { get; set; }
        public ChangePass()
        {
            InitializeComponent();
        }
        Utilities data = new Utilities();
        NhanVien nvcandoimk = new NhanVien();
        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            try { 
            if (data.ktmatKhau(manhanvien, txtmkcu.Text))
            {
                if (txtmkmoi.Text.Equals(txtnhaplaimkmoi.Text))
                {
                    data.doiPass(manhanvien, txtmkmoi.Text);
                    MessageBox.Show("Đổi mật khẩu thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu mới không trùng nhau.");
                }
            }
            else
            {
                MessageBox.Show("Mật khẩu cũ không đúng.");
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);

            }
        }

        //hien thi va khong hien thi mat khau
        private void chkhienthi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkhienthi.Checked)
            {
                txtmkcu.UseSystemPasswordChar = false;
                txtmkmoi.UseSystemPasswordChar = false;
                txtnhaplaimkmoi.UseSystemPasswordChar = false;
            }
            else
            {
                txtmkcu.UseSystemPasswordChar = true;
                txtmkmoi.UseSystemPasswordChar = true;
                txtnhaplaimkmoi.UseSystemPasswordChar = true;
            }
        }
        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangePass_Load(object sender, EventArgs e)
        {

        }
    }
}
