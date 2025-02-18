using JoberDesk.Business.DTOs.User;
using JoberDesk.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(HttpContext httpContext,RegisterDto dto);
        Task SumbitRegistration(SubmitRegistrationDto dto);
        Task CreateRole();
        Task Login(LoginDto dto);
        Task ForgetPassword(HttpContext httpContext,ForgetPasswordDto dto);
        Task ResetPassword(ResetPasswordDto dto);
        Task Logout();
        Task<AppUser> GetCurrentUser(ClaimsPrincipal user);
        Task<AppUser> GetById(string id);
        Task Update();

    }
}
