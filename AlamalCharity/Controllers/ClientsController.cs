using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlamalCharity.Models;
using AlamalCharity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;

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
                ClientsList model = new ClientsList();
                var cLients = new List<CLientModel>();

                var query = context.Clients.ToList();
                if (query != null){
                    foreach (var item in query)
                    {   
                        CLientModel cLient = new CLientModel();
                        cLient.clntID = item.ID;
                        cLient.clntName = item.CLIENT_NAME;
                        cLient.clntFamily = item.CLIENT_FAMILY;
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
                ClientsList model = new ClientsList();
                var cLients = new List<CLientModel>();

                var query = context.Clients.ToList();
                if (query != null)
                {
                    foreach (var item in query)
                    {
                        CLientModel cLient = new CLientModel();
                        cLient.clntID = item.ID;
                        cLient.clntName = item.CLIENT_NAME;
                        cLient.clntFamily = item.CLIENT_FAMILY;
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
                        itm.Value = fam.FAMILY_NAME;
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
                if (cLient != null)
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
    }
}
