using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public class UserRoleService : IUserRoleService
    {
        private readonly HttpClient _httpClient;

        public UserRoleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public IList<UserRole> UserRoles { get; set; } = new List<UserRole>();

        public async Task LoadUserRolesAsync()
        {
            if(UserRoles.Count == 0)
            {
                UserRoles = await _httpClient.GetFromJsonAsync<IList<UserRole>>("api/UserRole");
            }
        }
    }
}
