using Asp.Versioning;
using AutoMapper;
using Core.FileUploadServices;
using Core.OrderServices;
using Core.PlanServices;
using Core.UserServices;
using Data.Models.OrderModels;
using API.Data.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.EmailServices;
using Core.DiscountServices;

namespace API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IPlanService _planService;
        private readonly IUserService _userService;
        private readonly IFileUploadService _fileUploadService;
        private readonly ILogger<OrdersController> _logger;
        private readonly IEmailService _emailService;
        private readonly IDiscountService _discountService;

        public OrdersController(IMapper mapper, IOrderService orderService, IPlanService planService, IUserService userService, IFileUploadService fileUploadService, ILogger<OrdersController> logger, IEmailService emailService, IDiscountService discountService)
        {
            _mapper = mapper;
            _orderService = orderService;
            _planService = planService;
            _userService = userService;
            _fileUploadService = fileUploadService;
            _logger = logger;
            _emailService = emailService;
            _discountService = discountService;
        }

        [Authorize]
        [HttpPost("residential/place-an-order/fill-order-form")]
        public async Task<IActionResult> PlaceResidentialOrder([FromBody] ResidentialOrderDto residentialDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var discount = await _discountService.FetchStreetNameDiscountAsync(residentialDto.State, residentialDto.StreetName);

                var existingCustomer = await _userService.UserIdExistsAsync(userId);
                if (!existingCustomer)
                {
                    return NotFound(ipNXApiResponse.Failure("Customer data not found. Please sign up."));
                }
                var planTypeExist = await _planService.GetPlanTypeByIdAsync(residentialDto.PlanTypeId);
                if (planTypeExist == null)
                {
                    return NotFound(ipNXApiResponse.Failure("The selected PlanType does not exist."));
                }
                var youHaveAPendingResidentialOrderWithThisCoverageLocationName = await _orderService.NetworkCoverageAddressNameExistsForResidentialAsync(userId, residentialDto.NetworkCoverageAddress, residentialDto.PlanTypeId, residentialDto.Email);

                if (youHaveAPendingResidentialOrderWithThisCoverageLocationName)
                {
                    return BadRequest(ipNXApiResponse.Failure("You have an existing incomplete submission with similar information."));
                }
                var newResidential = _mapper.Map<ResidentialOrder>(residentialDto);

                if (residentialDto.IsBillingAddressSameAsResidentialAddress == true)
                {
                    newResidential.CreatedById = userId;
                    newResidential.LastUpdatedById = userId;
                    newResidential.Id = _orderService.GenerateOrderId();
                  
                    if (discount != null)
                    {
                        newResidential.Discount = discount.Percentage;
                    }
                    else
                    {
                        newResidential.Discount = 0;
                    }                   

                    await _orderService.AddResidentialOrderAndSaveChangesAsync(newResidential);

                    var updatedCustomerBillingDto = _mapper.Map<ResidentialOrderBillingDetailDto>(residentialDto);
                    var newResidentialBillingDetail = _mapper.Map<ResidentialOrderBillingDetail>(updatedCustomerBillingDto);

                    newResidentialBillingDetail.CreatedById = userId;
                    newResidentialBillingDetail.LastUpdatedById = userId;
                    newResidentialBillingDetail.ResidentialId = newResidential.Id;
                    await _orderService.AddResidentialOrderBillingDetailsAndSaveAsync(newResidentialBillingDetail);
   
                    return Ok(ipNXApiResponse.Success(newResidential.Id));
                }
                newResidential.Id = _orderService.GenerateOrderId();
                newResidential.CreatedById = userId;
                newResidential.LastUpdatedById = userId;
                newResidential.ResidentialBillingDetails.ResidentialId = newResidential.Id;

                if (discount != null)
                {
                    newResidential.Discount = discount.Percentage;
                }
                else
                {
                    newResidential.Discount = 0;
                }
                await _orderService.AddResidentialOrderAndSaveChangesAsync(newResidential);
                 

                return Ok(ipNXApiResponse.Success(newResidential.Id));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when trying to fill the residential order-form");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing this residential request"));
            }
        }


        [Authorize]
        [HttpPost("sme/place-an-order/fill-order-form")]
        public async Task<IActionResult> AddSme([FromBody] SmeOrderDto smeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var discount = await _discountService.FetchStreetNameDiscountAsync(smeDto.State, smeDto.CompanyStreetName);

                var existingCustomer = await _userService.UserIdExistsAsync(userId);
                if (!existingCustomer)
                {
                    return NotFound(ipNXApiResponse.Failure("Customer data not found. Please sign up."));
                }

                var planExist = await _planService.GetPlanTypeByIdAsync(smeDto.PlanTypeId);
                if (planExist == null)
                {
                    return NotFound(ipNXApiResponse.Failure("The selected PlanType does not exist."));
                }
                var youHaveAPendingSmeRegistrationWithThisCoverageLocationName = await _orderService.NetworkCoverageAddressNameExistsForSmeAsync(userId,
                    smeDto.networkCoverageAddress, smeDto.PlanTypeId, smeDto.CompanyName);

                if (youHaveAPendingSmeRegistrationWithThisCoverageLocationName)
                {
                    return BadRequest(ipNXApiResponse.Failure("You have an existing incomplete submission with similar information."));
                }
                var newSme = _mapper.Map<SmeOrder>(smeDto);

                if (smeDto.IsBillingAddressSameAsResidentialAddress == true)
                {
                    newSme.Id = _orderService.GenerateOrderId();
                    newSme.CreatedById = userId;
                    newSme.LastUpdatedById = userId;

                    if (discount != null)
                    {
                        newSme.Discount = discount.Percentage;
                    }
                    else
                    {
                        newSme.Discount = 0;
                    }
                    await _orderService.AddSmeOrderAndSaveChangesAsync(newSme);

                    var updatedCustomerBillingDto = _mapper.Map<SmeOrderBillingDetailDto>(smeDto);
                    var newSmeBillingDetail = _mapper.Map<SmeOrderBillingDetail>(updatedCustomerBillingDto);

                    newSmeBillingDetail.CreatedById = userId;
                    newSmeBillingDetail.LastUpdatedById = userId;
                    newSmeBillingDetail.SmeId = newSme.Id;
                    await _orderService.AddSmeOrderBillingDetailsAndSaveChangesAsync(newSmeBillingDetail);

                    return Ok(ipNXApiResponse.Success(newSme.Id));
                }
                newSme.Id = _orderService.GenerateOrderId();
                newSme.CreatedById = userId;
                newSme.LastUpdatedById = userId;
                newSme.SmeBillingDetails.SmeId = newSme.Id;

                if (discount != null)
                {
                    newSme.Discount = discount.Percentage;
                }
                else
                {
                    newSme.Discount = 0;
                }
                await _orderService.AddSmeOrderAndSaveChangesAsync(newSme);

                return Ok(ipNXApiResponse.Success(newSme.Id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when trying to fill the sme order-form");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing this sme request"));
            }
        }




        [Authorize]
        [HttpPost("residential/place-an-order/upload-documents")]
        public async Task<IActionResult> UploadResidentialDocuments(string orderId, [FromForm] ResidentialDocumentUploadDto uploadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);

                var existingOrder = await _orderService.GetResidentialOrderByIdAsync(orderId);
                
                if (existingOrder == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Order Not found."));
                }


                if (existingOrder.PassportPhotograph == null)
                {
                    if (uploadDto.PassportPhotograph != null)
                    {
                        var passportUrl = await _fileUploadService.UploadFileAsync(uploadDto.PassportPhotograph, "passport_photos");
                        existingOrder.PassportPhotograph = passportUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Passport photograph. Please upload it. "));
                    }
                }
                else if (uploadDto.PassportPhotograph != null)
                {
                    var passportUrl = await _fileUploadService.UploadFileAsync(uploadDto.PassportPhotograph, "passport_photos");
                    existingOrder.PassportPhotograph = passportUrl;
                }


                if (existingOrder.GovernmentId == null)
                {
                    if (uploadDto.GovernmentId != null)
                    {
                        var governmentIdUrl = await _fileUploadService.UploadFileAsync(uploadDto.GovernmentId, "government_ids");
                        existingOrder.GovernmentId = governmentIdUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your GovernmentId. Please upload it."));
                    }
                }
                else if (existingOrder.GovernmentId != null && uploadDto.GovernmentId != null)
                {
                    var governmentIdUrl = await _fileUploadService.UploadFileAsync(uploadDto.GovernmentId, "government_ids");
                    existingOrder.GovernmentId = governmentIdUrl;
                }



                if (existingOrder.UtilityBill == null)
                {
                    if (uploadDto.UtilityBill != null)
                    {
                        var utilityBillUrl = await _fileUploadService.UploadFileAsync(uploadDto.UtilityBill, "utility_bills");
                        existingOrder.UtilityBill = utilityBillUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Utility bill. Please upload it."));
                    }
                }
                else if (existingOrder.UtilityBill != null && uploadDto.UtilityBill != null)
                {
                    var utilityBillUrl = await _fileUploadService.UploadFileAsync(uploadDto.UtilityBill, "utility_bills");
                    existingOrder.UtilityBill = utilityBillUrl;
                }

                existingOrder.LastUpdatedById = userId;
                existingOrder.LastUpdatedOnUtc = DateTime.UtcNow;
                existingOrder.IsActive = true;

                await _orderService.SaveChangesAsync();

                return Ok(ipNXApiResponse.Success("Documents uploaded successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while trying to upload a document for a residential order");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while processing the upload request"));
            }
        }





        [Authorize]
        [HttpPost("sme/place-an-order/upload-documents")]
        public async Task<IActionResult> UploadSmeDocuments(string orderId, [FromForm] SmeDocumentUploadDto smeDocumentUploadDto)
        {
            const long maxFileSize = 5 * 1024 * 1024; // 5 MB in bytes

            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }

            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);

                var existingOrder = await _orderService.GetSmeOrderByIdAsync(orderId);
                if (existingOrder == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Customer not found. Please go to sign up."));
                }

                if (existingOrder.PassportPhotograph == null)
                {
                    if (smeDocumentUploadDto.PassportPhotograph != null)
                    {
                        var passportUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.PassportPhotograph, "passport_photos");
                        existingOrder.PassportPhotograph = passportUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Passport Photograph. Please upload it."));
                    }
                }
                else if (existingOrder.PassportPhotograph != null && smeDocumentUploadDto.PassportPhotograph != null)
                {
                    var passportUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.PassportPhotograph, "passport_photos");
                    existingOrder.PassportPhotograph = passportUrl;
                }


                if (existingOrder.GovernmentId == null)
                {
                    if (smeDocumentUploadDto.GovernmentId != null)
                    {
                        var governmentIdUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.GovernmentId, "government_ids");
                        existingOrder.GovernmentId = governmentIdUrl;
                    }
                    else 
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your GovernmentId. Please upload it."));
                    }
                }
                else if(existingOrder.GovernmentId != null && smeDocumentUploadDto.GovernmentId != null)
                {
                    var governmentIdUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.GovernmentId, "government_ids");
                    existingOrder.GovernmentId = governmentIdUrl;
                }


                if (existingOrder.UtilityBill == null)
                {
                    if (smeDocumentUploadDto.UtilityBill != null)
                    {
                        var utilityBillUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.UtilityBill, "utility_bills");
                        existingOrder.UtilityBill = utilityBillUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Utility Bill. Please upload it."));
                    }
                }
                else if(existingOrder.UtilityBill != null && smeDocumentUploadDto.UtilityBill != null)
                {
                    var utilityBillUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.UtilityBill, "utility_bills");
                    existingOrder.UtilityBill = utilityBillUrl;
                }



                if (existingOrder.LetterOfIntroduction == null)
                {
                    if (smeDocumentUploadDto.LetterOfIntroduction != null)
                    {
                        var letterOfIntroductionUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.LetterOfIntroduction, "letters_of_introduction");
                        existingOrder.LetterOfIntroduction = letterOfIntroductionUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Letter of Introduction. Please upload it."));
                    }
                }
                else if(existingOrder.LetterOfIntroduction == null && smeDocumentUploadDto.LetterOfIntroduction != null)
                {
                    var letterOfIntroductionUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.LetterOfIntroduction, "letters_of_introduction");
                    existingOrder.LetterOfIntroduction = letterOfIntroductionUrl;
                }


                if (existingOrder.CertificateOfIncorporation == null)
                {
                    if (smeDocumentUploadDto.CertificateOfIncorporation != null)
                    {
                        var certificateOfIncorporationUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.CertificateOfIncorporation, "certificates_of_incorporation");
                        existingOrder.CertificateOfIncorporation = certificateOfIncorporationUrl;
                    }
                    else
                    {
                        return BadRequest(ipNXApiResponse.Failure("We have no records of your Certificate of Incorporation. Please upload it."));
                    }
                }
                else if(existingOrder.CertificateOfIncorporation != null && smeDocumentUploadDto.CertificateOfIncorporation != null)
                {
                    var certificateOfIncorporationUrl = await _fileUploadService.UploadFileAsync(smeDocumentUploadDto.CertificateOfIncorporation, "certificates_of_incorporation");
                    existingOrder.CertificateOfIncorporation = certificateOfIncorporationUrl;
                }

                existingOrder.LastUpdatedById = userId;
                existingOrder.LastUpdatedOnUtc = DateTime.UtcNow;
                existingOrder.IsActive = true;

                await _orderService.SaveChangesAsync();

                return Ok(ipNXApiResponse.Success("Documents uploaded successfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to upload a document for a SME order");
                return StatusCode(500, ipNXApiResponse.Failure("An error occurred during sme upload"));
            }
        }


        [Authorize]
        [HttpPut("residential/place-an-order/fill-order-form/{residentialOrderId}")]
        public async Task<IActionResult> UpdateResidentialOrder(string residentialOrderId, [FromBody] ResidentialOrderDto residentialDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var discount = await _discountService.FetchStreetNameDiscountAsync(residentialDto.State, residentialDto.StreetName);

                var existingCustomer = await _userService.UserIdExistsAsync(userId);
                if (!existingCustomer)
                {
                    return NotFound(ipNXApiResponse.Failure("Customer data not found. Please sign up."));
                }
                               
                var existingResidentialOrder = await _orderService.GetResidentialOrderByIdAsync(residentialOrderId);
                if (existingResidentialOrder == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Order not found"));
                }
                var planExist = await _planService.GetPlanTypeByIdAsync(residentialDto.PlanTypeId);
                if (planExist == null)
                {
                    return NotFound(ipNXApiResponse.Failure("The selected PlanType does not exist."));
                }
                if (existingResidentialOrder.CreatedById != userId)
                {
                    return Unauthorized(ipNXApiResponse.Failure("You are not authorized to update this order."));

                }

                var _context = _orderService.OrderContext();


                _context.Entry(existingResidentialOrder).CurrentValues.SetValues(residentialDto);
                existingResidentialOrder.LastUpdatedById = userId;
                existingResidentialOrder.LastUpdatedOnUtc = DateTime.UtcNow;
                existingResidentialOrder.networkCoverageAddress = $"{residentialDto.StreetName} {residentialDto.City}, {residentialDto.State}";

                if (residentialDto.IsBillingAddressSameAsResidentialAddress == true)
                {                   
                    var existingBilling = await _orderService.ResidentialOrderBillingDetailsExistAsync(residentialOrderId);
                    _context.Entry(existingBilling).CurrentValues.SetValues(residentialDto);
                    existingBilling.LastUpdatedById = userId;
                    existingBilling.LastUpdatedOnUtc = DateTime.UtcNow; 
                    
                }
                if (discount != null)
                {
                    existingResidentialOrder.Discount = discount.Percentage;
                }
                else
                {
                    existingResidentialOrder.Discount = 0;
                }
                await _context.SaveChangesAsync();
                return Ok(ipNXApiResponse.Success("update was successful"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when trying to update residential order");
                return BadRequest(ipNXApiResponse.Failure("An error occured when trying to update residential order"));
            }
        }




        [Authorize]
        [HttpPut("sme/place-an-order/fill-order-form/{smeOrderId}")]
        public async Task<IActionResult> UpdateSmeOrder(string smeOrderId, [FromBody] SmeOrderDto smeDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ipNXApiResponse.Failure("Invalid data submitted"));
            }
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var discount = await _discountService.FetchStreetNameDiscountAsync(smeDto.State, smeDto.CompanyStreetName);

                var existingCustomer = await _userService.UserIdExistsAsync(userId);
                if (!existingCustomer)
                {
                    return NotFound(ipNXApiResponse.Failure("Customer data not found. Please sign up."));
                }

                var existingSmeOrder = await _orderService.GetSmeOrderByIdAsync(smeOrderId);
                if (existingSmeOrder == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Order not found"));
                }
                var planExist = await _planService.GetPlanTypeByIdAsync(smeDto.PlanTypeId);
                if (planExist == null)
                {
                    return NotFound(ipNXApiResponse.Failure("The selected PlanType does not exist."));
                }
                if (existingSmeOrder.CreatedById != userId)
                {
                    return Unauthorized(ipNXApiResponse.Failure("You are not authorized to update this order."));

                }

                var _context = _orderService.OrderContext();

                _context.Entry(existingSmeOrder).CurrentValues.SetValues(smeDto);
                existingSmeOrder.LastUpdatedById = userId;
                existingSmeOrder.LastUpdatedOnUtc = DateTime.UtcNow;
                existingSmeOrder.networkCoverageAddress = $"{smeDto.CompanyStreetName} {smeDto.City}, {smeDto.State}";

                if (smeDto.IsBillingAddressSameAsResidentialAddress == true)
                {
                    var existingBilling = await _orderService.SmeOrderBillingDetailsExistAsync(smeOrderId);
                    _context.Entry(existingBilling).CurrentValues.SetValues(smeDto);
                    existingBilling.LastUpdatedById = userId;
                    existingBilling.LastUpdatedOnUtc = DateTime.UtcNow;
                }
                if (discount != null)
                {
                    existingSmeOrder.Discount = discount.Percentage;
                }
                else
                {
                    existingSmeOrder.Discount = 0;
                }
                await _context.SaveChangesAsync();
                return Ok(ipNXApiResponse.Success("Your sme order was updated successfully"));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured when trying to update sme order");
                return BadRequest(ipNXApiResponse.Failure("An error occured when trying to update sme orde"));
            }
        }


        [Authorize]
        [HttpGet("admin/view-all-customer-orders")]
        public async Task<IActionResult> GetAllCustomerOrders([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var paginatedOrders = await _orderService.ViewAllCustomerOrdersAsync(pageNumber, pageSize);
                if (paginatedOrders == null || !paginatedOrders.Items.Any())
                {
                    return NotFound(ipNXApiResponse.Failure("No orders have been placed yet"));
                }

                var result = new
                {
                    Orders = paginatedOrders.Items,
                    Metadata = new
                    {
                        paginatedOrders.TotalCount,
                        paginatedOrders.PageSize,
                        paginatedOrders.CurrentPage,
                        paginatedOrders.TotalPages,
                        paginatedOrders.HasNext,
                        paginatedOrders.HasPrevious
                    }
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to fetch all customer orders");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while trying to fetch all customer orders"));
            }
        }




        [Authorize]
        [HttpGet("admin/view-customer-order/{orderId}")]
        public async Task<IActionResult> ViewCustomerOrderById(string orderId)
        {
            try
            {
                var orderDetails = await _orderService.GetOrderDetailsByIdAsync(orderId);
                if (orderDetails == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Order not found"));
                }
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to fetch order details");
                return StatusCode(500, ipNXApiResponse.Failure("An error occurred while trying to fetch order details"));
            }
        }





        [Authorize]
        [HttpGet("payment-verification/{orderId}/{referenceNumber}/{agentId}")]
        public async Task<IActionResult> VerifyAndConfirmOrder(string orderId, string referenceNumber, string agentId)
        {
            try
            {
                var (exist,order) = await _orderService.OrderExistAsync(orderId);
                if (!exist)
                {
                    return NotFound(ipNXApiResponse.Failure("Order not found"));
                }
                var orderIsConfirmed = await _orderService.OrderPaymentIsConfirmedAsync(order, referenceNumber);
                if (!orderIsConfirmed)
                {
                    return Ok(ipNXApiResponse.Failure("Payment was attempted but failed"));
                }

                
                // Determine the West African Time (WAT) time zone
                var watTimeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Central Africa Standard Time");
                var utcNow = DateTime.UtcNow;
                var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, watTimeZone);

                var result = new {                    
                    OrderId = order.Id,
                    ReferenceNumber = order.PaymentReferenceNumber,
                    Status = order.PaymentStatus,
                    Date = localTime.ToString("MMMM dd, yyyy"),
                    Time = localTime.ToString("h:mm tt")
                };
                var recipients = new List<string>{"pfermac@ipnxnigeria.net","telesalesunit@ipnxnigeria.net"};

                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var myOrder = await _orderService.GetOrderByIdAsync(result.OrderId);

                if (myOrder.ContactPersonEmail != null)
                {
                    recipients.Add(myOrder.ContactPersonEmail);
                }
                else
                {
                    recipients.Add(myOrder.Email);
                }

                //check wifi status and send out email to telesales
                if (order.HasRequestedToAddWifi == true)
                {
                    await _emailService.SendEmailAsync(recipients, "WIFI REQUEST - NEW SALES PORTAL TEST", $"order with reference number {result.ReferenceNumber} has requested for Wifi");
                }               
                await _emailService.SendEmailAsync(recipients, "ORDER PLACED! - NEW SALES PORTAL TEST", $"Order with ID:{order.Id} and reference number {result.ReferenceNumber} and  has been successfully placed.");


                //Attach agent to order
                await _orderService.AttachAgentToOrderAsync(order, agentId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred. Please ensure the reference number is valid.");
                return BadRequest(ipNXApiResponse.Failure("An error occurred. Please ensure the reference number is valid."));
            }
        }




        [Authorize]
        [HttpGet("view-my-orders")]
        public async Task<IActionResult> ViewMyOrders()
        {
            try
            {
                var userIdFromClaims = User.Claims.FirstOrDefault()?.Value;
                var userId = new Guid(userIdFromClaims);
                var myOrders = await _orderService.GetMyOrdersAsync(userId);
                if (myOrders == null)
                {
                    return NotFound(ipNXApiResponse.Failure("No orders found"));
                }
                return Ok(myOrders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to fetch orders ");
                return BadRequest(ipNXApiResponse.Failure("An error occurred while trying to fetch orders "));
            }
        }



        [Authorize]
        [HttpGet("view-my-order/{orderId}")]
        public async Task<IActionResult> ViewMyOrderById(string orderId) 
        {
            try
            {
                var orderDetails = await _orderService.GetOrderByIdAsync(orderId);
                if (orderDetails == null)
                {
                    return NotFound(ipNXApiResponse.Failure("Order not found"));
                }
                return Ok(orderDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to fetch order details");
                return StatusCode(500, ipNXApiResponse.Failure("An error occurred while trying to fetch order details"));
            }
        }

        [Authorize]
        [HttpGet("attach-agent-to-order")]
        public async Task<IActionResult> AttachAgentToOrder(string orderId, string agentId)
        {
            try
            {
                var (orderExists, order) = await _orderService.OrderExistAsync(orderId);
                if (!orderExists)
                {
                    return NotFound(ipNXApiResponse.Failure("Order Not found"));
                }
                await _orderService.AttachAgentToOrderAsync(order, agentId);
                return Ok("Agent has been attached to order");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to attach agent to the order");
                return StatusCode(500, ipNXApiResponse.Failure("An error occurred while trying to attach agent to the order"));
            }
        }


    }
}
