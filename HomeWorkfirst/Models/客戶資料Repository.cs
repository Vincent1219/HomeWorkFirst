using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using HomeWorkfirst.Models.ViewModel;

namespace HomeWorkfirst.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        /// <summary>
        /// 回傳的ALL排除刪除資料
        /// </summary>
        /// <returns></returns>
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where( x=> !x.是否已刪除 );
        }

        public IQueryable<客戶資料> Get客戶資料列表所有資料(客戶資料VM search)
        {
            var data = this.All();
            // 搜尋名稱
            if ( !string.IsNullOrEmpty(search.keyword)) {
                data = data.Where(x => x.客戶名稱.Contains(search.keyword));
            }
            // 搜尋 客戶分類
            if ( !string.IsNullOrEmpty(search.客戶分類)) {
                data = data.Where(x => x.客戶分類.Contains(search.客戶分類));
            }
            return data.OrderByDescending(x => x.Id);
        }

        public 客戶資料 Get單筆資料ByID(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public List<SelectListItem> Get客戶分類下拉選單列表()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem() { Value = "", Text = "請選擇", Selected = true });
            items.Add(new SelectListItem() { Value = "資訊業", Text = "資訊業" });
            items.Add(new SelectListItem() { Value = "製造業", Text = "製造業" });
            items.Add(new SelectListItem() { Value = "銀行業", Text = "銀行業" });
            return items;
        }

        public void Update(客戶資料 entity)
        {
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary> 刪除 </summary>
        /// <param name="entity">刪除的資料</param>
        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}