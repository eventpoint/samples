using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using EventPoint.Samples.Clients.Web.ViewModels;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Clients.Web.Controllers
{
    public class EvalController : Controller
    {
        // GET: Eval
        public async Task<ActionResult> Session(string id)
        {
            var client = Helper.ApiClient();
            var registrantKey = Helper.RegistrantKey();

            var session = await client.GetTopicAsync(id);
            
            var eval = await client.GetEvalForSessionAsync(registrantKey, id);
            var model = new EvalSessionViewModel
            {
                Session = session,
                Eval = eval
            };
            return View(model);
        }
    }
}