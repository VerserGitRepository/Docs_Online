using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class ExplorerModel
    {
        public List<DirModel> DirModelList;
        public List<FileModel> FileModelList;

        public String FolderName;
        public String ParentFolderName;
        public String URL;

        public ExplorerModel(List<DirModel> dirModelList, List<FileModel> fileModelList)
        {
            DirModelList = dirModelList;
            FileModelList = fileModelList;
        }
    }
    public class DirModel
    {
        public string DirName { get; set; }
        public DateTime DirAccessed { get; set; }
    }

    public class FileModel
    {
        public string FileName { get; set; }
        public string FileSizeText { get; set; }
        public DateTime FileAccessed { get; set; }
        public string FileType { get; set; }
    }
}