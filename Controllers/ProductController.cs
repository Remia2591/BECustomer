using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using html.Models;

namespace html.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult DanhSachJson()
        {
            using (var db = new html.Models.db_ad03e2_giftshopdbEntities())
            {
                var sanPhams = db.SanPhams
                    .Where(p => p.DangBan)
                    .Select(p => new
                    {
                        ma = p.SanPhamID,
                        ten = p.TenSanPham,
                        hinh = p.HinhAnh,
                        gia = p.Gia,
                        giaGoc = p.GiaGoc,
                        loai = p.DanhMuc.MaLoai,
                        mota = p.MoTa,
                        tonKho = p.SoLuongTon,
                        thuongHieu = p.ThuongHieu,
                        comboGom = p.ComboGom
                    })
                    .ToList();

                return Json(sanPhams, JsonRequestBehavior.AllowGet);
            }
        }
        // GET: Product
        public ActionResult ChiTiet(int? ma)
        {
            if (!ma.HasValue)
            {
                return HttpNotFound();
            }

            using (var db = new db_ad03e2_giftshopdbEntities())
            {
                var sanPham = db.SanPhams
                    .Include("DanhMuc")
                    .FirstOrDefault(p => p.SanPhamID == ma.Value);

                if (sanPham == null)
                {
                    return HttpNotFound();
                }

                return View(sanPham);
            }
        }

        public ActionResult SanPham()
        {
            return View();
        }
    }
}