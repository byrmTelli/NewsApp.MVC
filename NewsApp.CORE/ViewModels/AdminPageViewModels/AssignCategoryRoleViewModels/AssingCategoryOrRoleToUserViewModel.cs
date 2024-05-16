using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.AdminPageViewModels.AssignCategoryRoleViewModels
{
    public class AssingCategoryOrRoleToUserViewModel
    {
        public string UserId { get; set; }
        public string? CategoryId { get; set; }
        public string? RoleId { get; set; }
    }
}
