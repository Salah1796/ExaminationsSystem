using AutoMapper;
using ExaminationsSystem.Application.Common.Responses;
using ExaminationsSystem.Application.Contracts.Identity;
using ExaminationsSystem.Application.Contracts.Persistence;
using ExaminationsSystem.Application.Contracts.Persistence.IRepositories;
using ExaminationsSystem.Application.Models.ViewModels.Identity;
using ExaminationsSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExaminationsSystem.Identity
{
    public class StudentAuthenticationService : IStudentAuthenticationService
    {
        private readonly ITokenService _jwtService;
        private readonly IMapper _mapper;
        private readonly IStudentRepository _studentRepository;
        private readonly IUnitOfWorkAsync _unitOfWorkAsyn;
        public StudentAuthenticationService(ITokenService jwtService, IStudentRepository studentRepository, IMapper mapper, IUnitOfWorkAsync unitOfWorkAsyn)
        {
            _jwtService = jwtService;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _unitOfWorkAsyn = unitOfWorkAsyn;
        }


        public async Task<BaseResponse<AuthenticationResponse>> SignIn(AuthenticationRequest loginViewModel)
        {
            BaseResponse<AuthenticationResponse> loginResponseViewModel = new BaseResponse<AuthenticationResponse>();
            try
            {
                var existUser = await _studentRepository.Get(u => u.UserName == loginViewModel.UserName)
                .SingleOrDefaultAsync();
                if (existUser != null)
                {
                    if (BCrypt.Net.BCrypt.Verify(loginViewModel.Password, existUser.Password))
                    {
                        AuthenticationResponse response = _jwtService.CreateToken(existUser);
                        loginResponseViewModel.Data = response;
                    }
                    else
                    {
                        loginResponseViewModel.Success = false;
                        loginResponseViewModel.Message = "Invalid Username or passowrd ";
                    }
                }
                else
                {
                    loginResponseViewModel.Success = false;
                    loginResponseViewModel.Message = "Invalid Username or passowrd ";
                }
            }
            catch (Exception e)
            {

                loginResponseViewModel.Success = false;
                loginResponseViewModel.Message = "Invalid Username or passowrd ";
            }
            return loginResponseViewModel;
        }

        private string HashPassword(string password)
        {
            int workFactor = 15;
            string salt = BCrypt.Net.BCrypt.GenerateSalt(workFactor);
            string loginpasswordHashed = BCrypt.Net.BCrypt.HashPassword(password, salt);
            return loginpasswordHashed;
        }

        public async Task<BaseResponse<RegistrationResponse>> Register(RegistrationRequest registrationRequest)
        {
            BaseResponse<RegistrationResponse> registrationResponse = new();
            var isUserExist = await _studentRepository.Get()
                .FirstOrDefaultAsync(u => u.UserName == registrationRequest.UserName ||
                (!string.IsNullOrEmpty(u.Email) && u.Email == registrationRequest.Email));
            if (isUserExist != null)
            {
                registrationResponse.Success = false;
                if (registrationRequest.Email == registrationRequest.Email)
                    registrationResponse.Message = "Email already exists";
                if (registrationRequest.UserName == registrationRequest.UserName)
                    registrationResponse.Message += "UserName already exists";
            }
            else
            {
                var newStudent = _mapper.Map<Student>(registrationRequest);
                newStudent.Password = HashPassword(registrationRequest.Password);
                await _studentRepository.AddAsync(newStudent);
                await _unitOfWorkAsyn.CommitAsync();
                registrationResponse.Data = new RegistrationResponse() { Id = newStudent.Id };
            }
            return registrationResponse;
        }
    }
}