using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data.Dao;
using App.Data.Model;

namespace App.Controllers
{
    public class GIENDOController : Controller
    {
        //
        // GET: /GIENDO/

        public ActionResult Index(FormCollection collection)
        {
            GiendoDao Giendo = new GiendoDao();
            string HN = !String.IsNullOrEmpty(Request.Params["hn"]) ? Request.Params["hn"].ToString() : "";
            //List<GiendoModel> GiendoList = Giendo.FindGiendo(HN);
            ViewBag.RN = HN;
            return View();
        }
        [HttpPost]
        public ActionResult FindGiendoJSON(String HN = "")
        {
            try
            {
                GiendoDao Giendo = new GiendoDao();
                 //string HN = !String.IsNullOrEmpty(Request.Params["hn"]) ? Request.Params["hn"].ToString() : "";
                List<GiendoModel> GiendoList = Giendo.FindGiendo(HN);
               
                
                return Json(new { success = true, Hn = HN, model = GiendoList });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, Hn = HN, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue
            };
        }

    }
    
}
