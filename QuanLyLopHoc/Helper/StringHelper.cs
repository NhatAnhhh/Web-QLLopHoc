using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyLopHoc.Helper
{
    public class StringHelper
    {
        public static string MaLopHocConvert(int ma)
        {
            return "DKC" + ma.ToString("000000000");
        }
    }
}