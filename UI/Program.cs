using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using UI.Data;
using Refit;
using UI.Data.ServiceCredential;
using BlazorStrap;
using ExaminationsSystem.Application;
using AutoMapper;
using System.Reflection;
using Blazored.LocalStorage;
using UI.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddTransient<IAuthTokenStore, AuthTokenLocalStore>();
builder.Services.AddTransient<AuthHeaderHandler>();
builder.Services.AddRefitClient<IServiceCredentialService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["APIURL"]);
    });
    //.AddHttpMessageHandler<AuthHeaderHandler>();

builder.Services.AddRefitClient<IAccountService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["APIURL"]);
    });


builder.Services.AddBlazorStrap();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
