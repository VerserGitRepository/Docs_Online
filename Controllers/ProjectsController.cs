﻿using DocsOnline.Models;
using DocsOnline.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace DocsOnline.Controllers
{
    public class ProjectsController : Controller
    {
        public static readonly string Filepath = ConfigurationManager.AppSettings["Filepath"];
        public TreeViewModel node = null;
        public int counter = 1;

        public ActionResult Index()
        {
            if (!UserRoles.UserEditCompleteBookings())
            {
                return RedirectToAction("Login", "Login");
            }
            var Projects = new ProjectDetailsModel();
            Projects.Projectlist = new SelectList(DropDownHelperService.ProjectList().Result, "ID", "Value");
            IEnumerable<string> dirList = Directory.EnumerateDirectories(Filepath);
            Projects.Files = GetDirectoryFiles(dirList.First());

            foreach (string dir in dirList)
            {
                var d = new DirectoryInfo(dir);
                var _folders = new FoldersModel
                {
                    FolderName = d.Name,
                    FolderDirectory = dir,
                    FolderDate = d.LastWriteTime.Date
                };
                Projects.FoldersList.Add(_folders);
                Projects.FolderName.Add(d.Name);
                Projects.FolderDate.Add(d.LastWriteTime.Date);
            }
            return View(Projects);
        }
        private void ConstructTree(DirectoryInfo[] dirList, int id, int pId, bool isParent, TreeViewModel parent, string FilePathRoot)
        {
            if (dirList == null)
            {
                DirectoryInfo di = new DirectoryInfo(FilePathRoot);
                dirList = di.GetDirectories();
                id = 1;
                pId = 0;

            }
            string folderPath;
            DirectoryInfo folder = null;
            if (parent == null)
            {
                folderPath = FilePathRoot;
                folder = new DirectoryInfo(folderPath);
            }
            else
            {
                folderPath = parent.name + folder.Name + "/";
            }

            node = new TreeViewModel(folder.Name);
            node.children = new List<TreeViewModel>();
            foreach (var subFolder in folder.GetDirectories())
            {
                TreeViewModel child = GetNode(subFolder, node, FilePathRoot);
                node.children.Add(child);
                node.isParent = node.children.Count == 0;
            }
        }

        private TreeViewModel GetNode(DirectoryInfo folder, TreeViewModel parent, string FilepathRoot)
        {
            string folderPath;

            if (parent == null)
            {
                folderPath = FilepathRoot;
                folder = new DirectoryInfo(folderPath);
            }
            else
            {
                folderPath = parent.name + folder.Name + "/";
            }

            TreeViewModel node = new TreeViewModel(folder.Name);
            node.children = new List<TreeViewModel>();
            foreach (var subFolder in folder.GetDirectories())
            {
                TreeViewModel child = GetNode(subFolder, node, FilepathRoot);

                node.children.Add(child);
                node.isParent = node.children.Count == 0;
            }

            return node;
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

        public FileResult DownloadFile(string path, string fileName)
        {
            var filepath = System.IO.Path.Combine(path, fileName);
            byte[] filedata = System.IO.File.ReadAllBytes(filepath);
            string contentType = MimeMapping.GetMimeMapping(filepath);

            var cd = new System.Net.Mime.ContentDisposition
            {
                FileName = filepath,
                Inline = true,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(filedata, contentType, MimeMapping.GetMimeMapping(filepath));

            //return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
        }

        [HttpGet]
        public ActionResult GetListOfFiles(string FolderName)
        {
            var _filepath = System.IO.Path.Combine(Filepath, FolderName);
            //Filepath = _filepath;
            DirectoryInfo d = new DirectoryInfo(_filepath);
            List<FilesModel> files = new List<FilesModel>();
            foreach (FileInfo info in d.GetFiles())
            {
                FilesModel fileModel = new FilesModel
                {
                    FileName = info.Name,
                    FileDate = info.LastWriteTime,
                    FileSize = Convert.ToInt32(info.Length),
                    FileType = info.Extension
                    //FileSize = d.
                };
                files.Add(fileModel);
            }
            ConstructTree(null, 1, 0, true, null, _filepath);
            var json = new JavaScriptSerializer().Serialize(node);
            var result = new { Data = json, Files = files };
            return new JsonResult { Data = result, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
        }

        [HttpGet]
        public ActionResult GetFolderFiles(string FolderName)
        {
            // List<string> files = GetAllFiles(@"\"+FolderName);                    
          var files =  GetDirectoryFiles(@"\" + FolderName);
            return new JsonResult { Data = files, JsonRequestBehavior = JsonRequestBehavior.AllowGet };   
        }

        public List<string> GetAllFiles(string sDirt)
        {
            List<string> files = new List<string>();
            try
            {
                foreach (string file in Directory.GetFiles(sDirt))
                {
                    
                    files.Add(file);
                }

                foreach (string fl in Directory.GetDirectories(sDirt))
                {
                    files.AddRange(GetAllFiles(fl));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return files;
        }

        public List<FilesModel> GetDirectoryFiles(string dir)
        {
            var d = new DirectoryInfo(dir);
            var files = new List<FilesModel>();

            foreach (FileInfo info in d.GetFiles())
            {
                var fileModel = new FilesModel
                {
                    FileName = info.Name,
                    FileDate = info.LastWriteTime,
                    FileSize = Convert.ToInt32(info.Length),
                    FileType = info.Extension,
                    FileFullPath=info.DirectoryName                    
                };
                files.Add(fileModel);
            }
            return files;
        }
    }
}