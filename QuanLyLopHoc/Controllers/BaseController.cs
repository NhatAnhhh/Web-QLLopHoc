using QuanLyLopHoc.Enums;
using QuanLyLopHoc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QuanLyLopHoc.Controllers
{
    public class BaseController : Controller
    {
        protected static QlLopHocEntities Db;
        public static int MaTaiKhoan;
        public static int adminType = (int)UserType.Admin;
        public static ShowMesseger messeger = new ShowMesseger();
        // GET: Base
        public BaseController()
        {
            Db = new QlLopHocEntities();
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            HttpCookie id = Request.Cookies.Get("Id");
            if(id == null)
            {
                MaTaiKhoan = 0;
            }
            else
            {
                MaTaiKhoan = int.Parse(id.Value);
            }
        }

    }
}