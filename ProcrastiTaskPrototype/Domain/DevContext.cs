using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcrastiTaskPrototype.Domain
{
    public class DevContext
    {
        public string ConCurrentDevelopmentState { get; set; }
        public string ConNextStep { get; set; }
        public string ConSourceFile { get; set; }
        public string ConComponent { get; set; }
        public string ConGitBranch { get; set; }
        public string ConTechnicalNotes { get; set; }
    }
}
