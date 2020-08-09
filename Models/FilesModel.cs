using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class FolderModel
    {
        public List<string> FolderName { get; set; }
        public List<DateTime> FolderDate { get; set; }
        public List<FilesModel> FileModel { get; set; }

    }
    public class FilesModel
    {
        public string Folders { get; set; }
        public DateTime DirAccessed { get; set; }
    }

}