using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Сountry_cottage_area.Controllers
{
    public class AdminsController : Controller
    {
        // GET: Admins
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {           
                return View();            
        }
    }
}