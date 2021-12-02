using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace QLNhaHang
{
    class Utilities
    {
        string filename = @"temp\tempFile.bin";
        public string makh { get; set; }
        SqlConnection con;
        public Utilities()
        {
            string constr = @"Data Source=.\SQLEXPRESS;Initial Catalog=QLNhaHang;Integrated Security=True";

            con = new SqlConnection(constr);
        }

        //lay du lieu chi tiet mon an

        public string layDuongDanImg(string tenmonan)
        {
            string result = "";
            DataTable table = danhsachmonan();
            int length = table.Rows.Count;
            for (int i = 0; i < length; i++)
            {
                if (tenmonan == table.Rows[i].ItemArray[1].ToString())
                {
                    result = table.Rows[i].ItemArray[4].ToString();

                }
            }
            return result;
        }

        //Nhan vien
        //Lay du lieu nhan vien
        public DataTable userAndPass(string dieukien)
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select * from nhanvien " + dieukien;
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }
            con.Close();
            return table;
        }
        public DataTable danhsachNV()
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select MaNhanVien,TenNhanVien,GioiTinh,Tuoi,DiaChi,SoDienThoai,quyen from nhanvien";
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }
            con.Close();
            return table;
        }
        //tim nhan vien
        public DataTable timnhanvien(String manv)
        {
            DataTable table = new DataTable();
            string sql = "select MaNhanVien,TenNhanVien,GioiTinh,Tuoi,DiaChi,SoDienThoai,quyen from nhanvien where manhanvien=" + int.Parse(manv);
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //Them, sua, xoa nhan vien.
        public void themNV(NhanVien s)
        {
            string sql = "insert into nhanvien values(@tennhanvien," +
                "@gioitinh,@tuoi,@diachi,@sodienthoai,@user,@pass,@chucvu)";
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tennhanvien", s.TenNV);
            cmd.Parameters.AddWithValue("gioitinh", s.GioiTinh);
            cmd.Parameters.AddWithValue("tuoi", s.Tuoi);
            cmd.Parameters.AddWithValue("diachi", s.DiaChi);
            cmd.Parameters.AddWithValue("sodienthoai", s.SoDienThoai);
            cmd.Parameters.AddWithValue("user", s.user);
            cmd.Parameters.AddWithValue("pass", s.pass);
            cmd.Parameters.AddWithValue("chucvu", s.chucvu);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void suaNV(NhanVien s)
        {
            con.Open();
            string sql = "update nhanvien set tennhanvien=@tennhanvien," +
                "gioitinh=@gioitinh,tuoi=@tuoi," +
                "diachi=@diachi,sodienthoai=@sodienthoai," +
                "quyen=@chucvu where manhanvien=" + int.Parse(s.MaNV);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tennhanvien", s.TenNV);
            cmd.Parameters.AddWithValue("gioitinh", s.GioiTinh);
            cmd.Parameters.AddWithValue("tuoi", s.Tuoi);
            cmd.Parameters.AddWithValue("diachi", s.DiaChi);
            cmd.Parameters.AddWithValue("sodienthoai", s.SoDienThoai);
            cmd.Parameters.AddWithValue("chucvu", s.chucvu);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void xoaNV(NhanVien s)
        {
            con.Open();
            string sql = "delete from nhanvien where manhanvien=@manhanvien";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("manhanvien", int.Parse(s.MaNV));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void doiPass(string u, string s)
        {
            con.Open();
            try
            {
                string sql = "update nhanvien set " +
                    "matkhau=@pass where manhanvien=" + int.Parse(u);
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("pass", s);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + ex.Message);
            }
            con.Close();
        }
        public String quenPass(string a, string b, string c)
        {
           
                DataTable table = new DataTable();
                String kq = "";
                string sql = "select * from nhanvien where manhanvien=" + int.Parse(a);
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                adapter.Fill(table);
                foreach (DataRow dt in table.Rows)
                {
                    if (dt["manhanvien"].ToString().Trim().Equals(a.Trim()) == true
                        && dt["tennhanvien"].ToString().Trim().Equals(b.Trim()) == true
                        && dt["sodienthoai"].ToString().Trim().Equals(c.Trim()) == true)
                    {
                        kq = "Mật khẩu của bạn là: " + dt["matkhau"].ToString();
                    }
                    else
                        kq = "Không có nhân viên đã nhập tồn tại";
                }
            
            con.Close();
            return kq;
        }
        //kiểm tra mật khẩu
        public Boolean ktmatKhau(string u, string s)
        {
            DataTable table = new DataTable();
            Boolean kq = false;
            string sql = "select * from nhanvien where manhanvien=" + int.Parse(u);
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(table);
            foreach (DataRow dt in table.Rows)
            {
                if (dt["matkhau"].ToString().Equals(s) == true)
                {
                    kq = true;
                }
            }
            con.Close();
            return kq;
        }
        //bàn ăn
        //lay du lieu ban an
        public DataTable danhsachban()
        {
            DataTable table = new DataTable();
            string sql = "select * from banan";
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        public DataTable dsBanTheoTinhTrang(string tinhtrang)
        {
            DataTable table = new DataTable();
            string sql = "select * from banan where tinhtrang=N'" + tinhtrang + "'";
            con.Open();
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //Them sua xoa ban an
        public bool ThemBA(BanAn s)
        {
            String sql = "insert into banan values(@tenbanan,@soghe,@tinhtrang)";

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tenbanan", s.Tenbanan);
                cmd.Parameters.AddWithValue("soghe", s.Soghe);
                cmd.Parameters.AddWithValue("tinhtrang", s.Tinhtrang);
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
        public void XoaBA(BanAn s)
        {
            String sql = "delete from banan where mabanan=" + int.Parse(s.Mabanan);
            con.Open();
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SuaBA(BanAn s)
        {
            con.Open();
            string sql = "update banan set tenban=@tenbanan,soghe=@soghe,tinhtrang=@tinhtrang where mabanan=" + int.Parse(s.Mabanan);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tenbanan", s.Tenbanan);
            cmd.Parameters.AddWithValue("soghe", s.Soghe);
            cmd.Parameters.AddWithValue("tinhtrang", s.Tinhtrang);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SuaBAvstenBA(string tenba)
        {
            con.Open();
            string sql = "update banan set soghe=4,tinhtrang=N'Trống' where tenban=N'" + tenba + "'";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void DongCuaHang(List<BanAn> listBan)
        {

            string sql = "update banan set tinhtrang=@tinhtrang where mabanan=@mabanan";
            DataRowCollection list = dsBanTheoTinhTrang("Có khách").Rows;
            foreach (DataRow item in list)
            {
                con.Open();
                int maban = int.Parse(item.ItemArray[0].ToString());
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("mabanan", maban);
                cmd.Parameters.AddWithValue("tinhtrang", "Trống");
                cmd.ExecuteNonQuery();
                con.Close();
            }


        }

        //Lay danh sach khach hang
        public DataTable danhsachKH()
        {
            DataTable table = new DataTable();
            con.Open();

            string sql = "select * from khachhang";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        public DataTable timKH(KhachHang kh)
        {
            DataTable table = new DataTable();
            con.Open();
            string sql = "select * from khachhang " +
                " where tenkhachhang=N'" + kh.tenkh + "'" +
                " and diachi=N'" + kh.diachi + "'" +
                " and sodienthoai='" + kh.dienthoai + "'";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //them sua xoa khach hang
        public void ThemKH(KhachHang s)
        {
            try
            {
                con.Open();
                String sql = "insert into khachhang values(@tenkhachhang,@diachi,@sodienthoai)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("tenkhachhang", s.tenkh);
                cmd.Parameters.AddWithValue("diachi", s.diachi);
                cmd.Parameters.AddWithValue("sodienthoai", s.dienthoai);
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            finally { con.Close(); }

        }
        public void XoaKH(KhachHang s)
        {
            con.Open();
            String sql = "delete from khachhang where makhachhang=" + int.Parse(s.makh);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void SuaKH(KhachHang s)
        {
            con.Open();
            string sql = "update khachhang set tenkhachhang=@tenkhachhang,diachi=@diachi," +
                "sodienthoai=@sodienthoai where makhachhang=" + int.Parse(s.makh);
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tenkhachhang", s.tenkh);
            cmd.Parameters.AddWithValue("diachi", s.diachi);
            cmd.Parameters.AddWithValue("sodienthoai", s.dienthoai);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        //menu
        //lay du lieu
        public DataTable danhsachmenu()
        {
            DataTable table = new DataTable();
            con.Open();

            string sql = "select * from Menu";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }

        //mon an
        //lay du lieu mon an

        public DataTable danhsachmonan()
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select * from MonAn";
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }
            con.Close();
            return table;
        }
        //danh sach loai mon an
        public DataTable danhsachloai()
        {
            DataTable table = new DataTable();
            con.Open();
            string sql = "select distinct mota from MonAn";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        public DataTable danhsachmonantheoloai(string loaimonan)
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select tenmonan,menu.tenmenu,gia,monan.mota from monan,menu " +
                "where monan.mamenu=menu.mamenu and monan.mota=N'" + loaimonan + "'";
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }
            con.Close();
            return table;
        }

        public DataTable danhsachmonantheomenu(string mamenu)
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select tenmonan,menu.tenmenu,gia,monan.mota from monan,menu " +
                 "where monan.mamenu=menu.mamenu and monan.mamenu=" + int.Parse(mamenu);
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }

            con.Close();
            return table;
        }
        public DataTable dsMonAnTheoTenMenu(string tenmenu)
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select tenmonan,tenmenu,gia,monan.mota from monan,menu " +
                 "where monan.mamenu=menu.mamenu and tenmenu=N'" + tenmenu + "'";
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }

            con.Close();
            return table;
        }
        //public DataTable dsMonAnTheoMenu(string mamenu)
        //{
        //    DataTable table = new DataTable();
        //    con.Open();
        //    try
        //    {
        //        string sql = "select tenmonan,mamonan,menu.tenmenu,gia,monan.mota from monan,menu " +
        //        "where monan.mamenu=menu.mamenu and monan.mamenu=" + int.Parse(mamenu);
        //        SqlDataAdapter adap = new SqlDataAdapter(sql, con);
        //        adap.Fill(table);
        //    }
        //    catch (Exception) { }
        //    con.Close();
        //    return table;
        //}
        //HD
        //lấy dữ liệu hóa đơn
        public DataTable hienthiHD()
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select hoadon.mahoadon,ngaylap,tennhanvien,tenkhachhang,tenban,sum(soluong * Gia) as 'tien' " +
                         "from hoadon, KhachHang, NhanVien, ChiTietHoaDon, MonAn, BanAn " +
                         "where HoaDon.MaHoaDon = ChiTietHoaDon.MaHoaDon " +
                         "and ChiTietHoaDon.MaMonAn = MonAn.MaMonAn " +
                         "and hoadon.makhachhang = khachhang.makhachhang " +
                         "and hoadon.manhanvien = nhanvien.manhanvien " +
                         "and hoadon.mabanan = banan.mabanan " +
                         "group by HoaDon.MaHoaDon,ngaylap,tennhanvien,tenkhachhang,tenban";

            //string sql = "select * from hoadon";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //lay du lieu chi tiet hoa don
        public DataTable hienthichitietHD(string mahd)
        {
            con.Open();
            DataTable table = new DataTable();

            string sql = "select tenmenu,tenmonan,soluong from monan,chitiethoadon,menu " +
                "where monan.mamonan=chitiethoadon.mamonan " +
                "and menu.mamenu=monan.mamenu " +
                "and mahoadon=" + int.Parse(mahd);
            //SqlCommand cmd = new SqlCommand(sql, con);
            //cmd.Parameters.AddWithValue("mahd", mahd);
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //delete
        public void deleteHD(string strMatch)
        {
            con.Open();
            string sql = "delete from chitiethoadon where mahoadon=@strMatch;" +
                "delete from hoadon where mahoadon=@strMatch";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("strMatch", int.Parse(strMatch));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //Add
        public void addHD(HoaDon hd)
        {
            con.Open();
            try
            {
                string sql = "insert into hoadon values (@ngaylap,@manhanvien,@makhachhang,@mabanan)";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("ngaylap", hd.ngaylap);
                cmd.Parameters.AddWithValue("manhanvien", int.Parse(hd.manv));
                cmd.Parameters.AddWithValue("makhachhang", int.Parse(hd.makh));
                cmd.Parameters.AddWithValue("mabanan", int.Parse(hd.maban));
                cmd.ExecuteNonQuery();
            }
            catch (Exception) { }
            con.Close();

        }
        //update
        public void updateHD(HoaDon hd, string mamonan, int soluong)
        {
            con.Open();
            string sql1 = "update chitiethoadon set soluong=@soluong where mamonan=@mamonan and mahoadon= @mahd;";
            string sql2 = "update hoadon set ngaylap=@ngaylap,manhanvien=@manhanvien,makhachhang=@makhachhang where mahoadon= @mahd;";

            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("mahd", int.Parse(hd.mahd));

            cmd.Parameters.AddWithValue("mamonan", int.Parse(mamonan));
            cmd.Parameters.AddWithValue("soluong", soluong);
            cmd.ExecuteNonQuery();

            cmd = new SqlCommand(sql2, con);
            cmd.Parameters.AddWithValue("mahd", int.Parse(hd.mahd));
            cmd.Parameters.AddWithValue("ngaylap", hd.ngaylap);
            cmd.Parameters.AddWithValue("manhanvien", int.Parse(hd.manv));
            cmd.Parameters.AddWithValue("makhachhang", int.Parse(hd.makh));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        //chi tiet hd
        //Them, sua, xoa chi tiet HD
        public void themctHD(string mahd, string mamon, int soluong)
        {
            con.Open();
            string sql = "insert into chitiethoadon values (@mahd,@mamonan,@soluong);";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("mahd", int.Parse(mahd));
            cmd.Parameters.AddWithValue("mamonan", int.Parse(mamon));
            cmd.Parameters.AddWithValue("soluong", soluong);
            cmd.ExecuteNonQuery();
            con.Close();
        }


        //Châu


        public void addMenu(Menu mn)
        {
            con.Open();
            string sql = "insert into menu values (@tenmenu,@mota);";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("tenmenu", mn.tenmenu);
            cmd.Parameters.AddWithValue("mota", mn.mota);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable hienthiMN()
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select * from menu";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        public void updateMN(Menu mn)
        {
            con.Open();
            string sql1 = "update menu set tenmenu=@tenmenu, mota=@mota where mamenu= @mamenu;";


            SqlCommand cmd = new SqlCommand(sql1, con);
            cmd.Parameters.AddWithValue("mamenu", int.Parse(mn.mamenu));
            cmd.Parameters.AddWithValue("tenmenu", mn.tenmenu);
            cmd.Parameters.AddWithValue("mota", mn.mota);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void deleteMenu(string strMatch)
        {
            con.Open();
            string sql = "delete from Menu where mamenu=@strMatch;";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("strMatch", int.Parse(strMatch));
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public DataTable timMenu(string strMatch)
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select * from menu where tenmenu=N'" + strMatch + "'";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        public DataTable danhsachmenutheoma(string mamenu)
        {
            DataTable table = new DataTable();
            con.Open();
            try
            {
                string sql = "select * from menu where mamenu=" + int.Parse(mamenu);
                SqlDataAdapter adap = new SqlDataAdapter(sql, con);
                adap.Fill(table);
            }
            catch (Exception) { }
            con.Close();
            return table;
        }
        public DataTable danhsachmamenu()
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select mamenu from menu";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }

        public bool checkExists(List<BanAn> li, BanAn banan)
        {
            foreach (BanAn item in li)
            {
                if (item.Mabanan == banan.Mabanan)
                {
                    return true;
                }
            }
            return false;
        }
        //trong


        public void capNhatMonAn(MonAn monAn)
        {
            con.Open();
            try
            {
                String querry = "UPDATE MonAn SET tenMonAn = @tenMonAn ,moTa = @moTa , gia = @gia , maMenu = @maMenu, Anh=@anh  WHERE maMonAn = @maMonAn ";
                SqlCommand sqlCommand = new SqlCommand(querry, con);
                sqlCommand.Parameters.AddWithValue("maMonAn", int.Parse(monAn.maMonAn));
                sqlCommand.Parameters.AddWithValue("tenMonAn", monAn.tenMonAn);
                sqlCommand.Parameters.AddWithValue("gia", monAn.gia);
                sqlCommand.Parameters.AddWithValue("anh", monAn.anh);
                sqlCommand.Parameters.AddWithValue("moTa", monAn.moTa);
                sqlCommand.Parameters.AddWithValue("maMenu", int.Parse(monAn.maMenu));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception) { }
            con.Close();
        }
        public void capNhatMonAnKhongAnh(MonAn monAn)
        {
            con.Open();
            try
            {
                String querry = "UPDATE MonAn SET tenMonAn = @tenMonAn ,moTa = @moTa , gia = @gia , maMenu = @maMenu  WHERE maMonAn = @maMonAn ";
                SqlCommand sqlCommand = new SqlCommand(querry, con);
                sqlCommand.Parameters.AddWithValue("maMonAn", int.Parse(monAn.maMonAn));
                sqlCommand.Parameters.AddWithValue("tenMonAn", monAn.tenMonAn);
                sqlCommand.Parameters.AddWithValue("gia", monAn.gia);
                sqlCommand.Parameters.AddWithValue("moTa", monAn.moTa);
                sqlCommand.Parameters.AddWithValue("maMenu", int.Parse(monAn.maMenu));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception) { }
            con.Close();
        }
        public void addMonAn(MonAn monAn)
        {
            con.Open();
            try
            {
                string querry = "INSERT INTO MonAn VALUES (@tenMonAn,@gia, @moTa, @anh, @maMenu);";
                SqlCommand sqlCommand = new SqlCommand(querry, con);
                sqlCommand.Parameters.AddWithValue("tenMonAn", monAn.tenMonAn);
                sqlCommand.Parameters.AddWithValue("gia", monAn.gia);
                sqlCommand.Parameters.AddWithValue("moTa", monAn.moTa);
                sqlCommand.Parameters.AddWithValue("anh", monAn.anh);
                sqlCommand.Parameters.AddWithValue("maMenu", int.Parse(monAn.maMenu));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception) { }
            con.Close();
        }

        public void deleteMonAn(string maMonAn)
        {
            con.Open();
            try
            {
                string querry = "DELETE FROM MonAn where MaMonAn=@maMonAn;";
                SqlCommand sqlCommand = new SqlCommand(querry, con);
                sqlCommand.Parameters.AddWithValue("maMonAn", int.Parse(maMonAn));
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception) { }

            con.Close();
        }

        //đổ dữ liệu vào listBanTrong
        public List<BanAn> loadBanTrong(List<BanAn> listBanTrong)
        {
            List<BanAn> tmp = new List<BanAn>();
            //Làm mới danh sách trước khi nạp lại dữ liệu
            listBanTrong.Clear();
            DataRowCollection list = dsBanTheoTinhTrang("Trống").Rows;
            foreach (DataRow item in list)
            {
                BanAn ban = new BanAn();
                ban.Mabanan = item.ItemArray[0].ToString();
                ban.Tenbanan = item.ItemArray[1].ToString();
                ban.Soghe = int.Parse(item.ItemArray[2].ToString());
                ban.Tinhtrang = item.ItemArray[3].ToString();
                tmp.Add(ban);
                listBanTrong.Add(ban);
            }
            return tmp;
        }
        //Đăng nhập
        public Boolean dangNhap(string u, string p)
        {
            DataTable table = new DataTable();
            Boolean kq = false;
            string sql = "select * from nhanvien where tendangnhap='" + u + "'";
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(table);
            foreach (DataRow dt in table.Rows)
            {
                if (u.Equals(dt["tendangnhap"].ToString()) == true && p.Equals(dt["matkhau"].ToString()) == true)
                {
                    kq = true;
                }
            }
            con.Close();
            return kq;
        }
        //Quyền hạn
        public Boolean quyenHan(string u)
        {
            DataTable table = new DataTable();
            Boolean kq = false;
            string sql = "select * from nhanvien where tendangnhap='" + u + "'";
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(table);
            foreach (DataRow dt in table.Rows)
            {
                if (dt["quyen"].ToString().Equals("Quản lý") == true)
                {
                    kq = true;
                }
            }
            con.Close();
            return kq;
        }
        //Lấy giá trị max cua 1 cột
        public int getMaxOfTable(string tableName, string colName)
        {
            int max = 0;
            DataTable table = new DataTable();
            //Boolean kq = false;
            string sql = "select max(" + colName + ") from " + tableName;
            con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            adapter.Fill(table);
            max = int.Parse(table.Rows[0].ItemArray[0].ToString());
            con.Close();
            return max;
        }

        //ghi file tạm
        public void writeFile(List<BanAn> list)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(filename, FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, list);
            stream.Close();
        }
        public List<BanAn> readFile()
        {
            List<BanAn> li = new List<BanAn>();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                li = (List<BanAn>)formatter.Deserialize(stream);
                stream.Close();
            }
            catch (Exception) { }
            return li;
        }
        //Thống kê
        //danh sách món ăn và số lượng cho thống kê top món ăn bán chạy nhất
        public DataTable thongKeMonAn(int number)
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select top " + number + " tenmonan,SUM(soluong) as 'soluong' " +
                "from monan,ChiTietHoaDon " +
                "where MonAn.MaMonAn = ChiTietHoaDon.MaMonAn " +
                "group by TenMonAn";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //Thống kê hóa đơn theo khoarng ngày
        public DataTable thongKeKhoangNgay(string fromDate, string toDate)
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select mahoadon,ngaylap,tennhanvien,tenkhachhang " +
                "from hoadon,nhanvien,khachhang " +
                "where hoadon.manhanvien=nhanvien.manhanvien " +
                "and hoadon.makhachhang=khachhang.makhachhang " +
                "and ngaylap between '" + fromDate + "' " +
                "and '" + toDate + "' ";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //Thống kê hóa đơn theo ngày
        public DataTable thongKeNgay(string Ngay)
        {
            con.Open();
            DataTable table = new DataTable();
            string sql = "select mahoadon,ngaylap,tennhanvien,tenkhachhang " +
                "from hoadon,nhanvien,khachhang " +
                "where hoadon.manhanvien=nhanvien.manhanvien " +
                "and hoadon.makhachhang=khachhang.makhachhang " +
                "and ngaylap = '" + Ngay + "'";
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
        //Thống kê hóa đơn theo tha
        public DataTable thongKeThang(int thang, int nam)
        {
            con.Open();
            DataTable table = new DataTable();

            string sql = "select mahoadon,ngaylap,tennhanvien,tenkhachhang " +
               "from hoadon,nhanvien,khachhang " +
               "where hoadon.manhanvien=nhanvien.manhanvien " +
               "and hoadon.makhachhang=khachhang.makhachhang " +
               "and MONTH(ngaylap)= " + thang +
               "and YEAR(ngaylap)= " + nam;
            SqlDataAdapter adap = new SqlDataAdapter(sql, con);
            adap.Fill(table);
            con.Close();
            return table;
        }
    }
}
