using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Models
{
    public class ProjectUsers
    {
        public Guid Project_id { get; set; }
        public Guid User_id { get; set; }
    }
}
