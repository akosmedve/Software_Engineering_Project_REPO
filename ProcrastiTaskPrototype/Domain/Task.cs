using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcrastiTaskPrototype.Domain
{
    //getters and setters only!!
    public class Task
    {
        public string TaskTitle { get; set; }
        public string TaskDescription { get; set; }

        public TaskStatus TaskStatus { get; set; }
        public TaskPriority TaskPriority { get; set; }

        public DevContext DevContext { get; set; }
    }
}
