using DocsOnline.Models;
using DocsOnline.ServiceHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            foreach (string dir in dirList)
            {
                DirectoryInfo d = new DirectoryInfo(dir);

                //string[] files = Directory.GetFiles(Filepath);
                //for (int i = 0; i < files.Length; i++)
                //{
                //    files[i] = Path.GetFileName(files[i]);
                //}
                //ViewBag.Files = files;
                //FilesModel dirModel = new FilesModel
                //{
                //    FileName = Path.GetFileName(dir),
                //    FileDate = d.LastAccessTime,
                //    //FileSize = d.
                //};

                // Projects.Folders.Add(d.Name.ToString());
                Projects.FolderName.Add(d.Name);
                Projects.FolderDate.Add(d.LastWriteTime.Date);

            }

            //getfiles get = new getfiles();
            List<string> files = GetAllFiles(Filepath);

            var filesList= new List<FileModel>();
            foreach (string f in files)
            {
                var _files = new FileModel
                {
                    FileName = f
                   // FileDate = f.LastAccessTime,
                   //FileSize = d.
                };
                filesList.Add(_files);
            }
            Projects.Files= filesList;
            return View(Projects);
        }
        private void ConstructTree(DirectoryInfo[] dirList,int id,int pId,bool isParent,TreeViewModel parent,string FilePathRoot)
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
                TreeViewModel child = GetNode(subFolder, node,FilePathRoot);               
                node.children.Add(child);
            }
        }

        private TreeViewModel GetNode(DirectoryInfo folder, TreeViewModel parent,string FilepathRoot)
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
                TreeViewModel child = GetNode(subFolder, node,FilepathRoot);
               
                node.children.Add(child);
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

        public FileResult DownloadFile(string fileName)
        {
            var filepath = System.IO.Path.Combine(Filepath, fileName);
            return File(filepath, MimeMapping.GetMimeMapping(filepath), fileName);
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
                    FileSize =  Convert.ToInt32(info.Length),
                    FileType = info.Extension
                //FileSize = d.
            };
                files.Add(fileModel);
        }
            ConstructTree(null, 1, 0, true,null,_filepath);
            return new JsonResult { Data = node, JsonRequestBehavior = JsonRequestBehavior.AllowGet }; ;
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
    }
}