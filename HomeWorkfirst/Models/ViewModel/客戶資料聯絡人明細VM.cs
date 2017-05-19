using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeWorkfirst.Models.ViewModel
{
    public class 客戶資料聯絡人明細VM
    {

        public 客戶資料 客戶資料 { get; set; }

        public IQueryable<客戶聯絡人> 客戶聯絡人 { get; set; }
    }
}