using NewsApp.CORE.DBModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.NewsRequestModels
{
    public class PostRequestModel
    {
        public string Id { get; set; }
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        [Display(Name = "İçerik")]
        public string Content { get; set; }
        [Display(Name = "Resim")]
        public string Image { get; set; }
        [Display(Name = "Kategori")]
        public string CategoryId { get; set; }
        [Display(Name = "Oluşturan Kişi")]
        public string CreatorId { get; set; }
        public bool IsPrivateOnly { get; set; }
        public bool IsPublished { get; set; }
    }
}
