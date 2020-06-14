using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Fiszki.Repositories
{
    public class UserLogs
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string LoginDate { get; set; }
        public string LoginStatus { get; set; }
    }
}
