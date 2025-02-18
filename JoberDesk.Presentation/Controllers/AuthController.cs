using JoberDesk.Business.DTOs.User;
using JoberDesk.Business.Helpers.Exceptions.User;
using JoberDesk.Business.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JoberDesk.Presentation.Controllers
{
    public class AuthController : Controller
    {
        readonly IUserService _userService;
        readonly IMailService _mailService;

        public AuthController(IUserService userService, IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
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
                await _userService.Register(HttpContext,dto);
                return RedirectToAction("Login", "Auth");
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
        public async Task<IActionResult>Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _userService.Login(dto);
                return RedirectToAction("Index", "Home"); 
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
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>ForgetPassword(ForgetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _userService.ForgetPassword(HttpContext, dto);
                return RedirectToAction("Login", "Auth");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult ResetPassword(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return BadRequest();
            }
            ResetPasswordDto dto = new ResetPasswordDto()
            {
                userId = userId,
                token = token
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            try
            {
                await _userService.ResetPassword(dto);
                return RedirectToAction("Login", "Auth");
            }
            catch (ResetPasswordException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult SubmitRegistration(string userId,string token)
        {
            if(userId == null || token == null)
            {
                return BadRequest();
            }
            SubmitRegistrationDto dto = new SubmitRegistrationDto()
            {
                UserId = userId,
                Token = token
            };
            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult>SubmitRegistration(SubmitRegistrationDto dto)
        {
            try
            {
                await _userService.SumbitRegistration(dto);
                return RedirectToAction("Login","Auth");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
