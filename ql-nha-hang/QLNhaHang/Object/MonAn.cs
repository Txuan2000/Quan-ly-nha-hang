using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang
{
    class MonAn
    {
        public String maMonAn{ get; set; }
        public String tenMonAn{ get; set; }
        public int gia{ get; set; }
        public String moTa{ get; set; }
        public String maMenu{ get; set; }
        public String anh{ get; set; }

        public MonAn()
        {

        }

        public MonAn(String maMonAn , String tenMonAn , int gia , String moTa ,String anh, String maMenu)
        {
            this.maMonAn = maMonAn;
            this.tenMonAn = tenMonAn;
            this.gia = gia;
            this.moTa = moTa;
            this.anh = anh;
            this.maMenu = maMenu;


        }

        public override string ToString()
        {
            return "Ma Mon an" + maMonAn + "Ten Mon an :"+tenMonAn + "Gia :"+ gia + "Mo ta :"+ moTa +"Ma Menu :"+maMenu;
        }


    }
}
