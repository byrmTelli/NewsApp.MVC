using NewsApp.CORE.ViewModels.CategoryViewModels;
using NewsApp.CORE.ViewModels.RoleViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.UserViewModels
{
    public class AppUserViewModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Phone { get; set; }
        public string? Image { get; set; }
        public string? Email { get; set; }
        public string? HomeLand { get; set; }
        public bool IsSubcriber { get; set; }
        public DateTime? BirthDate { get; set; }
        public string UserName { get; set; }
        public List<string>? Roles { get; set; } = new List<string>();
        public CategoryViewModel? UserCategory { get; set; }
    }
}
