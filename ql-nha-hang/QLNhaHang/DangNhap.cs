using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang
{
    class DangNhap
    {
        private string username { get; set; }
        private string password { get; set; }
        private string quyen { get; set; }
        DangNhap() { }
        DangNhap(string tendangnhap,string matkhau,string quyen)
        {
            this.username = tendangnhap;
            this.password = matkhau;
            this.quyen = quyen;
        }
    }
}
