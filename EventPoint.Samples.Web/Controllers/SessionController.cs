using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EventPoint.Services.Api.Models;
using EventPoint.Samples.Clients.Web.ViewModels;
using Microsoft.Azure;

namespace EventPoint.Samples.Web.Controllers
{
    public class SessionController : Controller
    {
        // GET: Session
        public async Task<ActionResult> Catalog()
        {
            var client = await Helper.GetApiClient();
            var sessions = await client.GetTopicsAsync();
            
            var model = new SessionIndexViewModel
            {
                Sessions = sessions
            };

            return View(model);
        }

        
    }
}