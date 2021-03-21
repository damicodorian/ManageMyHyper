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

        public IList<WorkTask> WorkTasks { get; set; } = new List<WorkTask>();
        public IList<WorkTask> MyWorkTasks { get; set; } = new List<WorkTask>();

        public event Action OnChange;

        public async Task BookWorkTask(int workTaskId)
        {
            var result = await _httpClient.PutAsJsonAsync("api/WorkTask", workTaskId);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if(response.Success)
            {
                _toastService.ShowSuccess(response.Message, "Succès");
            }
            else
            {
                _toastService.ShowError(response.Message, "Erreur.");
            }
            await GetWorkTaskAsync();
        }

        public async Task<ServiceResponse<string>> CreateNewWorkTask(WorkTask request)
        {
            var result = await _httpClient.PostAsJsonAsync("api/WorkTask", request);
            var response =  await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if(response.Success)
            {
                WorkTasksTodo += 1;
                _toastService.ShowSuccess(response.Message, "Succès");
                WorkTasksTodoChanged();
            }
            else
            {
                _toastService.ShowError(response.Message, "Erreur.");
            }
            return response;
        }

        public async Task DeleteWorkTask(int workTaskId)
        {
            var result = await _httpClient.DeleteAsync($"api/WorkTask/{workTaskId}");
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if (response.Success)
            {
                _toastService.ShowSuccess(response.Message, "Succès");
                WorkTasksTodo -= 1;
                WorkTasksTodoChanged();
            }
            else
            {
                _toastService.ShowError(response.Message, "Erreur.");
            }
            await GetMyWorkTaskAsync();
        }

        public async Task GetMyWorkTaskAsync()
        {
            MyWorkTasks = await _httpClient.GetFromJsonAsync<IList<WorkTask>>("api/WorkTask/getmyworktasks");
        }

        public async Task GetWorkTaskAsync()
        {
            WorkTasks = await _httpClient.GetFromJsonAsync<IList<WorkTask>>("api/WorkTask");
        }

        public async Task LoadWorkTaskPrioritiesAsync()
        {
            if(WorkTaskPriorities.Count == 0)
            {
                WorkTaskPriorities = await _httpClient.GetFromJsonAsync<IList<WorkTaskPriority>>("api/WorkTaskPriority");
            }
        }

        public async Task ValidWorkTask(int workTaskId)
        {
            var result = await _httpClient.PutAsJsonAsync("api/WorkTask/validworktask", workTaskId);
            var response = await result.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            if(response.Success)
            {
                _toastService.ShowSuccess(response.Message, "Succès");
                WorkTasksTodo -= 1;
                WorkTasksTodoChanged();
            }
            else
            {
                _toastService.ShowError(response.Message, "Erreur.");
            }
            await GetMyWorkTaskAsync();
        }

        void WorkTasksTodoChanged() => OnChange.Invoke();
    }
}
