namespace ourTime_server.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }
        public string GetMyName()
        {
            var result = String.Empty;
            if(_HttpContextAccessor.HttpContext != null)
            {
                result = _HttpContextAccessor.HttpContext.User?.Identity?.Name;
          
            }
            return result;
        }
    }
}
