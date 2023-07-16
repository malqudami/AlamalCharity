using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlamalCharity.Models;
using AlamalCharity.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AlamalCharity.Controllers
{
    public class ClientsController : Controller
    {
        private readonly AppDBContext context;

        public ClientsController(AppDBContext _context)
        {
            context = _context;
        }
        public IActionResult AddClient()
        {
            CLientsModel model = new CLientsModel();
            List<SelectListItem> famItems = new List<SelectListItem>();

            var query = context.Clients.OrderByDescending(c => c.ID).FirstOrDefault();
            if (query == null)
                model.clntID = 1;
            else
                model.clntID= query.ID + 1;

            var famList = context.Families.ToList();
            if (famList!= null)
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

        [HttpPost]
        public IActionResult AddClient(CLientsModel cLient)
        {
            if (cLient != null)
            {
                var clnt = new Data.Models.Clients()
                {
                    CLIENT_NAME = cLient.clntName,
                    CLIENT_SURENAME = cLient.clntFamily,
                    CLIENT_MOBILE=cLient.clntMobile,
                    ADD_DATE = DateTime.Now,
                    STATUS = 1
                };

                context.Clients.Add(clnt);
                context.SaveChanges();
            }

            return RedirectToAction("Index", "Home");

        }
    }
}
