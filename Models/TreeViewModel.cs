using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DocsOnline.Models
{
    public class TreeViewModel
    {
        public TreeViewModel(string theName)
        {
            name = theName;
        }
        public string name { get; set; }
        public bool open { get; set; }
        public bool isParent { get; set; }

        public List<TreeViewModel> children { get; set; }

    }
}