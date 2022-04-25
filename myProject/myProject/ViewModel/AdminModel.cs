using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myProject.ViewModel
{
    public class AdminModel
    {
        public int adminId { get; set; }
        public int adminKatId { get; set; }
        public string adminAdi { get; set; }
        public string adminSoyadi { get; set; }
        public string adminSifre { get; set; }
    }
}