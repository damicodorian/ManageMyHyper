using Blazored.Toast.Services;
using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public class WorkTaskService : IWorkTaskService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _httpClient;

        public WorkTaskService(IToastService toastService, HttpClient httpClient)
        {
            _toastService = toastService;
            _httpClient = httpClient;
        }

        public int WorkTasksTodo { get; set; } = 15;

        public IList<WorkTaskPriority> WorkTaskPriorities { get; set; } = new List<WorkTaskPriority>();

        public IList<ManagerWorkTask> MyWorkTasks { get; set; } = new List<ManagerWorkTask>();

        public event Action OnChange;

        public void CreateNewWorkTask(string taskName, int workTaskPriorityId)
        {
            WorkTaskPriority workTaskPriority = WorkTaskPriorities.First(w => w.Id == workTaskPriorityId);
            MyWorkTasks.Add(new ManagerWorkTask { WorkTaskId = workTaskPriorityId });
            WorkTasksTodo += 1;
            _toastService.ShowSuccess("La tâche a été créée.", "Succès");
            WorkTasksTodoChanged();
        }

        public async Task LoadWorkTaskPrioritiesAsync()
        {
            if(WorkTaskPriorities.Count == 0)
            {
                WorkTaskPriorities = await _httpClient.GetFromJsonAsync<IList<WorkTaskPriority>>("api/WorkTaskPriority");
            }
        }

        public void WorkTaskDone(string taskName)
        {
            WorkTasksTodo -= 1;
            WorkTasksTodoChanged();
        }

        void WorkTasksTodoChanged() => OnChange.Invoke();
    }
}
