using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace QLNhaHang
{
    [Serializable]
    class BanAn
    {
        public string Mabanan { get; set; }
        public string Tenbanan { get; set; }
        public int Soghe { get; set; }
        public string Tinhtrang { get; set; }

        public List<ThucDon> td { get; set; }
        public int tien { get; set; }
        public int Khuyenmai { get; set; }

        public BanAn() { }
        public BanAn(string mabanan)
        {
            this.Mabanan = mabanan;
        }
        public BanAn(string mabanan, string tenbanan, int soghe, string tinhtrang)
        {
            this.Mabanan = mabanan;
            this.Tenbanan = tenbanan;
            this.Soghe = soghe;
            this.Tinhtrang = tinhtrang;
        }
    }
}
