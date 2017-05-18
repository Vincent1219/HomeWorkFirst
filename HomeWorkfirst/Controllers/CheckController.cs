using HomeWorkfirst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkfirst.Controllers
{
    public class CheckController : BaseController
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        //public JsonResult RemoteEmailValidate(int 客戶Id, string Email)
        //{
        //    bool isValidate = false;

        //    if (Url.IsLocalUrl(Request.Url.AbsoluteUri))
        //    {
        //        var emailList = db.客戶聯絡人.AsQueryable().Select(x => x.Email);
        //        isValidate = null == db.客戶聯絡人.AsQueryable().
        //                             Where(x => x.客戶Id == 客戶Id).
        //                             Select(x => x.Email).
        //                             Where(x => x.ToLowerInvariant().Equals(Email.ToLowerInvariant())).FirstOrDefault();
        //    }

        //    return Json(isValidate, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult RemoteEmailValidate(string Email)
        {
            bool isValidate = false;

            //if (Url.IsLocalUrl(Request.Url.AbsoluteUri))
            //{
            var emailList = db.客戶聯絡人.AsQueryable().Select(x => x.Email);
            isValidate = null == db.客戶聯絡人.AsQueryable().
                                 Select(x => x.Email).
                                 Where(x => x.ToLowerInvariant().Equals(Email.ToLowerInvariant())).FirstOrDefault();
            //}
            return Json(isValidate, JsonRequestBehavior.AllowGet);
        }
    }
}