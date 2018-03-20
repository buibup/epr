using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace App.Controllers
{
    public class DefaultController : Controller
    {

        public ActionResult Index(FormCollection collection)
        {

            return View();
        }

        
    }
}
