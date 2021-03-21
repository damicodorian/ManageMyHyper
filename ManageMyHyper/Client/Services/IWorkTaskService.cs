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
        public IList<WorkTask> MyWorkTasks { get; set; }
        int NumberOfRemainingWorkTasksTodo { get; set; }
        Task<ServiceResponse<string>> CreateNewWorkTask(WorkTask request);
        Task LoadWorkTaskPrioritiesAsync();
        Task GetWorkTaskAsync();
        Task GetMyWorkTaskAsync();
        Task BookWorkTask(int workTaskId);
        Task ValidWorkTask(int workTaskId);
        Task DeleteWorkTask(int workTaskId);
        Task GetNumberOfRemainingWorkTasks();

    }
}
