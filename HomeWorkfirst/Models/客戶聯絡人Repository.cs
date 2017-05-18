using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;

namespace HomeWorkfirst.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(x => !x.是否已刪除).Include(客 => 客.客戶資料);
        }

        public IQueryable<客戶聯絡人> Get客戶聯絡人所有資料(string 職稱)
        {
            var data = this.All();
            if (!string.IsNullOrEmpty(職稱)) {
                data = data.Where(x => x.職稱 == 職稱);
            }
            return data.OrderByDescending(x => x.Id);
        }

        public 客戶聯絡人 Get單筆資料ByID(int id)
        {
            return this.All().FirstOrDefault(x => x.Id == id);
        }

        public void Update(客戶聯絡人 entity)
        {
            this.UnitOfWork.Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary> 刪除 </summary>
        /// <param name="entity">刪除的資料</param>
        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
        }

        public IQueryable<客戶資料> Get客戶資料選單列表()
        {
            return base.UnitOfWork.Context.Set<客戶資料>().Where(x => !x.是否已刪除).AsQueryable();
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}