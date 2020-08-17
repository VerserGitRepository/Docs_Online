using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class FoldersModel
    {
        public string FolderName { get; set; }
        public string FolderDirectory { get; set; }
        public DateTime? FolderDate { get; set; }
    }
}