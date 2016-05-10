using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Web.ViewModels
{
    public class SessionCatalogViewModel
    {
        public CategoryFilterViewModel FilterA { get; set; }
        public CategoryFilterViewModel FilterB { get; set; }
        public CategoryFilterViewModel FilterC { get; set; }
        public string Keyword { get; set; }
        public List<Topic> Sessions { get; set; }
    }

    public class CategoryFilterViewModel
    {
        public string Label { get; set; }
        public List<Category> Items { get; set; }
        public string SelectedId { get; set; }
    }
}