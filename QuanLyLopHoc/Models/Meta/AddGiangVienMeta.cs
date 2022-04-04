using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyLopHoc.Models.Meta
{
    public class AddGiangVienMeta
    {
        public int MaLopHoc { get; set; }
        public int MaGiangVien { get; set; }
    }

    public class AddHocVienMeta
    {
        public int MaLopHoc { get; set; }
        public int MaHocVien { get; set; }
    }
}