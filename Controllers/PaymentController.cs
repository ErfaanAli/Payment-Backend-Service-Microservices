using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Payment_Backend_Service.Common;
using Payment_Backend_Service.Models;
using Payment_Backend_Service.Services;
using System.Security.Claims;
using static Payment_Backend_Service.Common.Enums;

namespace Payment_Backend_Service.Controllers
{
    [ApiController]
    [Route("api/payment/[controller]")]
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
        }

        [HttpPost("add-salary")]
        public async Task<IActionResult> AddSalary([FromBody] EmployeeSalaryDetailDTO salaryInput)
        {
            try
            {

                // Retrieve userId from authentication cooke let's assume it's a JWT
                //As it's an existing system so it must have that inplemented
                var context = new DefaultHttpContext();


                if(context == null)
                {
                    //Add Error Logs , traces properly as it's an existing system so
                    //I beleieve that would be a common logging system already
                    return StatusCode(500, new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "Context is null" });
                }

                var rquestinguserRole = Roles.Accountant;
                ////Check If Logged in user Role is Accountant
                //if (!context.User.IsInRole("Manager"))
                //{

                //    //Add Error Logs , traces properly as it's an existing system so
                //    //I beleieve that would be a common logging system already
                //    //example 
                //    //_logger.trace("message" , status , TraceLevel.Error)
                //    return BadRequest(new ServiceResponse { StatusCode = 401, Status = "User unathorized", Message = "Provided User is not authorized to perform this action" });

                //}


                // Validate input
                if (salaryInput == null)
                {

                    //Add Error Logs , traces properly as it's an existing system so
                    //I beleieve that would be a common logging system already
                   //example 
                    //_logger.trace("message" , status , TraceLevel.Error)
                    return BadRequest(new ServiceResponse { StatusCode = 400, Status = "Bad Request", Message = "Invalid salary input" });
                }

                //

                // Perform any additional validation if needed

                // Add salary to the employee microservice asynchronously
                var response = await _paymentService.AddSalaryAsync(salaryInput, context);

                if (response.StatusCode == 200)
                {
                    // Log success
                }
                else
                {
                    // Log Error
                }
                // Return a  response
                return Ok(new { StatusCode = response.StatusCode, Status = response .Status, Message = response.Message });

               
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's needs
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("get-salaries")]
        public async Task<IActionResult> GetEmployeeSalaries([FromQuery] List<int>? employeeIds)
        {
            try
            {
                // Retrieve userId from authentication cooke let's assume it's a JWT
                //As it's an existing system so it must have that inplemented
                var context = new DefaultHttpContext();


                if (context == null)
                {
                    //Add Error Logs , traces properly as it's an existing system so
                    //I beleieve that would be a common logging system already
                    return StatusCode(500, new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "Context is null" });
                }
                var employeeSalaries = await _paymentService.GetEmployeeSalaryDetailsAsync(employeeIds);

                if (employeeSalaries != null && employeeSalaries.Any())
                {
                    return Ok(new ServiceResponse
                    {
                        StatusCode = 200,
                        Status = "Success",
                        Message = "Employee salaries retrieved successfully",
                        Data = employeeSalaries
                    });
                }
                else
                {
                    return NotFound(new ServiceResponse
                    {
                        StatusCode = 404,
                        Status = "Not Found",
                        Message = "No employee salaries found"
                    });
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it based on your application's needs
                return StatusCode(500, new ServiceResponse
                {
                    StatusCode = 500,
                    Status = "Internal Server Error",
                    Message = "An error occurred while retrieving employee salaries"
                });
            }
        }

        [HttpPost("add-payment-requests")]
        public async Task<IActionResult> AddPaymentRequests([FromBody] List<int>? paymentRequestIds)
        {
            try
            {
                // Retrieve userId from authentication cooke let's assume it's a JWT
                //As it's an existing system so it must have that inplemented
                var context = new DefaultHttpContext();


                if (context == null)
                {
                    //Add Error Logs , traces properly as it's an existing system so
                    //I beleieve that would be a common logging system already
                    return StatusCode(500, new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "Context is null" });
                }

                var rquestinguserRole = Roles.Manager; // Accountant
                ////Check If Logged in user Role is Manager OR Account
                //if (!context.User.IsInRole("Manager,accountant"))
                //{

                //    //Add Error Logs , traces properly as it's an existing system so
                //    //I beleieve that would be a common logging system already
                //    //example 
                //    //_logger.trace("message" , status , TraceLevel.Error)
                //    return BadRequest(new ServiceResponse { StatusCode = 401, Status = "User unathorized", Message = "Provided User is not authorized to perform this action" });

                //}
                // Validate input
                if (paymentRequestIds == null || !paymentRequestIds.Any())
                {
                    return BadRequest("Invalid payment request IDs");
                }

                // Call the service to add payment requests
                var paymentStatusList = await _paymentService.AddPaymentRequestAsync(paymentRequestIds);

                // Return the response
                return Ok(new PaymentResponse<List<PaymentStatusDTO>>
                {
                    StatusCode = 200,
                    Status = "Success",
                    Message = "Payment requests processed successfully",
                    Data = paymentStatusList
                });
            }
            catch (Exception ex)
            {
                // Log the exception using common logging

                // Return an error response
                return StatusCode(500, new ServiceResponse
                {
                    StatusCode = 500,
                    Status = "Error",
                    Message = "An error occurred while processing payment requests"
                });
            }
        }

    }
}
