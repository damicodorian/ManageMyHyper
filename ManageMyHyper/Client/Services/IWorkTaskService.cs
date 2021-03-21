using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public interface IWorkTaskService
    {
        
        event Action OnChange;
        IList<WorkTaskPriority> WorkTaskPriorities { get; set; }
        IList<ManagerWorkTask> MyWorkTasks { get; set; }
        int WorkTasksTodo { get; set; }
        void CreateNewWorkTask(string taskName, int workTaskPriorityId);
        void WorkTaskDone(string taskName);
        Task LoadWorkTaskPrioritiesAsync();
    }
}
