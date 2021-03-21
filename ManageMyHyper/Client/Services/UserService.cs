using ManageMyHyper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ManageMyHyper.Client.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IList<UserDTO> Users { get; set; } = new List<UserDTO>();

        public async Task GetUsers()
        {
            Users = await _httpClient.GetFromJsonAsync<IList<UserDTO>>("api/User/GetUserList");
        }
    }
}
