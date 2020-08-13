using DocsOnline.Models;
using DocsOnline.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DocsOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!UserRoles.UserEditCompleteBookings())
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
            //string path = "";
            //var folderPath = ConfigurationManager.AppSettings["Filepath"];
            //// or folderPath = "FullPath of the folder on server" 

            //var realPath = folderPath + path;

            //if (System.IO.File.Exists(realPath))
            //{
            //    var fileBytes = System.IO.File.ReadAllBytes(realPath);

            //    //http://stackoverflow.com/questions/1176022/unknown-file-type-mime
            //    return File(fileBytes, "application/force-download");
            //}
            //else if (System.IO.Directory.Exists(realPath))
            //{
            //    List<DirModel> dirListModel = MapDirs(realPath);

            //    List<FileModel> fileListModel = MapFiles(realPath);

            //    ExplorerModel explorerModel = new ExplorerModel(dirListModel, fileListModel);

            //    //For using browser ability to correctly browsing the folders,
            //    //Every path needs to end with slash
            //    if (realPath.Last() != '/' && realPath.Last() != '\\')
            //    { explorerModel.URL = "/Home/" + path + "/"; }
            //    else
            //    { explorerModel.URL = "/Home/" + path; }

            //    var request = HttpContext.Request;

            //    UriBuilder uriBuilder = new UriBuilder
            //    { Path = request.Path.ToString() };

            //    //Gettin the current directory name using page URL.
            //    explorerModel.FolderName = WebUtility.UrlDecode(uriBuilder.Uri.Segments.Last());

            //    //Making a URL to going up one level. 
            //    Uri uri = new Uri(uriBuilder.Uri.AbsoluteUri.Remove
            //                        (uriBuilder.Uri.AbsoluteUri.Length - uriBuilder.Uri.Segments.Last().Length));

            //    explorerModel.ParentFolderName = uri.AbsolutePath;

            //    return View(explorerModel);
            //}
            //else
            //{
            //    return Content(path + " is not a valid file or directory.");
            //}
       

        //private List<DirModel> MapDirs(String realPath)
        //{
        //    List<DirModel> dirListModel = new List<DirModel>();

        //    IEnumerable<string> dirList = Directory.EnumerateDirectories(realPath);
        //    foreach (string dir in dirList)
        //    {
        //        DirectoryInfo d = new DirectoryInfo(dir);

        //        DirModel dirModel = new DirModel
        //        {
        //            DirName = Path.GetFileName(dir),
        //            DirAccessed = d.LastAccessTime
        //        };

        //        dirListModel.Add(dirModel);
        //    }

        //    return dirListModel;
        //}

        //private List<FileModel> MapFiles(String realPath)
        //{
        //    List<FileModel> fileListModel = new List<FileModel>();

        //    IEnumerable<string> fileList = Directory.EnumerateFiles(realPath);
        //    foreach (string file in fileList)
        //    {
        //        FileInfo f = new FileInfo(file);

        //        FileModel fileModel = new FileModel();

        //        if (f.Extension.ToLower() != "php" && f.Extension.ToLower() != "aspx"
        //            && f.Extension.ToLower() != "asp" && f.Extension.ToLower() != "exe")
        //        {
        //            fileModel.FileName = Path.GetFileName(file);
        //            fileModel.FileAccessed = f.LastAccessTime;
        //            fileModel.FileSizeText = (f.Length < 1024) ? f.Length.ToString() + " B" : f.Length / 1024 + " KB";

        //            fileListModel.Add(fileModel);
        //        }
        //    }

        //    return fileListModel;
        //}
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
