using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyLopHoc.Models.Meta
{
    public class ThongTinLopHoc
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public int Type { get; set; }
        public int NguoiTao { get; set; }

    }

    public enum LoaiThongBao
    {
        BaiTap,
        TaiLieu,
        ThongBao
    }
}
