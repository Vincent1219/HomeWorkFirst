using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HomeWorkfirst.Models;
using MvcPaging;

namespace HomeWorkfirst.Controllers
{
    public class 客戶聯絡人Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        /// <summary> 每頁筆數 </summary>
        int PageSize = 10;

        // GET: 客戶聯絡人
        public ActionResult Index(int? page)
        {
            var data = db.客戶聯絡人.Where(x => x.是否已刪除 == false).
                            Include(客 => 客.客戶資料).
                            OrderByDescending(x => x.Id);
            // 計算出目前要顯示第幾頁的資料 ( 因為 page 為 Nullable<int> 型別 )
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            // 透過 ToPagedList 這個 Extension Method 將原本的資料轉成 IPagedList<T>
            return View(data.ToPagedList(currentPageIndex, PageSize));
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.客戶聯絡人.Add(客戶聯絡人);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶聯絡人).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人.是否已刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary> 遠端Email驗證 </summary>
        /// <param name="客戶Id">客戶ID</param>
        /// <param name="Email">檢查的Email</param>
        /// <returns></returns>
        public JsonResult RemoteEmailValidate(int 客戶Id, string Email)
        {
            bool isValidate = false;
            //if ( Url.IsLocalUrl( Request.Url.AbsoluteUri )) {
                var EmailList = db.客戶聯絡人.AsQueryable().
                                     Where(x => x.客戶Id == 客戶Id).
                                     Select(x => x.Email);
                var emailList = db.客戶聯絡人.AsQueryable().Select(x => x.Email);
                isValidate = null == EmailList.Where(x => x.ToLower().Equals(Email.ToLower())).FirstOrDefault();
            //}
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
