using QuanLyLopHoc.Models;
using QuanLyLopHoc.Models.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLopHoc.Controllers
{
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HttpCookie cookie = Request.Cookies.Get("UserFullName");
            if (cookie != null)
            {
                return Redirect("/home");
            }
            return View(new UserLoginMeta());
        }

        [HttpPost]
        public ActionResult Index(UserLoginMeta model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                //lay mat khau tu ban phim Ma hoa thanh md5 va so voi password trong csdl
                var f_Password = Mahoa.EncryptMD5(model.Password);
                var user = Db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == model.UserName && x.MatKhau == f_Password);

                if (user != null)
                {
                    HttpCookie cookie = new HttpCookie("UserFullName", Server.UrlEncode(user.TenDangNhap));
                    cookie.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(cookie);

                    HttpCookie id = new HttpCookie("Id", user.MaTaKhoan.ToString());
                    id.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(id);

                    HttpCookie userType = new HttpCookie("UserType", user.LoaiTaiKhoan.ToString());
                    userType.Expires = DateTime.Now.AddDays(30);
                    Response.Cookies.Add(userType);

                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    return Redirect("/login");
                }
                else
                {
                    ModelState.AddModelError("MatKhau", "Sai tên đăng nhập hoặc mật khẩu");
                    return View(model);
                }
            }
            return View(model);
        }
    }
}