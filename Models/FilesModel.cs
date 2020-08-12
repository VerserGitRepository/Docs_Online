using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class FilesModel
    {
        public string FileName { get; set; }
        public DateTime FileDate { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
    }
}