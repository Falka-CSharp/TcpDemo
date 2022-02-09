using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MyTaskRepos
    {
        public List<MyTask> MyTasks { get; set; }
        public MyTaskRepos()
        {
            MyTasks = new List<MyTask>();
        }
    }
}
