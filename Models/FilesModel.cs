using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class FilesModel
    {
        public string FileName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime FileDate { get; set; }
        public string FileType { get; set; }
        public int FileSize { get; set; }
    }
}