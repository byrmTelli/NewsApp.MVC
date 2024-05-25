using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.RequestModels.AdminRequestModels
{
    public class ApproveUserRequestModel
    {
        public string ApproverId { get; set; }
        public string UserId { get; set; }
    }
}
