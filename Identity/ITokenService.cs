using ExaminationsSystem.Application.Models.ViewModels.Identity;
using ExaminationsSystem.Domain.Entities;

namespace ExaminationsSystem.Identity
{
    public interface ITokenService
    {
        AuthenticationResponse CreateToken(Student student);
        string GetClaimValue(string token, string claimName);
    }
}