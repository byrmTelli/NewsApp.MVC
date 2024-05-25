using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.ViewModels.AdminPageViewModels
{
    public class ApprovePostViewModel
    {
        public string ApproverName { get; set; }
        public string ApproverSurname { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string PostId { get; set; }
    }
}
