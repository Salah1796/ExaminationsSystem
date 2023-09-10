using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Models.ViewModels.Identity;
using System.Threading.Tasks;

namespace ExaminationsSystem.Application.Contracts.Identity
{
    public interface IStudentAuthenticationService
    {
        Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest registrationRequest);
        Task<BaseResponse<AuthenticationResponse>> SignIn(AuthenticationRequest loginViewModel);
    }
}
