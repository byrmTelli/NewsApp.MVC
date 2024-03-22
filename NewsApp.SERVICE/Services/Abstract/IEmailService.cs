using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.SERVICE.Services.Abstract
{
    public interface IEmailService
    {
        Task SendResetPasswordEmail(string resetEmailLink, string toEmail);
    }
}
