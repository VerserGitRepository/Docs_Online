using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DocsOnline.Models
{
    public class ProjectDetailsModel
    {
        public ProjectDetailsModel()
        {
            FolderName = new List<string>();
            FolderDate = new List<DateTime>();
            Files = new List<FileModel>();
        }
        public SelectList Projectlist { get; set; }     
        public int? ProjectID { get; set; }
        public string ProjectName { get; set; }
        public List<FolderModel> Folders { get; set; }
        public List<string> FolderName { get; set; }
        public List<FileModel> Files { get; set; }        
        public List<DateTime> FolderDate { get; set; }
    }
}