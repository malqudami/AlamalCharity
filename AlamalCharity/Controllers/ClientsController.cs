using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlamalCharity.Models;
using AlamalCharity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using AlamalCharity.Data.Models;

namespace AlamalCharity.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDBContext context;

        public ClientsController(AppDBContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            using (context)
            {
                ViewBag.Dept = "إدارة الأعضاء";
                ViewBag.Page = "بيانات الأعضاء";

                ClientsList model = new ClientsList();
                var cLients = new List<CLientModel>();

                var query = context.Clients.Join(context.Families, c => c.CLIENT_FAMILY, f => f.ID, (c, f) => new
                {
                    ID = c.ID,
                    CLIENT_NAME = c.CLIENT_NAME,
                    CLIENT_FAMILY = f.FAMILY_NAME,
                    CLIENT_MOBILE = c.CLIENT_MOBILE,
                    ADD_DATE = c.ADD_DATE,
                    STATUS = c.STATUS

                }).ToList();

                if (query != null){
                    foreach (var item in query)
                    {   
                        CLientModel cLient = new CLientModel();
                        cLient.clntID = item.ID;
                        cLient.clntName = item.CLIENT_NAME;
                        cLient.clntFamilyValue = item.CLIENT_FAMILY;
                        cLient.clntMobile = item.CLIENT_MOBILE;
                        cLient.clntAddDate = item.ADD_DATE;
                        cLient.clntStatus = item.STATUS;

                        cLients.Add(cLient);
                    }

                    model.Clients = cLients;
                }

                return View(model);
            }
        }

        public IActionResult Families()
        {
            using (context)
            {
                ViewBag.Dept = "إدارة الأعضاء";
                ViewBag.Page = "ألعائلات";

                FamiliesList model = new FamiliesList();
                var fams = new List<FamilyModel>();

                var fquery = context.Families.ToList();
                if (fquery != null)
                {
                    foreach (var item in fquery)
                    {
                        FamilyModel f = new FamilyModel();
                        f.fmID = item.ID;
                        f.fmName = item.FAMILY_NAME;
                        f.fmAddDate = item.ADD_DATE;
                        f.fmStatus = item.STATUS;

                        fams.Add(f);
                    }

                    model.Families = fams;
                }

                return View(model);
            }
        }

        public IActionResult AddClient()
        {
            using (context)
            {
                CLientModel model = new CLientModel();
                List<SelectListItem> famItems = new List<SelectListItem>();

                var query = context.Clients.OrderByDescending(c => c.ID).FirstOrDefault();
                if (query == null)
                    model.clntID = 1;
                else
                    model.clntID = query.ID + 1;

                var famList = context.Families.ToList();
                if (famList != null)
                {
                    foreach (var fam in famList)
                    {
                        var itm = new SelectListItem();
                        itm.Value = fam.ID.ToString();
                        itm.Text = fam.FAMILY_NAME;

                        famItems.Add(itm);
                    }

                    model.Families = famItems;
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult AddClient(CLientModel cLient)
        {
            using (context)
            {
                if (cLient.clntName != "" && cLient.clntMobile != "")
                {
                    var clnt = new Data.Models.Clients()
                    {
                        CLIENT_NAME = cLient.clntName,
                        CLIENT_FAMILY = cLient.clntFamily,
                        CLIENT_MOBILE = cLient.clntMobile,
                        ADD_DATE = DateTime.Now,
                        STATUS = 1
                    };

                    context.Clients.Add(clnt);
                    context.SaveChanges();
                }

                return RedirectToAction("Index", "Clients");
            }
        }

        public IActionResult UpdateClient(int ID)
        {
            using (context)
            {
                CLientModel cLient = new CLientModel();
                List<SelectListItem> famItems = new List<SelectListItem>();

                var query = context.Clients.Where(c => c.ID == ID).FirstOrDefault();
                if (query != null)
                {
                    cLient.clntID = query.ID;
                    cLient.clntName = query.CLIENT_NAME;
                    cLient.clntFamily = Convert.ToInt32(query.CLIENT_FAMILY);
                    cLient.clntMobile = query.CLIENT_MOBILE;
                    cLient.clntStatus = query.STATUS;
                }

                var famList = context.Families.ToList();
                if (famList != null)
                {
                    foreach (var fam in famList)
                    {
                        var itm = new SelectListItem();
                        itm.Value = fam.ID.ToString();
                        itm.Text = fam.FAMILY_NAME;

                        famItems.Add(itm);
                    }

                    cLient.Families = famItems;
                }

                return RedirectToAction("Index", "Clients");
            }
        }

        [HttpPost]
        public IActionResult UpdateClient(CLientModel model)
        {
            using (context)
            {
                var cLient = (from c in context.Clients where c.ID == model.clntID select c).First();
                if (cLient != null)
                {
                    cLient.CLIENT_NAME = model.clntName;
                    cLient.CLIENT_FAMILY = model.clntFamily;
                    cLient.CLIENT_MOBILE = model.clntMobile;
                    cLient.UPDATE_DATE = DateTime.Now;
                    cLient.STATUS = 1;

                    context.SaveChanges();
                }
                return RedirectToAction("Index", "Clients");
            }
        }

        public IActionResult AddFamily() 
        { 
            using(context)
            {
                FamilyModel fam = new FamilyModel();
                var fm = context.Families.OrderByDescending(f => f.ID).First();
                if (fm != null)
                    fam.fmID = fm.ID + 1;
                else
                    fam.fmID = 1;

                return View(fam);
            }
        }
        [HttpPost]
        public IActionResult AddFamily(FamilyModel model) 
        {
            using (context) 
            {
                if (model.fmName != "")
                {
                    var fam = new Data.Models.Families()
                    {
                        FAMILY_NAME = model.fmName,
                        ADD_DATE = DateTime.Now,
                        STATUS = 1
                    };

                    context.Families.Add(fam);
                    context.SaveChanges();
                }
                return RedirectToAction("Families", "Clients");
            }
            
        }

        public IActionResult UpdateFamily(int ID)
        {
            using (context)
            {
                FamilyModel fam = new FamilyModel();
                var fm = context.Families.Where(c => c.ID == ID).First();
                if (fm != null)
                {
                    fam.fmID = fm.ID;
                    fam.fmName = fm.FAMILY_NAME;
                    fam.fmStatus = fm.STATUS;
                }

                return View(fam);
            }
        }

        [HttpPost]
        public IActionResult UpdateFamily(FamilyModel model) 
        {
            using (context)
            {
                var fm = (from f in context.Families where(f.ID == model.fmID) select f).First();
                if (fm != null)
                {
                    fm.FAMILY_NAME = model.fmName;
                    fm.UPDATE_DATE = DateTime.Now;
                    fm.STATUS = 1;

                    context.SaveChanges();
                }

                return RedirectToAction("Families", "Clients");
            }
        }
    }
}
