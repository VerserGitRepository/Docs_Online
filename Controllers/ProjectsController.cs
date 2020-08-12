﻿using DocsOnline.Models;
using DocsOnline.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsOnline.Controllers
{
    public class ProjectsController : Controller
    {
        public static readonly string Filepath = ConfigurationManager.AppSettings["Filepath"];

        public ActionResult Index()
        {
            if (!UserRoles.UserEditCompleteBookings())
            {
                return RedirectToAction("Login", "Login");
            }
                var Projects = new ProjectDetailsModel();
                Projects.Projectlist = new SelectList(DropDownHelperService.ProjectList().Result, "ID", "Value");
                IEnumerable<string> dirList = Directory.EnumerateDirectories(Filepath);
                foreach (string dir in dirList)
                {
                    DirectoryInfo d = new DirectoryInfo(dir);

                    string[] files = Directory.GetFiles(Filepath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        files[i] = Path.GetFileName(files[i]);
                    }
                    ViewBag.Files = files;
                    FilesModel dirModel = new FilesModel
                    {
                        FileName = Path.GetFileName(dir),
                        FileDate = d.LastAccessTime,
                        //FileSize = d.
                    };

                    // Projects.Folders.Add(d.Name.ToString());
                    Projects.FolderName.Add(d.Name);
                    Projects.FolderDate.Add(d.LastWriteTime.Date);

                }
            
            return View(Projects);
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string baseData)
        {
            if (HttpContext.Request.Files.AllKeys.Any())
            {
                for (int i = 0; i <= HttpContext.Request.Files.Count; i++)
                {
                    var file = HttpContext.Request.Files["files" + i];
                    if (file != null)
                    {
                        var fileSavePath = Path.Combine(Server.MapPath("/Files"), file.FileName);
                        file.SaveAs(fileSavePath);
                    }
                }
            }
            return View();
        }

        public ActionResult Download()
        {
            return View();
        }

        public FileResult DownloadFile(string fileName)
        {
           

            var filepath = System.IO.Path.Combine(Filepath, fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }

        [HttpGet]
        public ActionResult GetListOfFiles(string FolderName)
        {
            var _filepath = System.IO.Path.Combine(Filepath, FolderName);
            DirectoryInfo d = new DirectoryInfo(_filepath);
            List<FilesModel> files = new List<FilesModel>();
            foreach (FileInfo info in d.GetFiles())
            {
                FilesModel fileModel = new FilesModel
                {
                    FileName = info.Name,
                    FileDate = info.LastWriteTime,
                    FileSize =  Convert.ToInt32(info.Length),
                    FileType = info.Extension
                //FileSize = d.
            };
                files.Add(fileModel);
        }

            //   return View();
            return new JsonResult { Data = files, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }
    }
}