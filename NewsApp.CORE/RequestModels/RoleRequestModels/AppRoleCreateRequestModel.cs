using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.RoleRequestModels
{
    public class AppRoleCreateRequestModel
    {
        [Required(ErrorMessage ="Role için bir isim giriniz.")]
        [Display(Name="Role Adı: ")]
        public string Name { get; set; }
    }
}
