using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.UserRequestModels
{
    public class AppUserUpdateRequestModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? HomeLand { get; set; }
        public string? Phone { get; set; }
        public IFormFile? Image { get; set; }

    }
}
