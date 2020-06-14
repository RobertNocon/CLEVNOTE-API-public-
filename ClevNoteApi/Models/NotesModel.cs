using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fiszki.Repositories
{
    public class NotesModel
    {
        public int id { get; set; }
        public int userId { get; set; }
        public string type { get; set; }
        public string title { get; set; }
        public string articleUrl { get; set; }
        public string noteContent { get; set; }
        public string dateUser { get; set; }
        public string created { get; set; }
        public string lastEdit { get; set; }
        public int rating { get; set; }
        public int isActive { get; set; }
    }
}
