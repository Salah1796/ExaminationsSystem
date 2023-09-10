using ExaminationsSystem.Application.Features.Exams.Commands.Create;
using ExaminationsSystem.Application.Features.Exams.Commands.Delete;
using ExaminationsSystem.Application.Features.Exams.Commands.Update;
using ExaminationsSystem.Application.Features.Exams.Queries.GetById;
using ExaminationsSystem.Application.Features.Questions.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Answer;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Create;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Delete;
using ExaminationsSystem.Application.Features.StudentExams.Commands.End;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Grade;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Start;
using ExaminationsSystem.Application.Features.StudentExams.Commands.Update;
using ExaminationsSystem.Application.Features.StudentExams.Queries.GetById;
using ExaminationsSystem.Application.Models.ViewModels.Identity;
using ExaminationsSystem.Application.Validation.Exams;
using ExaminationsSystem.Application.Validation.Identity;
using ExaminationsSystem.Application.Validation.StudentExams;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using QuestioninationsSystem.Application.Features.Questions.Commands.Create;
using QuestioninationsSystem.Application.Features.Questions.Commands.Delete;
using QuestioninationsSystem.Application.Validation.Questions;
using System.Reflection;

namespace ExaminationsSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidator<CreateExamCommand>, CreateExamCommandValidator>();
            services.AddScoped<IValidator<UpdateExamCommand>, UpdateExamCommandValidator>();
            services.AddScoped<IValidator<DeleteExamCommand>, DeleteExamCommandValidator>();

            services.AddScoped<IValidator<CreateQuestionCommand>, CreateQuestionCommandValidator>();
            services.AddScoped<IValidator<UpdateQuestionCommand>, UpdateQuestionCommandValidator>();
            services.AddScoped<IValidator<DeleteQuestionCommand>, DeleteQuestionCommandValidator>();

            services.AddScoped<IValidator<CreateStudentExamCommand>, CreateStudentExamCommandValidator>();
            services.AddScoped<IValidator<EndStudentExamCommand>, EndStudentExamCommandValidator>();
            services.AddScoped<IValidator<StartStudentExamCommand>, StartStudentExamCommandValidator>();
            services.AddScoped<IValidator<GradeStudentExamCommand>, GradeStudentExamCommandValidator>();
            services.AddScoped<IValidator<AnswerStudentExamCommand>, AnswerStudentExamCommandValidator>();
            services.AddScoped<IValidator<DeleteStudentExamCommand>, DeleteStudentExamCommandValidator>();
            services.AddScoped<IValidator<GetStudentExamByIdQuery>, GetStudentExamByIdQueryValidator>();
            services.AddScoped<IValidator<UpdateStudentExamCommand>, UpdateStudentExamCommandValidator>();


            services.AddScoped<IValidator<GetExamByIdQuery>, GetExamByIdQueryValidator>();

            services.AddScoped<IValidator<AuthenticationRequest>, AuthenticationRequestValidator>();
            services.AddScoped<IValidator<RegistrationRequest>, RegistrationRequestValidator>();


            return services;
        }
    }
}
