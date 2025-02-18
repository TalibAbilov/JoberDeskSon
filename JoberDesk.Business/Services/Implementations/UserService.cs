using AutoMapper.Execution;
using JoberDesk.Business.DTOs.User;
using JoberDesk.Business.Helpers.Email;
using JoberDesk.Business.Helpers.Enums;
using JoberDesk.Business.Helpers.Exceptions.User;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;
        readonly LinkGenerator _linkGenerator;
        readonly IMailService _mailService;
        public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, LinkGenerator linkGenerator, IMailService mailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _linkGenerator = linkGenerator;
            _mailService = mailService;
        }


        public async Task Register(HttpContext httpContext,RegisterDto dto)
        {
            var oldUser = await _userManager.FindByEmailAsync(dto.Email);
          
            if (oldUser != null && oldUser.EmailConfirmed == true)
            {
                throw new UserRegisterException("Bu e-poçt adresi qeydiyyatda var.");
            }


            var user = new AppUser
            {
                Email =dto.Email,
                UserName=dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new UserRegisterException("Qeydiyyat zamanı xəta baş verdi.");
            }

            await _userManager.AddToRoleAsync(user, dto.UserRole.ToString());

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            object obj = new
            {
                userId = user.Id,
                token = token
            };
            var link = _linkGenerator.GetUriByAction(httpContext, "SubmitRegistration", "Auth", obj);
            MailRequest request = new MailRequest()
            {
                ToEmail = dto.Email,
                Subject = "Hesabın təsdqilənməsi",
                Body = $"<a href='{link}'>Hesabınızı təsdiqləyin</a>"
            };
            await _mailService.SendEmailAsync(request);

        }
        
		public async Task CreateRole()
		{
            foreach(var item in Enum.GetValues(typeof(UserRoles)))
            {
                await _roleManager.CreateAsync(new IdentityRole()
                {
                    Name = item.ToString()
                });
            }
		}
		public async Task Login(LoginDto dto)
		{
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UserLoginException("E-poçt adresi və ya şifrə yalnışdır!");
            }
            if (!user.EmailConfirmed)
            {
                throw new UserLoginException("E-poçt adresisiniz təsdiqlənməyib, e-poçt adresinizə göndərdiyimiz link vasitəsilə təsdqiləyin!");
            }
            var result= await _signInManager.CheckPasswordSignInAsync(user, dto.Password,true);

            if (result.IsLockedOut)
            {
				throw new UserLoginException("Birazdan yenidən sınayın.");
			}
            if (!result.Succeeded)
            {
                throw new UserLoginException("E-poçt adresi və ya şifrə yalnışdır!");
			}
            await _signInManager.SignInAsync(user, dto.RememberMe);  
		}

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<AppUser> GetCurrentUser(ClaimsPrincipal user)
        {
            var currentUser = await _userManager.GetUserAsync(user);
            if (currentUser == null)
            {
                throw new Exception("İstifadəçi tapılmadı.");
            }
            return currentUser;
        }

		public Task Update()
		{
			throw new NotImplementedException();
		}

        public async Task<AppUser> GetById(string id)
        {
            var user=await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                throw new Exception("İstifadəçi tapılmadı.");
            }
            return user;
        }

        public async Task ForgetPassword(HttpContext httpContext,ForgetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new Exception("İstifadəçi tapılmadı");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            object obj = new
            {
                userId = user.Id,
                token = token
            };
            var link = _linkGenerator.GetUriByAction(httpContext,"ResetPassword","Auth",obj);
            MailRequest request = new MailRequest()
            {
                ToEmail = dto.Email,
                Subject="Şifrənin dəyişdirilməsi",
                Body=$"<a href='{link}'>Şifrəni dəyiş</a>"
            };
            await _mailService.SendEmailAsync(request);
        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var user=await _userManager.FindByIdAsync(dto.userId);
            if (user == null)
            {
                throw new Exception("İstifadəçi tapılmadı");
            }
            var result = await _userManager.ResetPasswordAsync(user, dto.token, dto.NewPassword);
            if (!result.Succeeded)
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    stringBuilder.Append(item.Description);   
                }
                throw new ResetPasswordException(stringBuilder.ToString());
            }
        }

        public async Task SumbitRegistration(SubmitRegistrationDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);

            if (user == null)
            {
                throw new Exception("Istifadeci tapilmadi!");
            }

            if (user.EmailConfirmed)
            {
                throw new SubmitRegistrationException("Hesabınız artıq təsdiqlənib.");
            }
            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
        }
    }
}
