using AlamalCharity.Data;
using AlamalCharity.Data.Models;
using AlamalCharity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Cryptography.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AlamalCharity.Controllers
{
    [Authorize]
    public class SubsController : Controller
    {
        private readonly AppDBContext context;

        public SubsController(AppDBContext _context)
        {
            context = _context;
        }

        public IActionResult Index()
        {
            using (context)
            {
                ViewBag.Dept = "ألإدارة المالية";
                ViewBag.Page = "ألإشتراكات";

                SubscriptionsList model = new SubscriptionsList();
                model.SubsList = new List<SubModel>();
                var query = context.Subscriptions
                    .Join(context.Clients, s => s.CLNT_ID, c => c.ID, (s, c) => new { 
                        s.ID, c.CLIENT_NAME, s.YEAR, s.JAN, s.FEB, s.MAR, s.APR, s.MAY,
                        s.JUN, s.JUL, s.AUG, s.SEP, s.OCT, s.NOV, s.DEC
                    }).ToList();
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        SubModel sm = new SubModel();
                        sm.subID = item.ID;
                        sm.subClientName = item.CLIENT_NAME;
                        sm.subYear = item.YEAR;
                        sm.subJAN = item.JAN;
                        sm.subFEB = item.FEB;
                        sm.subMAR = item.MAR;
                        sm.subAPR = item.APR;
                        sm.subMAY = item.MAY;
                        sm.subJUN = item.JUN;
                        sm.subJUL = item.JUL;
                        sm.subAUG = item.AUG;
                        sm.subSEP = item.SEP;
                        sm.subOCT = item.OCT;
                        sm.subNOV = item.NOV;
                        sm.subDEC = item.DEC;
                        sm.subTotal = item.JAN + item.FEB + item.MAR + item.APR 
                            + item.MAY + item.JUN + item.JUL + item.AUG + item.SEP 
                            + item.OCT + item.NOV + item.DEC;

                        model.SubsList.Add(sm);
                    }
                    
                }
                return View(model);
            }
        }

        [HttpPost]
        public IActionResult Index(string txt)
        {
            using (context)
            {
                ViewBag.Dept = "ألإدارة المالية";
                ViewBag.Page = "ألإشتراكات";
                ViewBag.Search = txt;

                SubscriptionsList model = new SubscriptionsList();
                model.SubsList = new List<SubModel>();
                var search = context.Subscriptions
                    .Join(context.Clients, s => s.CLNT_ID, c => c.ID, (s, c) => new {
                        s.ID, c.CLIENT_NAME, s.YEAR, s.JAN,  s.FEB, s.MAR, s.APR, s.MAY, 
                        s.JUN, s.JUL, s.AUG, s.SEP, s.OCT, s.NOV, s.DEC
                    }).ToList();
                if (!String.IsNullOrEmpty(txt))
                {
                    search = search.Where(x => x.CLIENT_NAME.Contains(txt)).ToList();
                    foreach (var item in search)
                    {
                        SubModel sm = new SubModel();
                        sm.subID = item.ID;
                        sm.subClientName = item.CLIENT_NAME;
                        sm.subYear = item.YEAR;
                        sm.subJAN = item.JAN;
                        sm.subFEB = item.FEB;
                        sm.subMAR = item.MAR;
                        sm.subAPR = item.APR;
                        sm.subMAY = item.MAY;
                        sm.subJUN = item.JUN;
                        sm.subJUL = item.JUL;
                        sm.subAUG = item.AUG;
                        sm.subSEP = item.SEP;
                        sm.subOCT = item.OCT;
                        sm.subNOV = item.NOV;
                        sm.subDEC = item.DEC;
                        sm.subTotal = item.JAN + item.FEB + item.MAR + item.APR
                            + item.MAY + item.JUN + item.JUL + item.AUG + item.SEP
                            + item.OCT + item.NOV + item.DEC;

                        model.SubsList.Add(sm);
                    }
                }

                return View(model);
            }
        }

        public IActionResult Collect()
        {
            using (context)
            {
                ViewBag.Dept = "ألإدارة المالية";
                ViewBag.Page = "التحصيل";

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

                if (query != null)
                {
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

        [HttpPost]
        public IActionResult Collect(string txt)
        {
            using (context)
            {
                ViewBag.Dept = "ألإدارة المالية";
                ViewBag.Page = "التحصيل";
                ViewBag.Search = txt;

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

                if (query != null)
                {
                    query = query.Where(x => x.CLIENT_NAME.Contains(txt) || x.CLIENT_MOBILE.Contains(txt)).ToList();
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

        public IActionResult PaySubs(int ID, string Name)
        {
            using (context)
            {
                SubModel model = new SubModel();
                model.Years = new List<SelectListItem>();

                var newID = context.Subscriptions.OrderByDescending(c => c.ID).FirstOrDefault();
                if (newID == null)
                    model.subID = 1;
                else
                    model.subID = newID.ID + 1;

                var yitm = new SelectListItem();
                yitm.Value = "0";
                yitm.Text = "إختر السنة";

                model.Years.Add(yitm);

                var years = context.Years.ToList();
                if (years.Count > 0)
                {
                    foreach (var cnt in years)
                    {
                        var itm = new SelectListItem();
                        itm.Value = cnt.YEAR.ToString();
                        itm.Text = cnt.YEAR.ToString();

                        model.Years.Add(itm);
                    }
                }

                model.subClientID = ID;
                model.subClientName = Name;

                return View(model);
            }
        }
    }
}
