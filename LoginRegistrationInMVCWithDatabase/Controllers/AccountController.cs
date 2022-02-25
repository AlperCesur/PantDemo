using LoginRegistrationInMVCWithDatabase.Models;
using LoginRegistrationInMVCWithDatabase.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace LoginRegistrationInMVCWithDatabase.Controllers
{
    public class AccountController : Controller
    {

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveRegisterDetails(Register registerDetails)
        {

            if (ModelState.IsValid)
            {
                using (var databaseContext = new LoginRegistrationInMVCEntities())
                {
                    tbl_Login reglog = new tbl_Login();


                    reglog.Username = registerDetails.Username;
                    reglog.Mail = registerDetails.Email;
                    reglog.Password = registerDetails.Password;

                    var isAvailable = IsTagAvailable(reglog.Username);
                    if (isAvailable == null)
                    {
                        databaseContext.tbl_Login.Add(reglog);
                        databaseContext.SaveChanges();
                        ViewBag.Message = "User Details Saved";

                        return View("Login");
                    }
                    else
                    {
                        ModelState.AddModelError("Failure", "This username already exist");
                        return View("Register", registerDetails);
                    }
                }
            }
            else
            {

                return View("Register", registerDetails);
            }
        }

        public ActionResult Buildings(Buildings build)
        {
            LoginRegistrationInMVCEntities dc = new LoginRegistrationInMVCEntities();
            var item = dc.tbl_Buildings.ToList();
            return View(item);
        }
        public ActionResult Yeni()
        {
            var model = new tbl_Buildings();
            return View("AddBuilding", model);
        }
        public ActionResult Kaydet(Buildings model)
        {
            using (LoginRegistrationInMVCEntities db = new LoginRegistrationInMVCEntities())
            {
                if (!ModelState.IsValid)
                {
                    return View("Buildings", model);
                }

                if (model.BuildingType == null)
                {
                    ViewBag.Hata = "Please Add Building Type";
                    return View("AddBuilding", model);
                }
                tbl_Buildings bd = new tbl_Buildings();

                bd.BuildingType = model.BuildingType;
                bd.BuildingCost = model.BuildingCost;
                bd.ConstructionTime = model.ConstructionTime;

                db.tbl_Buildings.Add(bd);
                TempData["addBuilding"] = "Add";
                db.SaveChanges();
                return View("Buildings");
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                var isValidUser = IsValidUser(model);

                if (isValidUser != null)
                    return View("Buildings", isValidUser);
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination !");
                    return View();
                }
            }
            else
            {
                //If model state is not valid, the model with error message is returned to the View.
                return View(model);
            }
        }

        public ActionResult Welcome()
        {
            return View();
        }

        public tbl_Login IsValidUser(LoginViewModel model)
        {
            using (var dataContext = new LoginRegistrationInMVCEntities())
            {

                tbl_Login user = dataContext.tbl_Login.Where(query => query.Username.Equals(model.Username) && query.Password.Equals(model.Password)).SingleOrDefault();
                //If user is present, then true is returned.
                if (user == null)
                {
                    ModelState.AddModelError("Failure", "This username already exists");
                    return null;
                }
                else
                    return user;
            }
        }
        public ActionResult IsTagAvailable(string User)
        {
            using (var dataContext = new LoginRegistrationInMVCEntities())
            {
                try
                {
                    var user = dataContext.tbl_Login.Single(m => m.Username == User);
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
                catch (Exception)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
            }
        }
    }
}