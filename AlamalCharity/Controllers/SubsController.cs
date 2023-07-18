using AlamalCharity.Data;
using AlamalCharity.Data.Models;
using AlamalCharity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Security.Cryptography.Xml;

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

        public IActionResult PaySubs()
        {
            using (context)
            {
                SubModel model = new SubModel();
                model.Clients = new List<SelectListItem>();
                model.Years = new List<SelectListItem>();

                var newID = context.Subscriptions.OrderByDescending(c => c.ID).FirstOrDefault();
                if (newID == null)
                    model.subID = 1;
                else
                    model.subID = newID.ID + 1;

                var query = context.Clients.ToList();
                if (query.Count > 0)
                {
                    foreach (var cnt in query)
                    {
                        var itm = new SelectListItem();
                        itm.Value = cnt.ID.ToString();
                        itm.Text = cnt.CLIENT_NAME;

                        model.Clients.Add(itm);
                    }
                }

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

                return View(model);
            }
        }
    }
}
