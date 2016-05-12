using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventPoint.Services.Api.Models;

namespace EventPoint.Samples.Web.ViewModels
{
    public class EvalSessionViewModel
    {
        public Topic Session { get; set; }
        public IntegratedSurveyModel Eval { get; set; }
    }
}