using NewsApp.CORE.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class Permission:EntityBaseModel
    {
        public string Name { get; set; }
        public List<AppRolePermission> RolePermissions { get; set; } = new List<AppRolePermission>();
    }
}
