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
        public IList<WorkTask> WorkTasks { get; set; }
        int WorkTasksTodo { get; set; }
        Task<ServiceResponse<string>> CreateNewWorkTask(WorkTask request);
        void WorkTaskDone(string taskName);
        Task LoadWorkTaskPrioritiesAsync();
        Task GetWorkTaskAsync();
        Task BookWorkTask(int workTaskId);

    }
}
