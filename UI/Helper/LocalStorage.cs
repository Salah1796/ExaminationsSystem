using Blazored.LocalStorage;
namespace UI.Helper
{
    public class AuthTokenLocalStore : IAuthTokenStore
    {
        private readonly Blazored.LocalStorage.ILocalStorageService  _localStorage;

        private readonly string TokenKeyName = "Token";
        public AuthTokenLocalStore(ILocalStorageService localStorage)
        {
            this._localStorage = localStorage;
        }
        public async Task<string> GetToken()
        {
            var token =  await _localStorage.GetItemAsync<string>(TokenKeyName);
            if (string.IsNullOrEmpty(token)) return null;
            return "bearer " + token;   
        }

        public async Task SetToken(string token)
        {
            await _localStorage.SetItemAsync(TokenKeyName, token);
        }

        public async Task ClearToken()
        {
            await _localStorage.RemoveItemAsync(TokenKeyName);
        }
    }
}
