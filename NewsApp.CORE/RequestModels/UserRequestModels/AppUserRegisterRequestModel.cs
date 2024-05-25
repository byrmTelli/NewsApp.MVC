using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.UserRequestModels
{
    public class AppUserRegisterRequestModel
    {

        [Display(Name = "Kullanıcı Adı")]
        public string Username { get; set; }


        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }


        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Parola Onayı")]
        [Compare("Password", ErrorMessage = "Parolalarınız aynı olmalı.")]
        public string ConfirmPassword { get; set; }


        [Display(Name = "İsim")]
        public string Name { get; set; }


        [Display(Name = "Soyisim")]
        public string Surname { get; set; }


        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }


        [Phone]
        [Display(Name = "Telefon")]
        [RegularExpression(@"^\d{3} \d{3} \d{2} \d{2}$")]
        public string Phone { get; set; }

        [Display(Name="Doğum Yeri")]
        public string? HomeLand { get; set; }

        [Display(Name="Okudum, kabul ediyorum.")]
        [Required(ErrorMessage ="Gizlilik politikasını kabul etmelisiniz.")]
        public bool PrivacyPolicy { get; set; } =false;
    }

}
