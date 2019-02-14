using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pokemon.Models
{
    public class Held_items
    {
        public string item { get; set; }
        public Version_details MyProperty { get; set; }

        
    }

    public class Version
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}