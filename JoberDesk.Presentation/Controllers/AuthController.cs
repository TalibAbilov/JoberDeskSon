using JoberDesk.Business.DTOs.User;
using JoberDesk.Business.Helpers.Exceptions.User;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
    public class AuthController : Controller
    {
        readonly IUserService _userService;

		public AuthController(IUserService userService)
		{
			_userService = userService;
		}

		public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _userService.Register(dto);
                return RedirectToAction("Index", "Home");
            }
            catch (UserRegisterException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(LoginDto dto,string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                var url = await _userService.Login(dto,returnUrl);
                if(url == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(returnUrl);
            }
            catch (UserLoginException ex)
            {
				ModelState.AddModelError("", ex.Message);
				return View(dto);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _userService.Logout();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> CreateRole()
        {
            await _userService.CreateRole();
            return RedirectToAction("Index", "Home");

		}
    }
}
