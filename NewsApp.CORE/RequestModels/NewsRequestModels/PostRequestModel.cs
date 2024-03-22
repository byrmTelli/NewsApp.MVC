using NewsApp.CORE.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.NewsRequestModels
{
    public class PostRequestModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public string CategoryId { get; set; }
        public string CreatorId { get; set; }
        public bool IsPrivateOnly { get; set; }
        public bool IsPublished { get; set; }
    }
}
