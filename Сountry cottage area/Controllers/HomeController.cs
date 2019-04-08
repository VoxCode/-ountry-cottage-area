using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Сountry_cottage_area.Models;


namespace Сountry_cottage_area.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "user")]
        public ActionResult Application()
        {                     
                var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
                var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

                string jsonMap = "null";
                foreach (AllMap a in db.AllMaps.Where(allMap => allMap.SaveNumber == 1 && allMap.AreaId == area.Id))
                    jsonMap = a.Cordinates;

                ViewBag.Cord = jsonMap;
                return View();           
        }

        [Authorize(Roles = "user")]
        public ActionResult MapSave(List<string> arrCord)
        {
            AllMap allMap = new AllMap();
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(arrCord);

            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            int q = 1;         
            var save = db.AllMaps.FirstOrDefault(p => p.SaveNumber == q && p.AreaId == area.Id);
            if(save == null)
            {
                allMap.SaveNumber = q;
                allMap.Cordinates = json;
                allMap.AreaId = area.Id;

                db.AllMaps.Add(allMap);
                db.SaveChanges();
            }
            else
            {
                save.Cordinates = json;                            

                db.Entry(save).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            
            return RedirectToAction("Application");
        }

        [Authorize(Roles = "user")]
        public ActionResult AgricultureSave(List<string> agriArr)
        {
            Agriculture agriculture = new Agriculture();

            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            agriculture.AreaId = area.Id;
            agriculture.Variable = agriArr[0];
            agriculture.Date = Convert.ToDateTime(agriArr[1]);
            agriculture.AgricultureIndex = Convert.ToInt32(agriArr[2]);
            agriculture.AgricultureNumber = Convert.ToInt32(agriArr[3]);

            db.Agricultures.Add(agriculture);
            db.SaveChanges();                   
            return RedirectToAction("Application");
        }

        public ActionResult Guide()
        {
            return View(db.AgricultureTypes.ToList());
        }
       
        public ActionResult GetGuide(string id)
        {
            int cid = Convert.ToInt32(id);
            var culture = db.AgricultureTypes.FirstOrDefault(p => p.Index == cid);     
            
            return Content(culture.Content);
        }

        [Authorize(Roles = "user")]
        public ActionResult GetAgriculture(List<string> arr)
        {
            int index, number;

            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            index = Convert.ToInt32(arr[0]);
            number = Convert.ToInt32(arr[1]);
            var culture = db.Agricultures.FirstOrDefault(p => p.AgricultureIndex == index && p.AgricultureNumber == number && p.AreaId == area.Id);
            

            string f = "Ваша культура была посажена ";
            string e = "\n Сорт: ";
            return Content(f + culture.Date + e + culture.Variable);
        }

        [Authorize(Roles = "user")]
        public ActionResult DelAgriculture(List<string> arr)
        {
            int index, number;

            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            index = Convert.ToInt32(arr[0]);
            number = Convert.ToInt32(arr[1]);
            var culture = db.Agricultures.FirstOrDefault(p => p.AgricultureIndex == index && p.AgricultureNumber == number && p.AreaId == area.Id);

            db.Agricultures.Remove(culture);
            db.SaveChanges();

            string f = "Ваша культура была удалена!";
            return Content(f);
        }

        [Authorize(Roles = "user")]
        public ActionResult AddNewStage(List<string> arr)
        {
            PlanningStage planningStage = new PlanningStage();

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(arr);
            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            planningStage.Date = DateTime.Now;
            planningStage.Content = json;
            planningStage.AreaId = area.Id;

            db.PlanningStages.Add(planningStage);
            db.SaveChanges();
            return RedirectToAction("Application");
        }

        [Authorize(Roles = "user")]
        public ActionResult GetStagesList ()
        {
            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);
            var stages = db.PlanningStages.Where(p => p.AreaId == area.Id).ToList();           
            string content = "";

            for (int i = stages.Count-1; i >= 0; i--)
            {
                content += "<div><a href=\"#\" id=\"" + stages[i].Id + "\">" + stages[i].Date + "</a></div>";
            }          
            return Content(content);
        }

        [Authorize(Roles = "user")]
        public ActionResult GetChoiceStage (string id)
        {
            int cid = Convert.ToInt32(id);
            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);
            var stage = db.PlanningStages.FirstOrDefault(p => p.Id == cid && p.AreaId == area.Id);
            string i = stage.Content;
            return Content(stage.Content);
        }

        [Authorize(Roles = "user")]
        public ActionResult FinishTheYear()
        {

            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

   
            var culture = db.Agricultures.Where(p => p.AreaId == area.Id);
            foreach (var i in culture){
                db.Agricultures.Remove(i);
                
            }
            db.SaveChanges();

            string f = "Наступил новый сезон!";
            return Content(f);
        }

        [Authorize(Roles = "user")]
        public ActionResult LastThreeYears()
        {
            var userIdentity = db.Users.FirstOrDefault(p => p.Email == User.Identity.Name);
            var area = db.Areas.FirstOrDefault(p => p.UserId == userIdentity.Id);

            var stages = db.PlanningStages.Where(p => p.AreaId == area.Id).ToList();
            int j = 0;
            string[] arr = new string[3];

            for (int i = stages.Count - 1; i >= 0; i--)
            {
                if (j < 3)
                {
                    arr[j] = stages[i].Content;
                    j++;
                }
                else break;
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(arr);

            return Content(json);
        }

        [Authorize(Roles = "user")]
        public ActionResult IncompatibleCheck(List<string> arr)
        {
            int select = Convert.ToInt32(arr[arr.Count - 1]);
            var b = db.AgricultureTypes.FirstOrDefault(p => p.Index == select);
            if (b != null)
            {
                for (int i = arr.Count - 2; i >= 0; i--)
                {
                    int q = Convert.ToInt32(arr[i]);
                    var rule = db.IncompatibleAgricultures.FirstOrDefault(p => p.SecondCulure.Index == q && p.FirstCulure.Index == select);
                    if (rule != null)
                    {
                        arr[i] = "inc";
                    }
                    else
                    {
                        arr[i] = "com";
                    }
                }
            }
            
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(arr);

            return Content(json);
        }
    }
}