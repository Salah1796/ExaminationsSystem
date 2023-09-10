using Blazored.LocalStorage;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace UI.Helper
{
    class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;
        public AuthHeaderHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var auth = request.Headers.Authorization;

            if (auth == null)
            {
                if (await _localStorageService.ContainKeyAsync("Token"))
                {
                    string token = await _localStorageService.GetItemAsync<string>("Token");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
