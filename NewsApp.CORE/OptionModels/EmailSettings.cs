using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsApp.CORE.OptionModels
{
    public class EmailSettings
    {
        public string Host { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email  { get; set; } = null!;
    }
}
