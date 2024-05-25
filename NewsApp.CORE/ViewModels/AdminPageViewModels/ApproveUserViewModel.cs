using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.AdminPageViewModels
{
    public class ApproveUserViewModel
    {
        public string ApproverName { get; set; }
        public string ApproverSurname { get; set; }
        public string UserMail { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
