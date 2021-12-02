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
    public partial class Themnhanvien : Form
    {
        public Themnhanvien()
        {
            InitializeComponent();
        }
        Utilities data = new Utilities();
    
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NhanVien nv = new NhanVien();
            nv.TenNV = txttennhanvien.Text;
            nv.GioiTinh = cbbGioitinh.Text;
            nv.Tuoi = int.Parse(txttuoi.Text);
            nv.DiaChi = txtdiachi.Text;
            nv.SoDienThoai = txtsodienthoai.Text;
            nv.chucvu = cbxChucvu.Text;
            nv.user = txttaikhoan.Text;
            nv.pass = txtmatkhau.Text;
            data.themNV(nv);
            this.Close();
        }

        private void Themnhanvien_Load(object sender, EventArgs e)
        {

        }
    }
}
