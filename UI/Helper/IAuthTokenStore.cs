namespace UI.Helper
{
    public interface IAuthTokenStore
    {
         Task<string> GetToken();
        Task SetToken(string token);
        Task ClearToken();
    }


}
