using Asp.Versioning;
using AutoMapper;
using Core.CustomerRequestServices;
using Core.EmailServices;
using Core.UserServices;
using Data.Models.CustomerRequestsModels;
using API.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Core.OrderServices;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CustomerRequestsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerRequestService _customerRequestService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly ILogger<CustomerRequestsController> _logger;
        private readonly IOrderService _orderService;

        public CustomerRequestsController(IMapper mapper, ICustomerRequestService customerRequestService, IEmailService emailService, IUserService userService, ILogger<CustomerRequestsController> logger, IOrderService orderService)
        {
            _mapper = mapper;
            _customerRequestService = customerRequestService;
            _emailService = emailService;
            _userService = userService;
            _logger = logger;
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("request-for-installation")]
        public async Task<IActionResult> RequestInstallation([FromBody] RequestForInstallationDto requestForInstallationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data"));
            } 
            try
            {
                var (orderExists, order) = await _orderService.OrderExistAsync(requestForInstallationDto.OrderId);
                if(!orderExists)
                {
                    return NotFound(ipNXApiResponse.Failure("Order Not found"));
                }
  
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                if (order.CreatedById != userId)
                {
                    return Unauthorized(ipNXApiResponse.Failure("You are not authorized to make this installation request"));
                }
                var installationRequest = _mapper.Map<RequestForInstallation>(requestForInstallationDto);
                installationRequest.CreatedById = userId;
                installationRequest.LastUpdatedById = userId;
                await _customerRequestService.SaveInstallationRequestAsync(installationRequest);
                await _orderService.MarkInstallationStatusAsTrueAsync(order);

                var userDetails = await _userService.UserExistsAsync(userId);

                var body = $"{userDetails.FirstName} {userDetails.LastName} made a plan purchase and has scheduled an installation. Please check your dashboard";
                await _emailService.SendEmailAsync("pfermac@ipnxnigeria.net","SALES PORTAL: Installation has been Scheduled", body);
                return Ok(ipNXApiResponse.Success("Congratulations, installation has been scheduled!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while customer attempted to request an installation");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the request"));
            }

        }







        [Authorize]
        [HttpPost("request-a-call")]
        public async Task<IActionResult> RequestForCallBack()
        {
            var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
            var userId = new Guid(userIdFromClaims);
            try
            {
                var callBackRequest = new RequestCallBack
                {
                    CreatedById = userId,
                    LastUpdatedById = userId
                };                        
                await _customerRequestService.SaveCallBackRequestAsync(callBackRequest);

                var userDetails = await _userService.UserExistsAsync(userId);

                var body = $"{userDetails.FirstName} {userDetails.LastName} requested a call. Please check your dashboard";
                var recipients = new List<string>
                {
                    "ist@ipnxnigeria.net","worktools@ipnxnigeria.net","pfermac@ipnxnigeria.net"
                };
                await _emailService.SendEmailAsync(recipients, "SALES PORTAL: Call Request", body);
                
                return Ok(ipNXApiResponse.Success("Your call back request was submitted successfully. We will reach out to you as quickly as possible!"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while customer attempted to request a call");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the request"));
            }

        }



        [Authorize]
        [HttpPost("request-wifi-for-order")]
        public async Task<IActionResult> RequestForWifi(string orderId, bool activate)
        {
            var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
            var userId = new Guid(userIdFromClaims);
            try
            {

                var (orderExists, order) = await _orderService.OrderExistAsync(orderId);
                if (!orderExists)
                {
                    return NotFound(ipNXApiResponse.Failure("Order Not found"));
                }
                if (order.CreatedById != userId)
                {
                    return Unauthorized(ipNXApiResponse.Failure("You are not authorized to make this wifi request"));
                }
                await _orderService.MarkWifiRequestAsTrueOrFalseAsync(order, activate);
                return Ok(ipNXApiResponse.Success("wifi status was changed successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to change wifi status");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while trying to change wifi status"));
            }

        }





    }
    

}
