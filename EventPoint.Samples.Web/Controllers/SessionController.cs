using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using EventPoint.Samples.Web.ViewModels;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Web.Controllers
{
    public class SessionController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Catalog(string keyword = "", string a = "", string b = "", string c = "")
        {           
            var client = await Helper.GetApiClient();
            var sessions = await client.GetTopicsAsync();
            
            //filter sessions by category, if appropriate.  this is an AND filter.
            if(!String.IsNullOrEmpty(a))
            {
                sessions = sessions.Where(x => x.CategoryIds.Contains(a)).ToList();
            }
            if (!String.IsNullOrEmpty(b))
            {
                sessions = sessions.Where(x => x.CategoryIds.Contains(b)).ToList();
            }
            if (!String.IsNullOrEmpty(c))
            {
                sessions = sessions.Where(x => x.CategoryIds.Contains(c)).ToList();
            }
            

            var filterA = await GetCategoryFilter("Session Type", a);
            var filterB = await GetCategoryFilter("Track", b);
            var filterC = await GetCategoryFilter("Level", c);

            var model = new SessionCatalogViewModel
            {
                FilterA = filterA,
                FilterB = filterB,
                FilterC = filterC,
                Sessions = sessions.OrderBy(x => x.Code).Take(20).ToList()
            };

            return View(model);
        }

        private async Task<CategoryFilterViewModel> GetCategoryFilter(string name, string selected)
        {
            var client = await Helper.GetApiClient();
            var categories = await client.GetChildCategoriesAsync(name);
            categories.Add(new Category { Id = "", Name = "--" });
            return new CategoryFilterViewModel
            {
                Label = name,
                Items = categories.OrderBy(x => x.Name).ToList(),
                SelectedId = selected
            };
        }

        
    }
}