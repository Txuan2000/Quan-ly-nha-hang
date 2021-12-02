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
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace QLNhaHang
{
    public partial class Form1 : Form
    {
        //properties
        List<BanAn> listBan = new List<BanAn>();

        List<BanAn> listBanTrong = new List<BanAn>();
        public Boolean quyenhan { get; set; }
        //user đang đăng nhập
        NhanVien user = new NhanVien();
        public string username { get; set; }
        //Loại thống kê
        string loaiThongKe;

        BindingSource bindingsrc = new BindingSource();
        Utilities data = new Utilities();
        int indexSelected;
        public Form1()
        {

            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listBan = data.readFile();
            //Lấy tên người đang đăng nhập
            user.TenNV = data.userAndPass("where tendangnhap='"
                            + username + "'").Rows[0].ItemArray[1].ToString();
            msg.Text = "Xin chào " + user.TenNV;
            msg2.Text = "Xin chào " + user.TenNV;
            //Load cac ban trong
            listBanTrong = data.loadBanTrong(listBanTrong);
            bindingsrc.DataSource = listBanTrong;
            cbxGoimonBan.DataSource = bindingsrc;
            cbxGoimonBan.DisplayMember = "tenbanan";
            cbxGoimonBan.ValueMember = "mabanan";
            //Hien thi logo
            picMonAn.Image = System.Drawing.Image.FromFile(@"Image\logo.jpg");
            ptbAddMonAn.Image = System.Drawing.Image.FromFile(@"Image\logo.jpg");
            picClose.Image = System.Drawing.Image.FromFile("Image\\close.jpg");
            picLogout.Image = System.Drawing.Image.FromFile("Image\\logout.PNG");
            pictureBoxbanan.Visible = false;
            txtgiamgia.Text = "0";
            bindingsrc = new BindingSource();

            //Không cho phép thay đổi dữ liệu 3 cột đầu danh sách món ăn đã chọn trong mục gọi món
            dgvdsMondachon.Columns[0].ReadOnly = true;
            dgvdsMondachon.Columns[1].ReadOnly = true;
            dgvdsMondachon.Columns[2].ReadOnly = true;
            
            goimon();
            dgvdsmonan.DataSource = data.danhsachmonantheoloai(
                    cbxLoaimonan.SelectedValue.ToString());
            
            thongKe();
            HienThiDsMonAn();
            HienThiBanAn();
            //quyen han
            if (quyenhan == false)
            {
                btnThems.Visible = false;
                btnSuas.Visible = false;
                btnxoassss.Visible = false;
                btnthembans.Enabled = false;
                btnsuabans.Enabled = false;
                btnxoabans.Enabled = false;
                btnChangePass.Visible = false;
                btnSuaHD.Visible = false;
                btnXoaHD.Visible = false;
                imgnhanvien.Image = System.Drawing.Image.FromFile("Image\\logo.jpg");
                pictureBoxbanan.Visible = true;
                pictureBoxbanan.Image = System.Drawing.Image.FromFile("Image\\logo.jpg");
                tableLayoutPanel19.Hide();
                panelbanann.Hide();
                tabControl1.TabPages.Remove(tabPage2);
                tabControl1.TabPages.Remove(tabPage7);
                tabControl1.TabPages.Remove(tabPage8);
                tabPage6.Text = "Xem danh sách bàn ăn";
                tableLayoutPanel12.Controls.Clear();
                tableLayoutPanel12.Controls.Add(btnXem, 1, 1);
            }
        }
        private void tabcontrol_click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    goimon();
                    break;

                case 1:
                    laphoadon();
                    break;
                case 2:
                    capnhatHD();
                    DisplayHoadon();
                    break;
                case 3:
                    HienThiNhanVien();
                    break;

                case 4:
                    HienThiBanAn();
                    break;
                case 5:
                    danhsachcbMMN();
                    DisplayMenu(data.hienthiMN());
                    break;
                case 7:
                    thongKe();
                    break;
                case 8:
                    //picClose.Image = System.Drawing.Image.FromFile("Image\\close.jpg");
                    //picLogout.Image = System.Drawing.Image.FromFile("Image\\logout.PNG");
                    break;
            }
        }

        //gọi món

        private void goimon()
        {

            dgvdsMondachon.Rows.Clear();
            cbxLoaimonan.DataSource = data.danhsachloai();
            cbxLoaimonan.DisplayMember = "mota";
            cbxLoaimonan.ValueMember = "mota";

            cbxLoaimonan.Enabled = true;
            cbxGoimonMenu.Enabled = false;

            if (listBan.Count == 0)
            {
                radGoiThem.Enabled = false;
            }
            else
            {
                radGoiThem.Enabled = true;
            }

            cbxGoimonMenu.DataSource = data.danhsachmenu();
            cbxGoimonMenu.DisplayMember = "tenmenu";
            cbxGoimonMenu.ValueMember = "mamenu";

            //Load cac ban trong
            cbxGoimonBan.DataSource = null;
            data.loadBanTrong(listBanTrong);
            bindingsrc.DataSource = listBanTrong;
            cbxGoimonBan.DataSource = bindingsrc;
            cbxGoimonBan.DisplayMember = "tenbanan";
            cbxGoimonBan.ValueMember = "mabanan";

            //thay doi dgvdsmonan theo lua chon
            if (radMonan.Checked)
            {
                dgvdsmonan.DataSource = data.danhsachmonantheoloai(
                 cbxLoaimonan.SelectedValue.ToString());
            }
            else
            {
                dgvdsmonan.DataSource = data.danhsachmonantheomenu(
               cbxGoimonMenu.SelectedValue.ToString());
            }
            if (radGoiMoi.Checked)
            {
                bindingsrc.DataSource = listBanTrong;
                cbxGoimonBan.DataSource = bindingsrc;
                cbxGoimonBan.DisplayMember = "tenbanan";
                cbxGoimonBan.ValueMember = "mabanan";

                txtGoimonKM.Text = "0";
                txtGoimonMoney.Text = "0";
                txtSoghe.Text = "4";
            }
            else if (radGoiThem.Checked)
            {
                bindingsrc.DataSource = listBan;
                cbxGoimonBan.DataSource = bindingsrc;
                cbxGoimonBan.DisplayMember = "tenbanan";
                cbxGoimonBan.ValueMember = "mabanan";

                if (radGoiThem.Checked)
                {
                    int index = 0;
                    foreach (var item in listBan)
                    {
                        if (item.Tenbanan.Equals(cbxGoimonBan.Text))
                        {
                            index = listBan.IndexOf(item);
                            break;
                        }
                    }
                    HienThiDSmondachon(listBan[index].td);
                    txtSoghe.Text = listBan[index].Soghe + "";
                    txtGoimonKM.Text = listBan[index].Khuyenmai + "";
                    txtGoimonMoney.Text = listBan[index].tien + "";
                }
            }
            cbxGoimonBan.Focus();
        }
        private void btnGoimon_Click(object sender, EventArgs e)
        {
            if (radGoiThem.Checked)
            {
                int index = 0;
                //tim ban dang chon
                foreach (var item in listBan)
                {
                    if (item.Tenbanan.Equals(cbxGoimonBan.Text))
                    {
                        index = listBan.IndexOf(item);
                        break;
                    }
                }

                //tim xong thi sua du lieu
                listBan[index].Soghe = int.Parse(txtSoghe.Text);
                listBan[index].tien = int.Parse(txtGoimonMoney.Text);
                listBan[index].Khuyenmai = int.Parse(txtGoimonKM.Text);

                List<ThucDon> td = new List<ThucDon>();
                int n = dgvdsMondachon.RowCount - 1;
                for (int i = 0; i < n; i++)
                {
                    ThucDon thucdon = new ThucDon();
                    thucdon.tenmon = dgvdsMondachon.Rows[i].Cells[0].Value.ToString();
                    thucdon.tenmenu = dgvdsMondachon.Rows[i].Cells[1].Value.ToString();
                    thucdon.dongia = int.Parse(dgvdsMondachon.Rows[i].Cells[2].Value.ToString());
                    thucdon.soluong = int.Parse(dgvdsMondachon.Rows[i].Cells[3].Value.ToString());
                    td.Add(thucdon);
                }

                listBan[index].td = td;
                return;
            }
            if (dgvdsMondachon.RowCount <= 1)
            {
                MessageBox.Show("Bạn chưa chọn món", "Thông báo");
            }
            else
            {
                List<ThucDon> td = new List<ThucDon>();

                BanAn ban = new BanAn();
                ban.Mabanan = cbxGoimonBan.SelectedValue.ToString();
                ban.Tenbanan = cbxGoimonBan.Text;
                ban.Soghe = int.Parse(txtSoghe.Text);
                ban.Tinhtrang = "Có khách";

                data.SuaBA(ban);

                int n = dgvdsMondachon.RowCount - 1;
                for (int i = 0; i < n; i++)
                {
                    ThucDon thucdon = new ThucDon();
                    thucdon.tenmon = dgvdsMondachon.Rows[i].Cells[0].Value.ToString();
                    thucdon.tenmenu = dgvdsMondachon.Rows[i].Cells[1].Value.ToString();
                    thucdon.dongia = int.Parse(dgvdsMondachon.Rows[i].Cells[2].Value.ToString());
                    thucdon.soluong = int.Parse(dgvdsMondachon.Rows[i].Cells[3].Value.ToString());
                    td.Add(thucdon);
                }

                ban.td = td;

                ban.tien = int.Parse(txtGoimonMoney.Text);
                ban.Khuyenmai = int.Parse(txtGoimonKM.Text);

                if (!data.checkExists(listBan, ban))
                {
                    cbxBanan.DataSource = null;
                    listBan.Add(ban);
                    bindingsrc.DataSource = listBan;
                    cbxBanan.DataSource = bindingsrc.DataSource;
                    cbxBanan.DisplayMember = "tenbanan";
                    cbxBanan.ValueMember = "Mabanan";
                }

                listBanTrong.Remove(ban);
                goimon();

                dgvdsMondachon.Rows.Clear();
            }

        }
        //goi mon
        private void getDsMonDaChon(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                indexSelected = e.RowIndex;
            }
            catch (Exception) { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvdsMondachon.Rows.Count > 1)
                {
                    dgvdsMondachon.Rows.RemoveAt(indexSelected);
                }

                if (indexSelected == dgvdsMondachon.Rows.Count - 1)
                {
                    indexSelected--;
                }

            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            txtGoimonMoney.Text = tinhTien() + "";
            txtGoimonKM.Text = tinhKhuyenMai() + "";
        }
        //Tinh khuyen mai
        private int tinhKhuyenMai()
        {
            //Đơn giá lớn hơn 2 triệu thì giảm 20%
            if (txtGoimonMoney.Text != "")
            {
                int tien = int.Parse(txtGoimonMoney.Text);
                if (tien >= 2000000)
                    return 20;
            }

            int n = dgvdsMondachon.Rows.Count - 1;
            try
            {
                //Nếu đã chọn món
                if (n > 0)
                {
                    //Tính số món có trong menu có tên giống tên menu của món đầu tiên
                    int soMonTrongMenu = data.dsMonAnTheoTenMenu(
                            dgvdsMondachon.Rows[0].Cells[1].Value.ToString()).Rows.Count;
                    //Nếu số món bằng nhau
                    if (n == soMonTrongMenu)
                    {
                        //duyết danh sách món đã chọn nếu khác tên menu thì trả về 0
                        for (int i = 0; i < n; i++)
                        {
                            string tenmenu1 = dgvdsMondachon.Rows[i].Cells[1].Value.ToString();
                            for (int j = i; j < n; j++)
                            {
                                string tenmenu2 = dgvdsMondachon.Rows[j].Cells[1].Value.ToString();
                                if (tenmenu1 != tenmenu2)
                                {
                                    return 0;
                                }
                            }
                        }
                        //ngược lại trả về 10
                        return 10;
                    }
                }

            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            return 0;
        }
        //Tinh tien cua don
        private int tinhTien()
        {
            int sum = 0;
            for (int j = 0; j < dgvdsMondachon.Rows.Count - 1; j++)
            {
                sum += (int.Parse(dgvdsMondachon.Rows[j].Cells[3].Value.ToString()) * int.Parse(dgvdsMondachon.Rows[j].Cells[2].Value.ToString()));
            }
            return sum;
        }

        //chọn món 
        private void dgvdsmonan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index < 0) return;
                string tenmon = dgvdsmonan.Rows[index].Cells[0].Value.ToString();
                string tenmenu = dgvdsmonan.Rows[index].Cells[1].Value.ToString();
                string dongia = dgvdsmonan.Rows[index].Cells[2].Value.ToString();
                int soluong = 1;

                //load anh
                string ImgGoiMon = data.layDuongDanImg(tenmon) + ".jpg";
                picMonAn.Image = System.Drawing.Image.FromFile(ImgGoiMon);
                int n = dgvdsMondachon.Rows.Count;
                if (n > 1)
                {
                    for (int i = 0; i < n - 1; i++)
                    {
                        if (dgvdsMondachon.Rows[i].Cells[0].Value.ToString().Equals(tenmon))
                        {
                            dgvdsMondachon.Rows[i].Cells[3].Value = int.Parse(dgvdsMondachon.Rows[i].Cells[3].Value.ToString()) + 1 + "";
                            return;
                        }
                    }
                    dgvdsMondachon.Rows.Add(tenmon, tenmenu, dongia, soluong);
                }
                else
                {
                    dgvdsMondachon.Rows.Add(tenmon, tenmenu, dongia, soluong);
                }
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
            txtGoimonMoney.Text = tinhTien() + "";
            txtGoimonKM.Text = tinhKhuyenMai() + "";
        }

        private void btnGoimonAdd_Click(object sender, EventArgs e)
        {
            int n = dgvdsmonan.Rows.Count;
            for (int i = 0; i < n - 1; i++)
            {
                ThucDon thucdon = new ThucDon();
                thucdon.tenmon = dgvdsmonan.Rows[i].Cells[0].Value.ToString();
                thucdon.tenmenu = dgvdsmonan.Rows[i].Cells[1].Value.ToString();
                thucdon.dongia = int.Parse(dgvdsmonan.Rows[i].Cells[2].Value.ToString());
                thucdon.soluong = 1;
                //goi nhanh
                int m = dgvdsMondachon.Rows.Count - 1;
                if (m > 0)
                {
                    bool Exist = false;
                    for (int j = 0; j < m; j++)
                    {
                        if (thucdon.tenmon.Equals(dgvdsMondachon.Rows[j].Cells[0].Value.ToString()))
                        {
                            dgvdsMondachon.Rows[j].Cells[3].Value = int.Parse(dgvdsMondachon.Rows[j].Cells[3].Value.ToString()) + 1 + "";
                            Exist = true;
                            break;
                        }

                    }
                    if (!Exist)
                    {

                        dgvdsMondachon.Rows.Add(thucdon.tenmon, thucdon.tenmenu, thucdon.dongia, thucdon.soluong);
                    }
                }
                else
                    dgvdsMondachon.Rows.Add(thucdon.tenmon, thucdon.tenmenu, thucdon.dongia, thucdon.soluong);

            }
            txtGoimonMoney.Text = tinhTien() + "";
            txtGoimonKM.Text = tinhKhuyenMai() + "";
        }

        //radio changed
        private void goimon(object sender, EventArgs e)
        {
            if (radMonan.Checked)
            {
                cbxLoaimonan.Enabled = true;
                cbxGoimonMenu.Enabled = false;
                dgvdsmonan.Enabled = true;

                dgvdsmonan.DataSource = data.danhsachmonantheoloai(
                 cbxLoaimonan.SelectedValue.ToString());
            }
            else if (radMenu.Checked)
            {
                dgvdsmonan.Enabled = false;
                cbxLoaimonan.Enabled = false;
                cbxGoimonMenu.Enabled = true;
                picMonAn.Image = System.Drawing.Image.FromFile("Image\\logo.jpg");
                dgvdsmonan.DataSource = data.danhsachmonantheomenu(
                 cbxGoimonMenu.SelectedValue.ToString());
            }

        }

        private void cbxLoaimonan_TextChanged(object sender, EventArgs e)
        {
            dgvdsmonan.DataSource = data.danhsachmonantheoloai(
                cbxLoaimonan.SelectedValue.ToString());
        }
        private void cbxGoimonMenu_TextChanged(object sender, EventArgs e)
        {
            if (radMenu.Checked)
            {
                dgvdsmonan.DataSource = data.danhsachmonantheomenu(
                    cbxGoimonMenu.SelectedValue.ToString());
            }
        }

        //cập nhật món ăn

        //lập hóa đơn
        //hiển thị

        private void laphoadon()
        {
            cbxBanan.DataSource = null;
            bindingsrc.DataSource = listBan;
            cbxBanan.DataSource = bindingsrc.DataSource;
            cbxBanan.DisplayMember = "tenbanan";
            cbxBanan.ValueMember = "Mabanan";

            cbxNhanVien.DataSource = data.userAndPass("where tendangnhap='"
                            + username + "'");
            cbxNhanVien.DisplayMember = "tennhanvien";
            cbxNhanVien.ValueMember = "manhanvien";
            if (listBan.Count != 0)
            {
                cbxBanan.SelectedIndex = 0;
                try
                {
                    int index = 0;
                    foreach (var item in listBan)
                    {
                        if (item.Mabanan.Equals(cbxBanan.SelectedValue.ToString()))
                        {
                            index = listBan.IndexOf(item);
                            break;
                        }
                    }
                    bindingsrc.DataSource = listBan[index].td;
                    dgvLaphd_ctHD.DataSource = bindingsrc.DataSource;
                }
                catch (Exception ex) { MessageBox.Show("ERROR:" + ex.Message); }
                foreach (BanAn item in listBan)
                {
                    if (item.Mabanan == cbxBanan.SelectedValue.ToString())
                    {
                        txtgiamgia.Text = item.Khuyenmai + "";
                        break;
                    }
                }
            }
            else
            {
                //Hiển thị tên các cột
                bindingsrc.DataSource = new List<ThucDon>();
                dgvLaphd_ctHD.DataSource = bindingsrc.DataSource;
            }

        }
        private void LHD_cbxBanAnChange(object sender, EventArgs e)
        {
            try
            {
                int index = -1;
                foreach (var item in listBan)
                {
                    if (item.Tenbanan.Equals(cbxBanan.Text))
                    {
                        index = listBan.IndexOf(item);
                        break;
                    }
                }
                if (index >= 0)
                {
                    bindingsrc.DataSource = listBan[index].td;
                    dgvLaphd_ctHD.DataSource = bindingsrc.DataSource;
                }
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }

        }
        //cập nhật hóa đơn
        //hiển thị
        public void DisplayHoadon()
        {
            dgvDSHoaDon.DataSource = data.hienthiHD();
        }
        public void DisplayctHoadon(string mahd)
        {
            dgvChiTietHD.DataSource = data.hienthichitietHD(mahd);
        }
        private void capnhatHD()
        {
            cbxUpdateNV.DataSource = null;
            cbxUpdateNV.DataSource = data.danhsachNV();
            cbxUpdateNV.DisplayMember = "tennhanvien";
            cbxUpdateNV.ValueMember = "manhanvien";

            cbxUpdateKH.DataSource = data.danhsachKH();
            cbxUpdateKH.DisplayMember = "tenkhachhang";
            cbxUpdateKH.ValueMember = "makhachhang";

            cbxTenmenu.DataSource = data.danhsachmenu();
            cbxTenmenu.DisplayMember = "tenmenu";
            cbxTenmenu.ValueMember = "mamenu";

            string menu = cbxTenmenu.SelectedValue.ToString();
            cbxTenmonan.DataSource = data.danhsachmonan();
            cbxTenmonan.DisplayMember = "tenmonan";
            cbxTenmonan.ValueMember = "mamonan";
        }
        //clear
        public void clearHD()
        {
            txtMaHD.Clear();
            cbxUpdateNV.SelectedIndex = 0;
            dtpNgaylap.Value = DateTime.Now;
            cbxUpdateKH.SelectedIndex = 0;
            cbxTenmenu.SelectedIndex = 0;
            cbxTenmonan.SelectedIndex = 0;
            txtSoLuong.Clear();

        }
        //Cell_Click
        private void getHD(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0) return;
            try
            {
                txtMaHD.Text = dgvDSHoaDon.Rows[index].Cells[0].Value.ToString();
                dtpNgaylap.Text = dgvDSHoaDon.Rows[index].Cells[1].Value.ToString();
                cbxUpdateNV.Text = dgvDSHoaDon.Rows[index].Cells[2].Value.ToString();
                cbxUpdateKH.Text = dgvDSHoaDon.Rows[index].Cells[3].Value.ToString();

                DisplayctHoadon(txtMaHD.Text);
                if (dgvChiTietHD.Rows[0].Cells[0].Value.ToString() != "")
                {
                    cbxTenmenu.Text = dgvChiTietHD.Rows[0].Cells[0].Value.ToString();
                    cbxTenmonan.Text = dgvChiTietHD.Rows[0].Cells[1].Value.ToString();
                    txtSoLuong.Text = dgvChiTietHD.Rows[0].Cells[2].Value.ToString();
                }

            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }

        }
        private void dgvChiTietHD_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            try
            {
                cbxTenmenu.Text = dgvChiTietHD.Rows[index].Cells[0].Value.ToString();
                cbxTenmonan.Text = dgvChiTietHD.Rows[index].Cells[1].Value.ToString();
                txtSoLuong.Text = dgvChiTietHD.Rows[index].Cells[2].Value.ToString();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }
        //Thêm, sửa, xóa HD
        private void btnXoaHD_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn hóa đơn trước", "Thông báo");
                return;
            }
            DialogResult dlr = MessageBox.Show("Bạn có muốn xóa hóa đơn có mã: " + txtMaHD.Text + ".", "Thông báo", MessageBoxButtons.OKCancel);
            if (dlr == DialogResult.OK)
            {
                data.deleteHD(txtMaHD.Text);

                DisplayHoadon();
            }
            data.deleteHD(txtMaHD.Text);
            DisplayHoadon();
            clearHD();
        }

        private void btnSuaHD_Click(object sender, EventArgs e)
        {
            if (txtMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn hóa đơn trước", "Thông báo");
                return;
            }
            HoaDon hd = new HoaDon();
            hd.mahd = txtMaHD.Text;
            hd.manv = cbxUpdateNV.SelectedValue.ToString();
            hd.makh = cbxUpdateKH.SelectedValue.ToString();
            hd.ngaylap = dtpNgaylap.Value;

            string mamenu = cbxTenmenu.SelectedValue.ToString();
            string mamonan = cbxTenmonan.SelectedValue.ToString();
            int soluong = int.Parse(txtSoLuong.Text);


            data.updateHD(hd, mamonan, soluong);
            DisplayHoadon();
            DisplayctHoadon(dgvDSHoaDon.Rows[0].Cells[0].Value.ToString());
            clearHD();
        }

        //Nhân viên
        public void HienThiNhanVien()
        {
            txtmanvs.Text = "";
            txtmanvs.Focus();
            txttennvs.Text = "";
            cbbGioitinhs.Text = "";
            txtsodienthoais.Text = "";
            txtdiachis.Text = "";
            txttuois.Text = "";
            txttramasv.Text = "";
            cbxChucvu.Text = "";

            dataGridView2.DataSource = data.danhsachNV();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0)
                return;
            try
            {
                txtmanvs.Text = dataGridView2.Rows[index].Cells[0].Value.ToString();
                txttennvs.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
                cbbGioitinhs.Text = dataGridView2.Rows[index].Cells[2].Value.ToString();
                txttuois.Text = dataGridView2.Rows[index].Cells[3].Value.ToString();
                txtdiachis.Text = dataGridView2.Rows[index].Cells[4].Value.ToString();
                txtsodienthoais.Text = dataGridView2.Rows[index].Cells[5].Value.ToString();
                cbxChucvu.Text = dataGridView2.Rows[index].Cells[6].Value.ToString();
            }
            catch (Exception ex) { MessageBox.Show("ERROR: " + ex.Message); }
        }

        private void btnTims_Click(object sender, EventArgs e)
        {

            int n = dataGridView2.RowCount;
            Boolean a = false;
            for (int i = 0; i < n - 1; i++)
            {
                String b = dataGridView2.Rows[i].Cells[0].Value.ToString();
                if (txttramasv.Text.Equals(b))
                {
                    a = true;
                }
            }
            if (a == false)
            {
                MessageBox.Show("Mã nhân viên không hợp lệ.", "ERROR");
                return;
            }
            dataGridView2.DataSource = data.timnhanvien(txttramasv.Text);
            txtmanvs.Text = dataGridView2.Rows[0].Cells[0].Value.ToString();
            txttennvs.Text = dataGridView2.Rows[0].Cells[1].Value.ToString();
            cbbGioitinhs.Text = dataGridView2.Rows[0].Cells[2].Value.ToString();
            txttuois.Text = dataGridView2.Rows[0].Cells[3].Value.ToString();
            txtdiachis.Text = dataGridView2.Rows[0].Cells[4].Value.ToString();
            txtsodienthoais.Text = dataGridView2.Rows[0].Cells[5].Value.ToString();
            cbxChucvu.Text = dataGridView2.Rows[0].Cells[6].Value.ToString();
        }

        private void btnDSSVs_Click(object sender, EventArgs e)
        {
            HienThiNhanVien();
        }

        private void btnThems_Click(object sender, EventArgs e)
        {

            try
            {

                Themnhanvien tnv = new Themnhanvien();
                tnv.ShowDialog();
                HienThiNhanVien();
                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return;
            }
        }
        private void btnSuas_Click(object sender, EventArgs e)
        {
            txttennvs.Focus();
            if (txtmanvs.Text == "")
            {
                MessageBox.Show("Bạn phải chọn nhân viên trước", "Thông báo");
                return;
            }
            NhanVien nv = new NhanVien();
            nv.MaNV = txtmanvs.Text;
            nv.TenNV = txttennvs.Text;
            nv.GioiTinh = cbbGioitinhs.Text;
            nv.Tuoi = int.Parse(txttuois.Text);
            nv.DiaChi = txtdiachis.Text;
            nv.SoDienThoai = txtsodienthoais.Text;
            nv.chucvu = cbxChucvu.Text;
            data.suaNV(nv);
            HienThiNhanVien();
        }
        private void btnXoas_Click(object sender, EventArgs e)
        {
            if (txtmanvs.Text == "")
            {
                MessageBox.Show("Bạn phải chọn nhân viên trước", "Thông báo");
                return;
            }
            try
            {
                NhanVien nv = new NhanVien();
                nv.MaNV = txtmanvs.Text;
                if (txtmanvs.Text == "")
                    return;
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa nhân viên có mã: " + nv.MaNV + ".", "Thông báo", MessageBoxButtons.OKCancel);
                if (dlr == DialogResult.OK)
                {
                    data.xoaNV(nv);

                    HienThiNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return;
            }
        }
        private void btnChangePass_Click(object sender, EventArgs e)
        {
            txttennvs.Focus();
            if (txtmanvs.Text == "")
            {
                MessageBox.Show("Bạn phải chọn nhân viên trước", "Thông báo");
                return;
            }

            ChangePass doimk = new ChangePass();
            doimk.manhanvien = txtmanvs.Text;
            doimk.ShowDialog();
            HienThiNhanVien();
        }
        //Quản lý bàn ăn
        //xoa textbox
        public void clearTextbox()
        {
            txttenbanans.Clear();
            txtsoghes.Clear();
            cbbtrangthais.Text = "";
        }

        //danh sach ban an
        public void HienThiBanAn()
        {
            dataBanan.DataSource = data.danhsachban();
        }

        private void dataBanan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index < 0)
                return;
            txtMabanans.Text = dataBanan.Rows[index].Cells[0].Value.ToString();
            txttenbanans.Text = dataBanan.Rows[index].Cells[1].Value.ToString();
            txtsoghes.Text = dataBanan.Rows[index].Cells[2].Value.ToString();
            cbbtrangthais.Text = dataBanan.Rows[index].Cells[3].Value.ToString();
        }

        private void btnthembans_Click(object sender, EventArgs e)
        {
            try
            {
                string Mabanan = txtMabanans.Text;
                string Tenbanan = txttenbanans.Text;
                int Soghe = int.Parse(txtsoghes.Text);
                string Tinhtrang = cbbtrangthais.Text;
                data.ThemBA(new BanAn(Mabanan, Tenbanan, Soghe, Tinhtrang));
                HienThiBanAn();
                clearTextbox();
                txttenbanans.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR");
                return;
            }
        }

        private void btnsuabans_Click(object sender, EventArgs e)
        {
            if (txtMabanans.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bàn ăn trước", "Thông báo");
                return;
            }
            try
            {
                txttenbanans.Focus();
                BanAn ba = new BanAn();
                ba.Mabanan = txtMabanans.Text;
                ba.Tenbanan = txttenbanans.Text;
                ba.Soghe = int.Parse(txtsoghes.Text);
                ba.Tinhtrang = cbbtrangthais.Text;
                data.SuaBA(ba);

                HienThiBanAn();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
                return;
            }
        }

        private void btnXem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.quyenhan = quyenhan;
            f.ShowDialog();
            capnhatHD();
        }
        //Thêm ảnh cho mon an-Châu
        private void btAddImg_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fdbImg = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "JPEG|*jpg" })
            {
                if (fdbImg.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fdbImg.FileName;
                    ptbAddMonAn.Image = new Bitmap(fdbImg.FileName);
                }
            }
        }
        string des;
        private void HienThiDsMonAn()
        {
            try
            {
                cbxMaMenu.DataSource = data.danhsachmenu();
                cbxMaMenu.DisplayMember = "tenmenu";
                cbxMaMenu.ValueMember = "mamenu";
                dataGridView1.DataSource = data.danhsachmonan();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtTenMonAn.Text == "" || txtTenMonAn.Text == "" || txtPath.Text == "")
            {
                MessageBox.Show("Không được để trống tên món ăn, mã menu và đường dẫn ảnh");
                return;
            }
            try
            {
                MonAn monAn = new MonAn();
                monAn.tenMonAn = txtTenMonAn.Text;
                monAn.gia = int.Parse(txtGia.Text);
                monAn.moTa = txtMoTa.Text;
                monAn.maMenu = cbxMaMenu.SelectedValue.ToString();
                string des = @"Image\" + (data.getMaxOfTable("monan", "mamonan") + 1);
                File.Copy(txtPath.Text, des + ".jpg", true);
                monAn.anh = des;
                data.addMonAn(monAn);
                Clear_Board();
                HienThiDsMonAn();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Error");
            }

        }

        private void Clear_Board()
        {
            txtMaMonAn.Clear();
            txtTenMonAn.Clear();
            txtGia.Clear();
            txtMoTa.Clear();
            cbxMaMenu.SelectedIndex = 0;
            txtPath.Clear();
            ptbAddMonAn.Image = System.Drawing.Image.FromFile(@"Image\logo.jpg");
        }

        private void get1MonAn(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int hang = e.RowIndex;
                txtMaMonAn.Text = dataGridView1.Rows[hang].Cells[0].Value.ToString();
                txtTenMonAn.Text = dataGridView1.Rows[hang].Cells[1].Value.ToString();
                txtGia.Text = dataGridView1.Rows[hang].Cells[2].Value.ToString();
                txtMoTa.Text = dataGridView1.Rows[hang].Cells[3].Value.ToString();
                des = dataGridView1.Rows[hang].Cells[4].Value.ToString();
                ptbAddMonAn.Image = System.Drawing.Image.FromFile(des + ".jpg");
                cbxMaMenu.SelectedIndex = int.Parse(dataGridView1.Rows[hang].Cells[5].Value.ToString()) - 1;

            }
            catch (Exception) { }
        }
        //cập nhật món ăn

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            MonAn monAn = new MonAn();
            if (txtMaMonAn.Text == "" || des == "")
            {
                MessageBox.Show("Bạn phải chọn món ăn trước", "Thông báo");
                return;
            }
            ptbAddMonAn.Image.Dispose();
            monAn.maMonAn = txtMaMonAn.Text;
            monAn.tenMonAn = txtTenMonAn.Text;
            monAn.gia = int.Parse(txtGia.Text);
            monAn.moTa = txtMoTa.Text;
            monAn.maMenu = cbxMaMenu.SelectedValue.ToString();
            if (txtPath.Text == "")
            {
                data.capNhatMonAnKhongAnh(monAn);
                Clear_Board();
                HienThiDsMonAn();
                return;
            }
            //string des = @"Image\" + (data.getMaxOfTable("monan", "mamonan") + 1);
            File.Delete(des + ".jpg");
            monAn.anh = des;
            data.capNhatMonAn(monAn);
            File.Copy(txtPath.Text, des + ".jpg");
            Clear_Board();
            HienThiDsMonAn();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtMaMonAn.Text == "")
            {
                MessageBox.Show("Bạn phải chọn món ăn trước", "Thông báo");
                return;
            }
            DialogResult d = MessageBox.Show("Bạn chắc chắn muôn xóa món ăn này chứ ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (d.Equals(DialogResult.Yes))
            {
                ptbAddMonAn.Image.Dispose();
                File.Delete(des + ".jpg");
                String maMonAn = txtMaMonAn.Text;
                data.deleteMonAn(maMonAn);
                Clear_Board();
                HienThiDsMonAn();
            }
        }
        public void DisplayMenu(DataTable dt)
        {
            dgvMenu.DataSource = dt;
        }
        public void clearMN()
        {
            txtTenMenu.Focus();
            txtMaMenu.Clear();
            txtTenMenu.Clear();
            txtMoTaMenu.Clear();
        }
        private void btAddMenu_Click(object sender, EventArgs e)
        {
            Menu mn = new Menu();
            mn.tenmenu = txtTenMenu.Text;
            mn.mota = txtMoTaMenu.Text;
            data.addMenu(mn);
            DisplayMenu(data.hienthiMN());
            clearMN();
        }

        private void btUpdMenu_Click(object sender, EventArgs e)
        {
            if (txtMaMenu.Text == "")
            {
                MessageBox.Show("Bạn phải chọn menu trước", "Thông báo");
                return;
            }
            Menu mn = new Menu();
            mn.mamenu = txtMaMenu.Text;
            mn.tenmenu = txtTenMenu.Text;
            mn.mota = txtMoTaMenu.Text;

            data.updateMN(mn);
            DisplayMenu(data.hienthiMN());
            clearMN();
        }

        private void btDelMenu_Click(object sender, EventArgs e)
        {
            if (txtMaMenu.Text == "")
            {
                MessageBox.Show("Bạn phải chọn menu trước", "Thông báo");
                return;
            }
            data.deleteMenu(txtMaMenu.Text);
            DisplayMenu(data.hienthiMN());
            clearMN();
        }

        public void danhsachcbMMN()
        {
            txtTenMenu.Focus();
            cbMMN.DataSource = data.danhsachmamenu();
            cbMMN.DisplayMember = "tenmenu";
            cbMMN.ValueMember = "mamenu";
        }
        private void btnFind_Click(object sender, EventArgs e)
        {
            DisplayMenu(data.timMenu(txtFind.Text));
        }

        private void cbMMN_TextChanged(object sender, EventArgs e)
        {
            dgvMenu.DataSource = data.danhsachmenutheoma(
                cbMMN.SelectedValue.ToString());
        }

        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            try
            {
                txtMaMenu.Text = dgvMenu.Rows[index].Cells[0].Value.ToString();
                txtTenMenu.Text = dgvMenu.Rows[index].Cells[1].Value.ToString();
                txtMoTaMenu.Text = dgvMenu.Rows[index].Cells[2].Value.ToString();
            }
            catch (Exception) { }

        }

        private void LHD_btnLHD_Click(object sender, EventArgs e)
        {
            if (txtMoney.Text == "" || txtMoney.Text == "0")
            {
                return;
            }
            if (listBan.Count > 0)
            {
                ////them vao bang kh
                KhachHang kh = new KhachHang();
                HoaDon hd = new HoaDon();
                kh.tenkh = txtTenKH.Text;
                kh.diachi = txtDiaChiKH.Text;
                kh.dienthoai = txtDienThoaiKH.Text;

                if (data.timKH(kh).Rows.Count <= 1)
                {
                    data.ThemKH(kh);
                    hd.makh = data.getMaxOfTable("khachhang", "makhachhang") + "";
                }
                else
                {
                    hd.makh = data.timKH(kh).Rows[0].ItemArray[0].ToString();
                }
                data.SuaBAvstenBA(cbxBanan.Text);

                ////Them vao bang HD
                hd.ngaylap = dtpNgayLap_LHD.Value;
                hd.manv = cbxNhanVien.SelectedValue.ToString();
                hd.maban = cbxBanan.SelectedValue.ToString();
                string mahd = data.getMaxOfTable("hoadon", "mahoadon") + "";
                data.addHD(hd);
                //Them vao chi tiet HD
                mahd = data.getMaxOfTable("hoadon", "mahoadon") + "";
                foreach (BanAn item in listBan)
                {
                    if (hd.maban == item.Mabanan)
                    {
                        foreach (ThucDon monan in item.td)
                        {
                            DataTable dsmonan = data.danhsachmonan();
                            foreach (DataRow row in dsmonan.Rows)
                            {
                                if (monan.tenmon == row.ItemArray[1].ToString())
                                {
                                    string mamon = row.ItemArray[0].ToString();
                                    data.themctHD(mahd, mamon, monan.soluong);
                                    break;
                                }
                            }
                        }
                        //hiển thị thông báo muốn in hauy không
                        DialogResult rs = MessageBox.Show("Bạn có muốn in hóa đơn không?", "Thông báo", MessageBoxButtons.OKCancel);
                        if (rs == DialogResult.OK)
                        {
                            Inhoadon();
                        }
                        //clear ban
                        listBan.Remove(item);
                        listBanTrong.Add(item);
                        laphoadon();
                        return;
                    }
                }
            }


        }

        private void GM_dgvDSMonDaChon(object sender, EventArgs e)
        {
            txtGoimonKM.Text = tinhKhuyenMai() + "";
        }

        private void GM_dgvDSMonDaChon(object sender, DataGridViewCellEventArgs e)
        {
            if (radGoiMoi.Checked)
            {
                txtGoimonKM.Text = tinhKhuyenMai() + "";
                txtGoimonMoney.Text = tinhTien() + "";
            }

        }

        private void btAddImg_Click_1(object sender, EventArgs e)
        {

            using (OpenFileDialog fdbImg = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "JPEG|*jpg" })
            {
                if (fdbImg.ShowDialog() == DialogResult.OK)
                {
                    txtPath.Text = fdbImg.FileName;
                    ptbAddMonAn.Image = new Bitmap(fdbImg.FileName);
                }
            }
        }

        private void btnCal_Click(object sender, EventArgs e)
        {
            foreach (BanAn item in listBan)
            {
                if (item.Mabanan == cbxBanan.SelectedValue.ToString())
                {
                    txtMoney.Text = (item.tien - 1.0 * item.Khuyenmai * item.tien / 100) + "";
                    break;
                }
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {
            data.DongCuaHang(listBan);
            HienThiBanAn();
        }

        private void btnxoabans_Click(object sender, EventArgs e)
        {
            if (txtMabanans.Text == "")
            {
                MessageBox.Show("Bạn phải chọn bàn ăn trước", "Thông báo");
                return;
            }
            try
            {
                BanAn ba = new BanAn();
                ba.Mabanan = txtMabanans.Text;
                DialogResult dlr = MessageBox.Show("Bạn có muốn xóa bàn có mã:" + ba.Mabanan + ".", "Thông báo", MessageBoxButtons.OKCancel);
                if (dlr == DialogResult.OK)
                {
                    data.XoaBA(ba);

                    clearTextbox();
                    txtMabanans.Clear();
                    HienThiBanAn();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR");
                return;
            }
        }
        private void picClose_Click(object sender, EventArgs e)
        {
            data.DongCuaHang(listBan);
            listBan.Clear();
        }
        private void picLogout_Click(object sender, EventArgs e)
        {
            //ghi du lieu ra file tam
            data.writeFile(listBan);
            Close();
        }
        private void Inhoadon()
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF (*.pdf)|*.pdf";
                sfd.FileName = "Hoadon.pdf";
                string filename = string.Empty;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    filename = sfd.FileName;
                }
                else
                {
                    return;
                }

                Document doc = new Document(PageSize.A4.Rotate());
                BaseFont timenewrm = BaseFont.CreateFont(@"UTM Times.ttf", BaseFont.IDENTITY_H, true);
                var f15_bold = new iTextSharp.text.Font(timenewrm, 15, iTextSharp.text.Font.BOLD);
                var f12_normal = new iTextSharp.text.Font(timenewrm, 9, iTextSharp.text.Font.NORMAL);
                var f14_normal = new iTextSharp.text.Font(timenewrm, 14, iTextSharp.text.Font.NORMAL);
                Random rd = new Random();
                int name = rd.Next(1, 1000);
                FileStream os = new FileStream(filename, FileMode.Create);

                using (os)
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, os);
                    //PdfWriter.GetInstance(doc, os);
                    doc.Open();
                    PdfPTable table1 = new PdfPTable(1);
                    PdfPCell cell1 = new PdfPCell(new Phrase("HÓA ĐƠN", f15_bold));
                    cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell1.HorizontalAlignment = Element.ALIGN_CENTER;
                    table1.AddCell(cell1);
                    doc.Add(table1);

                    //
                    table1 = new PdfPTable(2);
                    cell1 = new PdfPCell(new Phrase("Mã hóa đơn: " + (data.getMaxOfTable("khachhang", "makhachhang") - 1).ToString(), f12_normal));
                    PdfPCell cell2 = new PdfPCell(new Phrase("Ngày lập: " + (dtpNgayLap_LHD.Value).ToString(), f12_normal));

                    cell1.HorizontalAlignment = Element.ALIGN_LEFT;

                    cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell2.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell2.PaddingRight = 20;
                    cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    table1.AddCell(cell1);
                    table1.AddCell(cell2);
                    doc.Add(table1);

                    table1 = new PdfPTable(3);

                    cell1 = new PdfPCell(new Phrase("\n\nTên khách hàng: " + txtTenKH.Text, f14_normal));
                    cell2 = new PdfPCell(new Phrase("", f14_normal));
                    PdfPCell cell3 = new PdfPCell(new Phrase("\n\nMã nhân viên: " + cbxNhanVien.SelectedValue, f14_normal)); ;
                    PdfPCell cell4 = new PdfPCell(new Phrase("Địa chỉ: " + txtDiaChiKH.Text, f14_normal));
                    PdfPCell cell5 = new PdfPCell(new Phrase("", f14_normal));
                    PdfPCell cell6 = new PdfPCell(new Phrase("Tên nhân viên: " + cbxNhanVien.Text, f14_normal));
                    PdfPCell cell7 = new PdfPCell(new Phrase("Số điện thoại: " + txtDienThoaiKH.Text, f14_normal));

                    cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell6.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell7.Border = iTextSharp.text.Rectangle.NO_BORDER;

                    cell4.PaddingTop = 10;
                    cell6.PaddingTop = 10;
                    cell7.PaddingTop = 10;
                    cell7.PaddingBottom = 20;

                    table1.AddCell(cell1);
                    table1.AddCell(cell2);
                    table1.AddCell(cell3);
                    table1.AddCell(cell4);
                    table1.AddCell(cell5);
                    table1.AddCell(cell6);


                    doc.Add(table1);
                    table1 = new PdfPTable(1);
                    table1.AddCell(cell7);
                    doc.Add(table1);

                    PdfPTable pdfTable = new PdfPTable(5);


                    pdfTable.WidthPercentage = 80;
                    pdfTable.HorizontalAlignment = Element.ALIGN_CENTER;
                    PdfPCell cell11 = new PdfPCell(new Phrase("Tên món", f14_normal));
                    PdfPCell cell12 = new PdfPCell(new Phrase("Tên menu", f14_normal));
                    PdfPCell cell13 = new PdfPCell(new Phrase("Đơn giá", f14_normal));
                    PdfPCell cell14 = new PdfPCell(new Phrase("Giảm giá", f14_normal));
                    PdfPCell cell15 = new PdfPCell(new Phrase("Số lượng", f14_normal));
                    PdfPCell cell18 = new PdfPCell(new Phrase("Thành tiền", f14_normal));
                    cell11.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell12.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell13.HorizontalAlignment = Element.ALIGN_CENTER;

                    cell15.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell18.HorizontalAlignment = Element.ALIGN_CENTER;


                    pdfTable.AddCell(cell11);
                    pdfTable.AddCell(cell12);
                    pdfTable.AddCell(cell13);
                    pdfTable.AddCell(cell15);

                    pdfTable.AddCell(cell18);

                    for (int i = 0; i < dgvLaphd_ctHD.Rows.Count; i++)
                    {
                        for (int j = 0; j < dgvLaphd_ctHD.Columns.Count; j++)
                        {

                            PdfPCell cell16 = new PdfPCell(new Phrase(dgvLaphd_ctHD.Rows[i].Cells[j].Value.ToString(), f14_normal));
                            cell16.HorizontalAlignment = Element.ALIGN_CENTER;
                            pdfTable.AddCell(cell16);

                        }

                        PdfPCell cell19 = new PdfPCell(new Phrase(((double)(float.Parse(dgvLaphd_ctHD.Rows[i].Cells[2].Value.ToString()) * float.Parse(dgvLaphd_ctHD.Rows[i].Cells[3].Value.ToString()))).ToString(), f14_normal));
                        cell19.HorizontalAlignment = Element.ALIGN_CENTER;
                        pdfTable.AddCell(cell19);

                    }

                    doc.Add(pdfTable);
                    table1 = new PdfPTable(3);
                    PdfPCell cell17 = new PdfPCell(new Phrase("Giảm giá: " + txtgiamgia.Text + " %", f14_normal));
                    cell17.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell17.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell17.PaddingTop = 10;

                    cell5 = new PdfPCell(new Phrase("", f14_normal));
                    cell5.Border = iTextSharp.text.Rectangle.NO_BORDER;


                    PdfPCell cell20 = new PdfPCell(new Phrase("Tổng tiền: " + txtMoney.Text + " VNĐ", f14_normal));
                    cell20.Border = iTextSharp.text.Rectangle.NO_BORDER;
                    cell20.HorizontalAlignment = Element.ALIGN_LEFT;
                    table1.AddCell(cell5);
                    table1.AddCell(cell5);
                    table1.AddCell(cell17);
                    table1.AddCell(cell5);
                    table1.AddCell(cell5);
                    table1.AddCell(cell20);
                    doc.Add(table1);

                    doc.Close();
                    os.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);

            }
        }

        //Thống kê
        public void thongKe()
        {
            //Biểu đồ món ăn bán chạy
            chart1.DataSource = data.thongKeMonAn(3);
            chart1.Series["Món ăn đã bán"].XValueMember = "tenmonan";
            chart1.Series["Món ăn đã bán"].YValueMembers = "soluong";
            //Món ăn bán chạy
            string monan = data.thongKeMonAn(1).Rows[0].ItemArray[0].ToString();
            string soluong = data.thongKeMonAn(1).Rows[0].ItemArray[1].ToString();
            lblMABanChay.Text = "" + monan + "(" + soluong + ")";
            //Số lượng  khách đến
            lblTongKhach.Text = "" + data.danhsachKH().Rows.Count;
            //Tổng doanh thu
            long sum = 0;
            foreach (DataRow row in data.hienthiHD().Rows)
            {
                if (row != null)
                {
                    sum += long.Parse(row.ItemArray[5].ToString());
                }
            }
            lblDoanhThu.Text = sum.ToString("N0") + " VNĐ";
        }
        //in thống kê
        private void inThongKe(object sender, EventArgs e)
        {
            if (dgvThongKe.Rows.Count <= 1)
            {
                MessageBox.Show("Bạn phải thống kê trước", "Thông báo");
                return;
            }
            //Khởi tạo kích thước bản in
            Document doc = new Document(PageSize.A4.Rotate());
            //Khởi tạo các font chữ sử dụng
            BaseFont timenewrm = BaseFont.CreateFont(@"UTM Times.ttf", BaseFont.IDENTITY_H, true);
            var f16_bold = new iTextSharp.text.Font(timenewrm, 16, iTextSharp.text.Font.BOLD);
            var f14_bold = new iTextSharp.text.Font(timenewrm, 14, iTextSharp.text.Font.BOLD);
            var f14_normal = new iTextSharp.text.Font(timenewrm, 14, iTextSharp.text.Font.NORMAL);
            //Khởi tạo 1 biến random
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "PDF (*.pdf)|*.pdf";
            sfd.FileName = "Outputthongke.pdf";
            string filename = string.Empty;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                filename = sfd.FileName;
            }
            else
            {
                return;
            }

            Random rd = new Random();
            //Khởi tạo biến name kiểu int
            int name = rd.Next(1, 1000);
            FileStream os = new FileStream(filename, FileMode.Create);

            using (os)
            {
                //Khởi tạo đối tuọng writer
                PdfWriter writer = PdfWriter.GetInstance(doc, os);
                doc.Open();
                //Tạo 1 bảng có 6 cột
                PdfPTable table1 = new PdfPTable(6);
                //Tiêu đề file in
                PdfPCell cell = new PdfPCell(new Phrase("THỐNG KÊ", f16_bold));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase("Nhà hàng: Bếp mẹ Ỉn", f14_normal));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", f16_bold));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table1.AddCell(cell);
                //dong 2
                cell = new PdfPCell(new Phrase("Ngày lập: " + DateTime.Now, f14_normal));
                cell.Colspan = 4;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase("Tổng số khách: " + lblTongKhach.Text, f14_normal));
                cell.Colspan = 2;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                //dòng 3
                cell = new PdfPCell(new Phrase("Món ăn bán chạy nhất: " + lblMABanChay.Text, f14_normal));
                cell.Colspan = 4;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase("Tổng doanh thu: " + lblDoanhThu.Text, f14_normal));
                cell.Colspan = 2;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_MIDDLE;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                //dòng 4
                cell = new PdfPCell(new Phrase("Thống kê: " + loaiThongKe, f14_normal));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                //dong 5
                cell = new PdfPCell(new Phrase("Thống kê hóa đơn", f14_bold));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase(" ", f16_bold));
                cell.Colspan = 6;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table1.AddCell(cell);
                //bảng số liệu
                PdfPTable table2 = new PdfPTable(4);
                table2.TotalWidth = 400;
                //bảng 2-dòng 1
                cell = new PdfPCell(new Phrase("Mã hóa đơn", f14_normal));
                cell.Border = iTextSharp.text.Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Ngày lập", f14_normal));
                cell.Border = iTextSharp.text.Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Tên nhân viên", f14_normal));
                cell.Border = iTextSharp.text.Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table2.AddCell(cell);
                cell = new PdfPCell(new Phrase("Tên khách hàng", f14_normal));
                cell.Border = iTextSharp.text.Rectangle.BOX;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                table2.AddCell(cell);

                for (int i = 0; i < dgvThongKe.Rows.Count - 1; i++)
                {
                    DataGridViewRow item = dgvThongKe.Rows[i];
                    cell = new PdfPCell(new Phrase(item.Cells[0].Value.ToString(), f14_normal));
                    cell.Border = iTextSharp.text.Rectangle.BOX;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.Cells[1].Value.ToString(), f14_normal));
                    cell.Border = iTextSharp.text.Rectangle.BOX;
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.Cells[2].Value.ToString(), f14_normal));
                    cell.Border = iTextSharp.text.Rectangle.BOX;
                    table2.AddCell(cell);
                    cell = new PdfPCell(new Phrase(item.Cells[3].Value.ToString(), f14_normal));
                    cell.Border = iTextSharp.text.Rectangle.BOX;
                    table2.AddCell(cell);
                }
                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table1.AddCell(cell);

                cell = new PdfPCell(table2);
                cell.Colspan = 4;
                table1.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                table1.AddCell(cell);
                //Phần ký tên
                cell = new PdfPCell();
                cell.Colspan = 4;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Người lập Ký tên", f14_normal));
                cell.Colspan = 2;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(cell);
                cell = new PdfPCell();
                cell.Colspan = 4;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(cell);
                cell = new PdfPCell(new Phrase(user.TenNV, f14_normal));
                cell.Colspan = 2;
                cell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                table1.AddCell(cell);

                doc.Add(table1);
                doc.Close();

            }
        }

        private void btn_Tk_Click(object sender, EventArgs e)
        {
            loaiThongKe = "Từ ngày" + dtpFromDate.Value.ToString("dd/MM/yyyy")
                + " đến ngày " + dtpToDate.Value.ToString("dd/MM/yyyy");
            string fromDate = dtpFromDate.Value.ToString("MM/dd/yyyy");
            string toDate = dtpToDate.Value.ToString("MM/dd/yyyy");
            dgvThongKe.DataSource = data.thongKeKhoangNgay(fromDate, toDate);
        }

        private void btnDayly_Click(object sender, EventArgs e)
        {
            loaiThongKe = "Ngày " + dtpThongKeTheoNgay.Value.ToString("dd/MM/yyyy");
            string ngay = dtpThongKeTheoNgay.Value.ToString("MM/dd/yyyy");
            dgvThongKe.DataSource = data.thongKeNgay(ngay);
        }

        private void btn_Monthly_Click(object sender, EventArgs e)
        {
            loaiThongKe = "Tháng " + dtpThongKeThang.Value.ToString("MM//yyyy");
            int thang = int.Parse(dtpThongKeThang.Value.ToString("MM"));
            int nam = int.Parse(dtpThongKeThang.Value.ToString("yyyy"));
            dgvThongKe.DataSource = data.thongKeThang(thang, nam);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            data.writeFile(listBan);
        }

        private void dgvdsMondachon_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            txtGoimonKM.Text = tinhKhuyenMai() + "";
            txtGoimonMoney.Text = tinhTien() + "";
        }

        private void goimonChange(object sender, EventArgs e)
        {
            if (radGoiMoi.Checked)
            {
                bindingsrc.DataSource = listBanTrong;
                cbxGoimonBan.DataSource = bindingsrc;
                cbxGoimonBan.DisplayMember = "tenbanan";
                cbxGoimonBan.ValueMember = "mabanan";
                dgvdsMondachon.Rows.Clear();

                txtGoimonKM.Text = "0";
                txtGoimonMoney.Text = "0";

            }
            else if (radGoiThem.Checked)
            {
                bindingsrc.DataSource = listBan;
                cbxGoimonBan.DataSource = bindingsrc.DataSource;
                cbxGoimonBan.DisplayMember = "tenbanan";
                cbxGoimonBan.ValueMember = "mabanan";

                cbxGoimonBan.SelectedIndex = 0;
                try
                {
                    int index = 0;
                    foreach (var item in listBan)
                    {
                        if (item.Mabanan.Equals(cbxGoimonBan.SelectedValue.ToString()))
                        {
                            index = listBan.IndexOf(item);
                            break;
                        }
                    }
                    HienThiDSmondachon(listBan[index].td);
                }
                catch (Exception ex) { MessageBox.Show("ERROR:" + ex.Message); }
            }
        }

        private void cbxGoimonBan_TextChanged(object sender, EventArgs e)
        {
            if (radGoiThem.Checked)
            {
                int index = 0;
                foreach (var item in listBan)
                {
                    if (item.Tenbanan.Equals(cbxGoimonBan.Text))
                    {
                        index = listBan.IndexOf(item);
                        break;
                    }
                }
                HienThiDSmondachon(listBan[index].td);
                txtSoghe.Text = listBan[index].Soghe + "";
                txtGoimonKM.Text = listBan[index].Khuyenmai + "";
                txtGoimonMoney.Text = listBan[index].tien + "";
            }
        }
        private void HienThiDSmondachon(List<ThucDon> listTD)
        {
            dgvdsMondachon.Rows.Clear();
            int i = 0;
            foreach (ThucDon item in listTD)
            {
                dgvdsMondachon.Rows.Add();
                dgvdsMondachon.Rows[i].Cells[0].Value = item.tenmon;
                dgvdsMondachon.Rows[i].Cells[1].Value = item.tenmenu;
                dgvdsMondachon.Rows[i].Cells[2].Value = item.dongia;
                dgvdsMondachon.Rows[i].Cells[3].Value = item.soluong;
                i++;
            }
        }
    }
}
