using NewsApp.CORE.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class Post:EntityBaseModel
    {
        public string Title  { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatorId { get; set; }
        public AppUser Creator { get; set; }
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public string Image { get; set; }
        public bool IsPrivateOnly { get; set; } = false;
        public bool IsPublished { get; set; } = false;
        public bool IsDeleted { get; set; } = false;

        public Post()
        {
            
        }
    }
}
