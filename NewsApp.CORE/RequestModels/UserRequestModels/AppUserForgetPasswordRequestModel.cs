using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.UserRequestModels
{
    public class AppUserForgetPasswordRequestModel
    {
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [EmailAddress(ErrorMessage ="Belirtilen email formatı uygun değil.")]
        [Display(Name ="Email: ")]
        public string Email { get; set; }
    }
}
