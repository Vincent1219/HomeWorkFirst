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
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

namespace HomeWorkfirst.Controllers
{
    public class 客戶聯絡人Controller : BaseController
    {
        // private 客戶資料Entities db = new 客戶資料Entities();

        客戶聯絡人Repository repo = RepositoryHelper.Get客戶聯絡人Repository();

        /// <summary> 每頁筆數 </summary>
        int PageSize = 10;

        // GET: 客戶聯絡人
        public ActionResult Index(string 職稱, int? page)
        {
            var data = repo.Get客戶聯絡人所有資料( 職稱 );
            ViewBag.職稱 = new SelectList( repo.All(), "職稱", "職稱");
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
            客戶聯絡人 客戶聯絡人 = repo.Get單筆資料ByID(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList( repo.Get客戶資料選單列表(), "Id", "客戶名稱");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repo.Get客戶資料選單列表(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Get單筆資料ByID(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repo.Get客戶資料選單列表(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Update(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repo.Get客戶資料選單列表(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            var 客戶聯絡人 = repo.Get單筆資料ByID(id.Value);
            repo.Delete(客戶聯絡人);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        /// <summary> 遠端Email驗證 </summary>
        /// <param name="客戶Id">客戶ID</param>
        /// <param name="Email">檢查的Email</param>
        /// <returns></returns>
        //public JsonResult RemoteEmailValidate(int 客戶Id, string Email)
        //{
        //    bool isValidate = false;
        //    if ( Url.IsLocalUrl( Request.Url.AbsoluteUri )) {
        //        var EmailList = db.客戶聯絡人.AsQueryable().
        //                             Where(x => x.客戶Id == 客戶Id).
        //                             Select(x => x.Email);
        //        var emailList = db.客戶聯絡人.AsQueryable().Select(x => x.Email);
        //        isValidate = null == EmailList.Where(x => x.ToLower().Equals(Email.ToLower())).FirstOrDefault();
        //    }
        //    return Json(isValidate, JsonRequestBehavior.AllowGet);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
