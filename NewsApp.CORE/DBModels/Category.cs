using NewsApp.CORE.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class Category:EntityBaseModel
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
        public List<AppUserCategory> UserCategories { get; set; } = new List<AppUserCategory>();
    }
}
