using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuanLyLopHoc.Models.Meta
{
    public class UserMeta
    {
        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Xác thực mật khẩu không đúng!")]
        public string ConfirmPassword { get; set; }
    }
}