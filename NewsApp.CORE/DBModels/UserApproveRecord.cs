using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class UserApproveRecord
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string ApprovedUserId { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
