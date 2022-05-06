using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrackerAPI.Models
{
    public class Subtask
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AssignedUser { get; set; }
        public string Status { get; set; }
        public int EstimatedTime { get; set; }
        public int RemainingTime { get; set; }
        public Guid ParentTask { get; set; }
    }
}
