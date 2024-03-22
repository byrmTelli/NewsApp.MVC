using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class AppRole:IdentityRole
    {
        public List<AppRolePermission> RolePermissions { get; set; } = new List<AppRolePermission>();
    }
}
