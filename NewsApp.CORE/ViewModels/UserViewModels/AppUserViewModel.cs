using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.UserViewModels
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        [Display(Name="İsim")]
        public string? Name { get; set; }
        [Display(Name = "Soyisim")]
        public string? Surname { get; set; }

        public string? Phone { get; set; }
        [Display(Name="Resim")]
        public string? Image { get; set; }
        public string? Email { get; set; }
        [Display(Name = "Doğum Yeri")]
        public string? HomeLand { get; set; }
        [Display(Name="Üyelik Durumu")]
        public bool IsSubcriber { get; set; }
        [Display(Name ="Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }
        public string UserName { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
        public CategoryViewModel? UserCategory { get; set; }
        public bool IsDeleted { get; set; }
    }
}
