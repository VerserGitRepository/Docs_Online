﻿using DocsOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsOnline.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        public ActionResult Index()
        {
           var Projects = new ProjectDetailsModel();
            return View(Projects);
        }
    }
}