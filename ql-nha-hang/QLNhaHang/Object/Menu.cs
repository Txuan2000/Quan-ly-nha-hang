using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLNhaHang
{
    class Menu
    {
        public string mamenu { get; set; }
        public string tenmenu { get; set; }
        public string mota { get; set; }
        public Menu()
        {

        }
        public Menu(string mamenu, string tenmenu, string mota)
        {
            this.mamenu = mamenu;
            this.tenmenu = tenmenu;
            this.mota = mota;
        }

    }
}
