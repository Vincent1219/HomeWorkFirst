using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using HomeWorkfirst.Models;
using MvcPaging;

namespace HomeWorkfirst.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        /// <summary> 每頁筆數 </summary>
        int PageSize = 10;

        // GET: 客戶資料
        //public ActionResult Index(int? page)
        //{
        //    var data = db.客戶資料.Where(x => x.是否已刪除 == false).OrderByDescending(x=>x.Id);
        //    // 計算出目前要顯示第幾頁的資料 ( 因為 page 為 Nullable<int> 型別 )
        //    int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
        //    // 透過 ToPagedList 這個 Extension Method 將原本的資料轉成 IPagedList<T>
        //    return View(data.ToPagedList(currentPageIndex, PageSize));
        //}

        public ActionResult Index(string keyword, int? page)
        {
            var data = db.客戶資料.Where(x => x.是否已刪除 == false).AsQueryable();
            if ( !string.IsNullOrEmpty( keyword )) {
                data = data.Where(x => x.客戶名稱.Contains(keyword));
            }
            data = data.OrderByDescending(x => x.Id);
            // 計算出目前要顯示第幾頁的資料 ( 因為 page 為 Nullable<int> 型別 )
            int currentPageIndex = page.HasValue ? page.Value - 1 : 0;
            // 透過 ToPagedList 這個 Extension Method 將原本的資料轉成 IPagedList<T>
            return View(data.ToPagedList(currentPageIndex, PageSize));
        }

        // GET: 客戶資料/Details/5
        public ActionResult CustomerDetails()
        {
            var 客戶資料檢視表 = db.CustomerDetailsView;
            return View(客戶資料檢視表);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 data = db.客戶資料.Find(id);
            if (data == null)
            {
                return HttpNotFound();
            }
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        public ActionResult Delete(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            客戶資料.是否已刪除 = true;
            db.SaveChanges();
            return RedirectToAction("Index");
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
