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
using System.Data.Entity.Validation;

namespace HomeWorkfirst.Controllers
{
    public class 客戶銀行資訊Controller : BaseController
    {
        // private 客戶資料Entities db = new 客戶資料Entities();

        客戶銀行資訊Repository repo = RepositoryHelper.Get客戶銀行資訊Repository();

        /// <summary> 每頁筆數 </summary>
        int PageSize = 10;

        // GET: 客戶銀行資訊
        public ActionResult Index(int? page)
        {
            var data = repo.Get客戶銀行資訊所有資料();
            // 計算出目前要顯示第幾頁的資料 ( 因為 page 為 Nullable<int> 型別 )
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            // 透過 ToPagedList 這個 Extension Method 將原本的資料轉成 IPagedList<T>
            return View(data.ToPagedList(currentPageIndex, PageSize));
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶銀行資訊 客戶銀行資訊 = repo.Get單筆資料ByID(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList( repo.Get取得客戶資料選單列表(), "Id", "客戶名稱" );
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶銀行資訊 客戶銀行資訊)
        {
            if ( ModelState.IsValid ) {
                repo.Add(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList( repo.Get取得客戶資料選單列表(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id );
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var 客戶銀行資訊 = repo.Get單筆資料ByID(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList( repo.Get取得客戶資料選單列表(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id );
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶銀行資訊 客戶銀行資訊)
        {
            if ( ModelState.IsValid ) {
                repo.Update(客戶銀行資訊);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList( repo.Get取得客戶資料選單列表(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id );
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            var 客戶銀行資訊 = repo.Get單筆資料ByID(id.Value);
            repo.Delete(客戶銀行資訊);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
