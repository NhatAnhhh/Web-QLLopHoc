using QuanLyLopHoc.Attributes;
using QuanLyLopHoc.Models;
using QuanLyLopHoc.Models.Meta;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyLopHoc.Controllers
{
 
        [AdminAuthorize]
        public class LopHocController : BaseController
        {
            // GET: LopHoc
            public ActionResult Index()
            {
                var list = Db.LopHocs.ToList();
                return View(list);
            }

            public ActionResult Add()
            {
                return View(new LopHoc() { NgayTao = DateTime.Now, NguoiTao = MaTaiKhoan });
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Add(LopHoc model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        model.NguoiTao = MaTaiKhoan;
                        model.NgayTao = DateTime.Now;
                        Db.LopHocs.Add(model);
                        Db.SaveChanges();

                        return RedirectToAction("detail", new { id = model.MaLopHoc });
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
                    var model = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == id);
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("index");
                }
            }

            [HttpPost]
            public ActionResult Edit(LopHoc model)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var obj = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == model.MaLopHoc);

                        obj.TenLopHoc = model.TenLopHoc;
                        obj.MoTa = model.MoTa;
                        obj.DiaChi = model.DiaChi;
                        obj.Phong = model.Phong;
                        obj.ChuDe = model.ChuDe;
                        obj.TrangThai = model.TrangThai;

                        Db.LopHocs.Attach(obj);
                        Db.Entry(obj).State = EntityState.Modified;
                        Db.SaveChanges();

                        return RedirectToAction("detail", new { id = model.MaLopHoc });
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
                    var model = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == id);
                    Db.LopHocs.Attach(model);
                    Db.Entry(model).State = EntityState.Deleted;
                    Db.LopHocs.Remove(model);
                    Db.SaveChanges();
                }
                catch
                {
                    TempData["mess"] = "Không thể xóa dữ liệu do có ràng buộc dữ liệu";
                }
                return RedirectToAction("index");
            }
            public ActionResult Detail(int id)
            {
                try
                {
                    var model = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == id);
                    return View(model);
                }
                catch
                {
                    return RedirectToAction("index");
                }
            }

            public ActionResult AddGiangVien(int id)
            {
                return View(new AddGiangVienMeta() { MaLopHoc = id });
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public JsonResult AddGiangVien(AddGiangVienMeta model)
            {
                try
                {
                    var obj = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == model.MaLopHoc);

                    if (obj.GiangViens.Count(x => x.MaGiangVien == model.MaGiangVien) > 0)
                    {
                        return Json(messeger.AddError("Lớp đã có giảng viên này rồi"), JsonRequestBehavior.AllowGet);
                    }

                    var gv = Db.GiangViens.FirstOrDefault(x => x.MaGiangVien == model.MaGiangVien);

                    obj.GiangViens.Add(gv);

                    Db.LopHocs.Attach(obj);
                    Db.Entry(obj).State = EntityState.Modified;
                    Db.SaveChanges();

                  

                    return Json(messeger.AddSuccess("Thêm thành công"), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(messeger.AddError("Thêm không thành công"), JsonRequestBehavior.AllowGet);
                }
            }

            [HttpPost]
            public ActionResult XoaGiangVien(AddGiangVienMeta model)
            {
                try
                {
                    var obj = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == model.MaLopHoc);

                    if (obj.GiangViens.Count(x => x.MaGiangVien == model.MaGiangVien) == 0)
                    {
                        return Json(messeger.AddError("Lớp chưa có giảng viên này"));
                    }

                    var gv = Db.GiangViens.FirstOrDefault(x => x.MaGiangVien == model.MaGiangVien);

                    obj.GiangViens.Remove(gv);

                    Db.LopHocs.Attach(obj);
                    Db.Entry(obj).State = EntityState.Modified;
                    Db.SaveChanges();

        

                    return Json(messeger.AddSuccess("Xóa thành công"));
                }
                catch
                {
                    return Json(messeger.AddError("Xóa không thành công"));
                }
            }

            public ActionResult AddHocVien(int id)
            {
                return View(new AddHocVienMeta() { MaLopHoc = id });
            }
            [HttpPost]
            [ValidateAntiForgeryToken]
            public JsonResult AddHocVien(AddHocVienMeta model)
            {
                try
                {
                    var obj = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == model.MaLopHoc);

                    if (obj.HocViens.Count(x => x.MaHocVien == model.MaHocVien) > 0)
                    {
                        return Json(messeger.AddError("Lớp đã có học viên này rồi"), JsonRequestBehavior.AllowGet);
                    }

                    var hv = Db.HocViens.FirstOrDefault(x => x.MaHocVien == model.MaHocVien);

                    obj.HocViens.Add(hv);

                    Db.LopHocs.Attach(obj);
                    Db.Entry(obj).State = EntityState.Modified;
                    Db.SaveChanges();

                

                    return Json(messeger.AddSuccess("Thêm thành công"), JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json(messeger.AddError("Thêm không thành công"), JsonRequestBehavior.AllowGet);
                }
            }

            [HttpPost]
            public ActionResult XoaHocVien(AddHocVienMeta model)
            {
                try
                {
                    var obj = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == model.MaLopHoc);

                    if (obj.HocViens.Count(x => x.MaHocVien == model.MaHocVien) == 0)
                    {
                        return Json(messeger.AddError("Lớp chưa có học viên này"));
                    }

                    var hv = Db.HocViens.FirstOrDefault(x => x.MaHocVien == model.MaHocVien);

                    obj.HocViens.Remove(hv);

                    Db.LopHocs.Attach(obj);
                    Db.Entry(obj).State = EntityState.Modified;
                    Db.SaveChanges();

                    return Json(messeger.AddSuccess("Xóa thành công"));
                }
                catch
                {
                    return Json(messeger.AddError("Xóa không thành công"));
                }
            }

        public ActionResult MoiNguoi(int id)
        {
            var model = Db.LopHocs.FirstOrDefault(x => x.MaLopHoc == id);
            return View("_MoiNguoi", model);
        }

    }
}