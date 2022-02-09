using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MyTask
    {
        public string Title { get; set; }
        public string About { get; set; }
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Status { get; set; }
        public string User { get; set; }
    }
}
