using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeWorkfirst.Controllers
{
    [HandleError(ExceptionType = typeof(DbEntityValidationException), View = "Error_DbEntityValidationException")]
    [HandleError(ExceptionType = typeof(DbUpdateException), View = "Error_DbUpdateException")]
    public class BaseController : Controller
    {
        // GET: Base
        public ActionResult Debug()
        {
            return View();
        }
    }
}