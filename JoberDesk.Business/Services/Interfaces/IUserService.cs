using JoberDesk.Business.DTOs.User;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task Register(RegisterDto dto);
        Task CreateRole();
        Task<string> Login(LoginDto dto,string? returnUrl);
        Task Logout();
    }
}
