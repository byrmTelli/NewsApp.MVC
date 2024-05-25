using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.DBModels
{
    public class PostApproveRecord
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
