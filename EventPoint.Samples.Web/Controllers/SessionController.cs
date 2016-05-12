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
        public async Task<ActionResult> Catalog(string keyword = "", string fSessionType = "", string fTrack = "", string fLevel = "", string fSpecialty = "", string fAudience = "")
        {           
            var client = await Helper.GetApiClient();
            var topics = await client.GetTopicsAsync();
            
            //filter sessions by category, if appropriate.  this is an AND filter.
            if (!String.IsNullOrEmpty(fSessionType))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(fSessionType)).ToList();
            }
            if (!String.IsNullOrEmpty(fTrack))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(fTrack)).ToList();
            }
            if (!String.IsNullOrEmpty(fLevel))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(fLevel)).ToList();
            }
            if (!String.IsNullOrEmpty(fSpecialty))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(fSpecialty)).ToList();
            }
            if (!String.IsNullOrEmpty(fAudience))
            {
                topics = topics.Where(x => x.CategoryIds.Contains(fAudience)).ToList();
            }

            if(!String.IsNullOrEmpty(keyword))
            {
                topics = topics.Where(x => x.Title.ToLower().Contains(keyword.ToLower()) || x.Description.ToLower().Contains(keyword.ToLower())).ToList();
            }

            //join topics against the Session Type category so you can display the value along with other session details
            var sessiontypes = await client.GetChildCategoriesAsync("session type");
            var tracks = await client.GetChildCategoriesAsync("track");
            var levels = await client.GetChildCategoriesAsync("level");
            var specialties = await client.GetChildCategoriesAsync("specialty/vertical");
            var audiences = await client.GetChildCategoriesAsync("target audience");

            var sessions = from topic in topics
                           select new SessionViewModel
                           {
                               Code = topic.Code,
                               Title = topic.Title,
                               Description = topic.Description,
                               SessionType = String.Join(", ", audiences.Where(x => topic.CategoryIds.Contains(x.Id)).Select(x => x.Name).ToList()),
                               Track = String.Join(", ", tracks.Where(x => topic.CategoryIds.Contains(x.Id)).Select(x => x.Name).ToList()),
                               Level = String.Join(", ", levels.Where(x => topic.CategoryIds.Contains(x.Id)).Select(x => x.Name).ToList()),
                               Specialty = String.Join(", ", specialties.Where(x => topic.CategoryIds.Contains(x.Id)).Select(x => x.Name).ToList()),
                               Audience = String.Join(", ", audiences.Where(x => topic.CategoryIds.Contains(x.Id)).Select(x => x.Name).ToList())
                           };
            

            var sessionTypeFilter = await GetCategoryFilter("Session Type", fSessionType);
            var trackFilter = await GetCategoryFilter("Track", fTrack);
            var levelFilter = await GetCategoryFilter("Level", fLevel);
            var specialtyFilter = await GetCategoryFilter("Specialty/Vertical", fLevel);
            var audienceFilter = await GetCategoryFilter("Target Audience", fLevel);

            var model = new SessionCatalogViewModel
            {
                SessionTypeFilter = sessionTypeFilter,
                TrackFilter = trackFilter,
                LevelFilter = levelFilter,
                SpecialtyFilter = specialtyFilter,
                AudienceFilter = audienceFilter,
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


        public ActionResult Mock()
        {
            return View();
        }

        
    }
}