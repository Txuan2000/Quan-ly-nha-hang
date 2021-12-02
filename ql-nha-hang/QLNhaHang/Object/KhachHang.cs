using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang
{
    class KhachHang
    {
        public string makh { get; set; }
        public string tenkh { get; set; }
        public string diachi { get; set; }
        public string dienthoai { get; set; }
        public KhachHang()
        {

        }
        public KhachHang(string makh,string tenkh,string diachi,string dienthoai)
        {
            this.makh = makh;
            this.tenkh = tenkh;
            this.diachi = diachi;
            this.dienthoai = dienthoai;
        }
    }
}
