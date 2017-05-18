using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace HomeWorkfirst.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        /// <summary>
        /// 回傳的ALL排除刪除資料
        /// </summary>
        /// <returns></returns>
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(x => !x.是否已刪除).Include(x => x.客戶資料);
        }

        public IQueryable<客戶銀行資訊> Get客戶銀行資訊所有資料()
        {
            return this.All().OrderByDescending(x => x.Id);
        }

        public 客戶銀行資訊 Get單筆資料ByID(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public void Update(客戶銀行資訊 entity)
        {
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary> 刪除 </summary>
        /// <param name="entity">刪除的資料</param>
        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
        }

        public IQueryable<客戶資料> Get取得客戶資料選單列表()
        {
            return base.UnitOfWork.Context.Set<客戶資料>().Where( x => !x.是否已刪除 ).AsQueryable();
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}