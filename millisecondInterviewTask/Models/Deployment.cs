using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace millisecondInterviewTask.Models
{
    public class Deployment
    {
        public string name { get; set; }
        public string description { get; set; }
        public List<string> categories { get; set; }
    }

    public class DeploymentRoot
    {
        public List<Deployment> deployments { get; set; }
    }
}
