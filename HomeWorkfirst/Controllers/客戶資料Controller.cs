using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HomeWorkfirst.Models;
using MvcPaging;
using HomeWorkfirst.Models.ViewModel;
using System.Data.Entity.Validation;
using System.Data.Entity.Infrastructure;
using System;

namespace HomeWorkfirst.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    public class 客戶資料Controller : BaseController
    {
        // private 客戶資料Entities db = new 客戶資料Entities();

        客戶資料Repository repo = RepositoryHelper.Get客戶資料Repository();

        /// <summary> 每頁筆數 </summary>
        int PageSize = 10;

        public ActionResult Index(客戶資料VM search, int? page)
        {
            ViewBag.客戶分類 = repo.Get客戶分類下拉選單列表();
            var data = repo.Get客戶資料列表所有資料( search );

            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            return View(data.ToPagedList(currentPageIndex, PageSize));
        }

        /// <summary> 客戶資料檢視表 </summary>
        /// <returns></returns>
        public ActionResult CustomerDetails()
        {
            客戶資料Entities db = new 客戶資料Entities();
            var 客戶資料檢視表 = db.CustomerDetailsView;
            return View(客戶資料檢視表);
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料聯絡人明細VM 客戶明細 = repo.取得客戶聯絡人清單(id.Value);
            if (客戶明細 == null)
            {
                return HttpNotFound();
            }
            return View(客戶明細);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            ViewBag.客戶分類 = repo.Get客戶分類下拉選單列表();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if ( ModelState.IsValid ) {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View( 客戶資料 );
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.客戶分類 = repo.Get客戶分類下拉選單列表();
            var data = repo.Get單筆資料ByID(id.Value);
            if (data == null) {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶資料 客戶資料)
        {
            if ( ModelState.IsValid ) {
                repo.Update(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        public ActionResult Delete(int id)
        {
            客戶資料 客戶資料 = repo.Get單筆資料ByID(id);
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult BatchUpdate(客戶聯絡人[] items)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in items)
                {
                    var prod = repo.取得客戶聯絡人資料(item.Id);
                    prod.職稱 = item.職稱;
                    prod.電話 = item.電話;
                    prod.手機 = item.手機;
                }
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View("Index");
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
