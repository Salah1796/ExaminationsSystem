using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Models.ViewModels;
using Refit;
namespace UI.Data.ServiceCredential
{
    public interface IServiceCredentialService
    {

        [Post("/ServiceCredential/Search")]
        Task<BasePaginatedResponse<List<ServiceCredentialLightViewModel>>> Search([Header("Authorization")] string authorization,[Body] ServiceCredentialSearchModel ServiceCredentialSearchModel);

        [Get("/ServiceCredential/Get/{Id}")]
        Task<BaseResponse<ServiceCredentialViewModel>> GetById([Header("Authorization")] string authorization,Guid Id);

        [Post("/ServiceCredential/Add")]
        Task<BaseResponse<ServiceCredentialViewModel>> Add([Header("Authorization")] string authorization,[Body] ServiceCredentialCreateViewModel ServiceCredentialCreateViewModel);

        [Put("/ServiceCredential/Update")]

        Task<BaseResponse<ServiceCredentialViewModel>> Update([Header("Authorization")] string authorization,[Body] ServiceCredentialEditViewModel ServiceCredentialCreateViewModel);

        [Delete("/ServiceCredential/Delete/{Id}")]
        Task Delete([Header("Authorization")] string authorization, Guid Id);

    }
}
