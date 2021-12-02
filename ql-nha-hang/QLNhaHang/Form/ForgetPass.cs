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
    public partial class ForgetPass : Form
    {
        public ForgetPass()
        {
            InitializeComponent();
        }

        Utilities data = new Utilities();
        private void btnxacnhan_Click(object sender, EventArgs e)
        {
            try { 
            MessageBox.Show(data.quenPass(txtmanv.Text, txttennhanvien.Text, txtsdt.Text) + "");
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);

            }

        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ForgetPass_Load(object sender, EventArgs e)
        {

        }
    }
}
