using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsOnline.Models
{
    public class ProjectDetailsModel
    {
        public SelectList Projectlist { get; set; }
     
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
    }
}