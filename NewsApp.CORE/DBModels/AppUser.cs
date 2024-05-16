using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public string? HomeLand { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte[]? Image { get; set; }
        public bool IsSubscriber { get; set; } = false;
        public Guid? UserCategoryId { get; set; }
        public Category UserCategory { get; set; }
    }
}
