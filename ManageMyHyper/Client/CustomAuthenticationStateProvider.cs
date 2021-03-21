using Blazored.LocalStorage;
using ManageMyHyper.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace ManageMyHyper.Client
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _http;
        private readonly IWorkTaskService _workTaskService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, HttpClient http, IWorkTaskService workTaskService)
        {
            _localStorageService = localStorageService;
            _http = http;
            _workTaskService = workTaskService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            string authToken = await _localStorageService.GetItemAsStringAsync("authenticationToken");

            var identity = new ClaimsIdentity();
            _http.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(authToken))
            {
                try
                {
                    identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");
                    _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                    await _workTaskService.GetNumberOfRemainingWorkTasks();
                }
                catch (Exception)
                {
                    await _localStorageService.RemoveItemAsync("authenticationToken");
                    identity = new ClaimsIdentity();
                }
            }

            var user = new ClaimsPrincipal(identity);
            var state = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

            return claims;
        }
    }
}
