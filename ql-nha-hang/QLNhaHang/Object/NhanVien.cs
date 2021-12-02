﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang
{
    class NhanVien
    {
        public NhanVien() { }
        public String MaNV { get; set; }
        public String TenNV { get; set; }
        public String GioiTinh { get; set; }
        public int Tuoi { get; set; }
        public String DiaChi { get; set; }
        public String SoDienThoai { get; set; }
        public String user { get; set; }
        public String pass { get; set; }
        public String chucvu { get; set; }
        public NhanVien(String manv,String tennv,String gioitinh, int tuoi, String diachi,String dienthoai, String user, String pass, String chucvu)
        {
            this.MaNV = manv;
            this.TenNV = tennv;
            this.Tuoi = tuoi;
            this.DiaChi = diachi;
            this.SoDienThoai = dienthoai;
            this.GioiTinh = gioitinh;
            this.user = user;
            this.pass = pass;
            this.chucvu = chucvu;

        }
      
    }
}
