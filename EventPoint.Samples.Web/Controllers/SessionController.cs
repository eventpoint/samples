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
        public async Task<ActionResult> Catalog(string keyword = "", string sessiontype = "", string track = "", string level = "")
        {           
            var client = await Helper.GetApiClient();
            var topics = await client.GetTopicsAsync();
            
            //filter sessions by category, if appropriate.  this is an AND filter.
            if(!String.IsNullOrEmpty(sessiontype))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(sessiontype)).ToList();
            }
            if (!String.IsNullOrEmpty(track))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(track)).ToList();
            }
            if (!String.IsNullOrEmpty(level))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(level)).ToList();
            }

            if(!String.IsNullOrEmpty(keyword))
            {
                topics = topics.Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower())).ToList();
            }

            //join topics against the Session Type category so you can display the value along with other session details
            var sessiontypes = await client.GetChildCategoriesAsync("session type");
            var sessions = from t in topics
                           from st in sessiontypes
                           where t.CategoryIds.Contains(st.Id)
                           select new SessionViewModel
                           {
                               Code = t.Code,
                               Title = t.Title,
                               Description = t.Description,
                               SessionType = st.Name
                           };
            

            var sessionTypeFilter = await GetCategoryFilter("Session Type", sessiontype);
            var trackFilter = await GetCategoryFilter("Track", track);
            var levelFilter = await GetCategoryFilter("Level", level);

            var model = new SessionCatalogViewModel
            {
                SessionTypeFilter = sessionTypeFilter,
                TrackFilter = trackFilter,
                LevelFilter = levelFilter,
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