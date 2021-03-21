using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageMyHyper.Shared
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsDone { get; set; }
        public DateTime? DateHasBeenDone { get; set; }
        public WorkTaskPriority Priority { get; set; }
    }
}
