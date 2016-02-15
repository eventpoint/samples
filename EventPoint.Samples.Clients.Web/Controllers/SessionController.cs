using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EventPoint.Services.Api.Models;
using EventPoint.Samples.Clients.Web.ViewModels;

namespace EventPoint.Samples.Clients.Web.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public async Task<ActionResult> Index()
        {
            var client = Helper.ApiClient();
            var sessions = await client.GetTopicsAsync();
            
            var model = new SessionIndexViewModel
            {
                Sessions = sessions
            };

            return View(model);
        }
    }
}