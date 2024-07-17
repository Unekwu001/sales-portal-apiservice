using Asp.Versioning;
using AutoMapper;
using Core.AuthServices;
using Core.EmailServices;
using Core.OtpServices;
using API.Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
   

    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IMapper mapper, IAuthService authService, IOtpService otpService, IEmailService emailService, ILogger<AuthController> logger)
        {
            _mapper = mapper;
            _authService = authService;
            _otpService = otpService;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpPost("password-login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var user = await _authService.AuthenticateUserAsync(loginDto.Username, loginDto.Password);
                if (user == null)
                {
                    return BadRequest(ipNXApiResponse.Failure("Invalid Credentials"));
                }
                var token =  _authService.GenerateJwtToken(user);
                Response.Headers.Add("Authorization", "Bearer " + token);

                return Ok(ipNXApiResponse.Success("Login was Successful."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Login request");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the Login request"));
            }
        }






        [HttpPost("otp-login/{email}")]
        public async Task<IActionResult> OtpLogin(string email, [FromBody] OtpLoginDto otpLoginDto)
        {
            try
            {
                var otpExist = await _otpService.OtpExistAsync(email, otpLoginDto.Otp);
                if (otpExist == null)
                {
                    return BadRequest(ipNXApiResponse.Failure("Otp supplied does not exist. Please request for a new otp."));
                }
                var isValidToken = await _otpService.IsOtpValidAsync(otpLoginDto.Otp, email);
                if (!isValidToken)
                {
                    return BadRequest(ipNXApiResponse.Failure("Otp has expired. Please request for a new otp."));
                }
                var existingUser = await _authService.GetUserByEmailAsync(email);
                var jwtToken = _authService.GenerateJwtToken(existingUser);
                Response.Headers.Add("Authorization", "Bearer " + jwtToken);

                return Ok(ipNXApiResponse.Success("Login was Successful."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Login request");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the Login request"));
            }
        }






        //[Authorize]
        [HttpPost("send-reset-password-link")]
        public async Task<IActionResult> SendResetPasswordLink(string email)
        {
            try
            {
                //var userId = User.Claims.FirstOrDefault().Value;
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(ipNXApiResponse.Failure("email cannot be empty."));
                }
                var user = await _authService.UserExistsAsync(email);
                if (user == null)
                {
                    return BadRequest(ipNXApiResponse.Failure("Invalid email provided"));
                }
                var token = _otpService.GenerateOtp();
                await _otpService.SaveOtpAsync(email, token);
                var passwordResetUrl = $"https://app.com/reset-password/{token}/{email}";
                var emailBody = $"Click the link to reset your password: {passwordResetUrl}";
                await _emailService.SendEmailAsync(email, "Password Reset", emailBody);

                return Ok(ipNXApiResponse.Success($"Password reset link was successfully sent. Please check your email: ({email})"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while  trying to send password reset link");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while  trying to send password reset link"));
            }

        }






        [HttpPost("reset-password/{token}/{email}")]
        public async Task<IActionResult> ResetPassword(string token, string email, [FromBody] NewPasswordDto newPasswordDto)
        {
            try
            {
                var isValidToken = await _otpService.IsOtpValidAsync(token,email);
                if (!isValidToken)
                {
                    return BadRequest(ipNXApiResponse.Failure("Reset Link has expired. Please request a new password reset link."));
                }

                if (string.IsNullOrEmpty(newPasswordDto.NewPassword))
                {
                    return BadRequest(ipNXApiResponse.Failure("New password cannot be empty."));
                }

                await _authService.ResetPasswordAsync(email, newPasswordDto.NewPassword);

                return Ok(ipNXApiResponse.Success($"Password reset was successful"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the password reset request");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the password reset request"));
            }
        }





        [HttpPost("check-if-user-exists")]
        public async Task<IActionResult> UserExists(string userEmail)
        {
            try
            {
                var user = await _authService.UserExistsAsync(userEmail);
                if (user == null)
                {
                    return NotFound(ipNXApiResponse.Failure("user does not exist"));
                }
                var token = _otpService.GenerateOtp();
                await _otpService.SaveOtpAsync(userEmail, token);
                var emailBody = $"Please use this token to login. It expires in 10 minutes: {token}";
                await _emailService.SendEmailAsync(userEmail, "ipNX Otp verification", emailBody);
                return Ok(ipNXApiResponse.Success("user exists"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while checking the email.");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while checking the email."));
            }
        }



        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            try
            {
                Response.Headers.Remove("Authorization");

                return Ok(ipNXApiResponse.Success("Logout was Successful."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the Logout request");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the Logout request"));
            }
        }



    }
}
