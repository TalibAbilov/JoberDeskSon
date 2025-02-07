using AutoMapper.Execution;
using JoberDesk.Business.DTOs.User;
using JoberDesk.Business.Helpers.Exceptions.User;
using JoberDesk.Business.Services.Interfaces;
using JoberDesk.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoberDesk.Business.Services.Implementations
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly SignInManager<AppUser> _signInManager;
		public UserService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_signInManager = signInManager;
		}


		public async Task Register(RegisterDto dto)
        {
            var isEmailExist = await _userManager.FindByEmailAsync(dto.Email);
          
            if (isEmailExist != null)
            {
                throw new UserRegisterException("Bu e-poçt adresi qeydiyyatda var.");
            }

            var user = new AppUser
            {
                UserType = dto.UserType,
                Email =dto.Email,
                UserName=dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new UserRegisterException("Qeydiyyat zamanı xəta baş verdi.");
            }

            await _userManager.AddToRoleAsync(user, "Member");

            //await _signInManager.SignInAsync(user, false);
        }
        
		public async Task CreateRole()
		{
            await _roleManager.CreateAsync(new IdentityRole()
            {
                Name = "Member"
            });
		}

		public async Task<string> Login(LoginDto dto,string? returnUrl)
		{
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new UserLoginException("E-poçt adresi və ya şifrə yalnışdır!");
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
            return returnUrl;
		}

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
