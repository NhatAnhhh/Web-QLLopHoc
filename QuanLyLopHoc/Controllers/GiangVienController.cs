using QuanLyLopHoc.Attributes;
using QuanLyLopHoc.Enums;
using QuanLyLopHoc.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLopHoc.Controllers
{
    [AdminAuthorize]
    public class GiangVienController : BaseController
    {
        // GET: GiaoVien
        public ActionResult Index()
        {
            var list = Db.GiangViens.ToList();
            return View(list);
        }

        public ActionResult Add()
        {
            return View(new GiangVien());
        }

        [HttpPost]
        public ActionResult Add(GiangVien model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Db.TaiKhoans.Any(x => x.TenDangNhap == model.Email))
                    {
                        TempData["mess"] = "Tài khoản đã tồn tại";
                        return View(model);
                    }
                    else
                    {
                        var taikhoan = new TaiKhoan()
                        {
                            TenDangNhap = model.Email,
                          //ma hoa mk gv
                            MatKhau = Mahoa.EncryptMD5(model.Email),
                            LoaiTaiKhoan = (int)UserType.GiangVien,
                            NgayTao = DateTime.Now
                        };

                        Db.TaiKhoans.Add(taikhoan);
                        Db.SaveChanges();

                        model.MaTaiKhoan = taikhoan.MaTaKhoan;

                        Db.GiangViens.Add(model);
                        Db.SaveChanges();

                     
                    }
                }
                catch
                {
                    TempData["mess"] = "Không thể thêm dữ liệu";
                }

                return RedirectToAction("index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var model = Db.GiangViens.FirstOrDefault(x => x.MaGiangVien == id);
                return View(model);
            }
            catch
            {
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public ActionResult Edit(GiangVien model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (Db.GiangViens.Any(x => x.Email == model.Email && x.MaGiangVien != model.MaGiangVien))
                    {
                        TempData["mess"] = "Giảng viên đã tồn tại";
                        return View(model);
                    }
                    else
                    {
                        var tk = Db.TaiKhoans.FirstOrDefault(x => x.TenDangNhap == model.Email);

                        tk.TenDangNhap = model.Email;
                        Db.TaiKhoans.Attach(tk);
                        Db.Entry(tk).State = EntityState.Modified;
                        Db.SaveChanges();

                        var obj = Db.GiangViens.FirstOrDefault(x => x.MaGiangVien == model.MaGiangVien);

                        obj.TenGiangVien = model.TenGiangVien;
                        obj.Email = model.Email;
                        obj.SoDienThoai = model.SoDienThoai;
                        obj.DiaChi = model.DiaChi;


                        Db.GiangViens.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();
                    }
                }
                catch
                {
                    TempData["mess"] = "Không thể sửa dữ liệu";
                }

                return RedirectToAction("index");
            }
            else
            {
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var model = Db.GiangViens.FirstOrDefault(x => x.MaGiangVien == id);
                Db.GiangViens.Attach(model);
                Db.Entry(model).State = EntityState.Deleted;
                Db.GiangViens.Remove(model);
                Db.SaveChanges();
            }
            catch
            {
                TempData["mess"] = "Không thể xóa dữ liệu do có ràng buộc dữ liệu";
            }
            return RedirectToAction("index");
        }
    }
}