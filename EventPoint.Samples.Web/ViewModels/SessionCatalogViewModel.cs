﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Web.ViewModels
{
    public class SessionCatalogViewModel
    {
        public CategoryFilterViewModel SessionTypeFilter { get; set; }
        public CategoryFilterViewModel TrackFilter { get; set; }
        public CategoryFilterViewModel LevelFilter { get; set; }
        public CategoryFilterViewModel SpecialtyFilter { get; set; }
        public CategoryFilterViewModel AudienceFilter { get; set; }
        public string Keyword { get; set; }
        public List<SessionViewModel> Sessions { get; set; }
    }

    public class CategoryFilterViewModel
    {
        public string Label { get; set; }
        public List<Category> Items { get; set; }
        public string SelectedId { get; set; }
    }

    public class SessionViewModel
    {
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SessionType { get; set; }
        public string Track { get; set; }
        public string Level { get; set; }
        public string Specialty { get; set; }
        public string Audience { get; set; }
    }
}