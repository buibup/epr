using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Data.Dao;
using App.Data.Model;

namespace App.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Loginontroller/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(String username,String password)
        {
            try
            {
                UserModel User = new UserModel();
                UserDAO UserDAO = new UserDAO();
                User = UserDAO.GetUser(username, password);
                //List<DocScanModel> DocScanList = docScanDAO.DocScanSQLServer(HN);
                return Json(new { success = true, user = username, model = User });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
