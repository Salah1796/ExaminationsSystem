using ExaminationsSystem.Application.Contracts.Identity;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Identity
{
    public class CurrentStudentService : ICurrentStudentService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentStudentService(IHttpContextAccessor httpContextAccessor = null)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetCurrentStudentId()
        {
            var context = _httpContextAccessor.HttpContext;
            var identity = context.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                var studentIdClaim = identity.Claims?.FirstOrDefault(c => c.Type == "StudentId")?.Value;
                if (!string.IsNullOrEmpty(studentIdClaim))
                {
                    if (Guid.TryParse(studentIdClaim, out Guid studentId))
                        return studentId;
                }
            }
            return null;
        }
    }
}
