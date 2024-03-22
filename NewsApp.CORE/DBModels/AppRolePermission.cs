using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class AppRolePermission
    {
        public string AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
