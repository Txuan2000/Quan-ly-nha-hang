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
    public partial class Login : Form
    {
        List<BanAn> listBan = new List<BanAn>();
        Utilities data = new Utilities();
        public Login()
        {
            InitializeComponent();
        }
        
        
        private void Login_Load(object sender, EventArgs e)
        {
            anhnen.Image = Image.FromFile("Image\\2020-05-24.jpg");
        }

        private void btndangnhap_Click(object sender, EventArgs e)
        {
            try
            {
                Form1 form1 = new Form1();
               
                if (data.dangNhap(txtusername.Text, txtpassword.Text))
                {
                    if (data.quyenHan(txtusername.Text) == true)
                    {
                        form1.quyenhan = true;
                    }
                    else
                    {
                        form1.quyenhan = false;
                    }
                    form1.username = txtusername.Text;
                    form1.ShowDialog();
                    txtusername.Clear();
                    txtpassword.Clear();
                }
                else
                {
                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtpassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
            }
        }

        private void CloseProgram(object sender, FormClosingEventArgs e)
        {
            data.DongCuaHang(data.readFile());
            data.writeFile(new List<BanAn>());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ForgetPass quenmk = new ForgetPass();
            quenmk.ShowDialog();
        }
    }
    
}
