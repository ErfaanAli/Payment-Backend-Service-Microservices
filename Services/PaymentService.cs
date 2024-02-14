using Azure.Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Payment_Backend_Service.Common;
using Payment_Backend_Service.Data.Domain;
using Payment_Backend_Service.Models;
using Payment_Backend_Service.Repository;
using static Payment_Backend_Service.Common.Enums;

namespace Payment_Backend_Service.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public async Task<List<PaymentStatusDTO>> AddPaymentRequestAsync(List<int>? paymentRequestIds)
        {
            // Create a list to store the results of each request
            List<PaymentStatusDTO> paymentRequestStatus = new List<PaymentStatusDTO>();
            RequestType Requestype = paymentRequestIds.Count > 1 ? RequestType.Group : RequestType.Individual;
            try
            {
                foreach (var Id in paymentRequestIds)
                {
                    //Check if provided salary details Id is a ValidId or Not
                    var isSalaryIdValid = await _paymentRepository.GetEmployeeSalaryDetailsAsync(new List<int> { Id });

                    if (isSalaryIdValid != null && isSalaryIdValid.Count > 0)
                    {
                        var paymentRequest = new SalaryRequest()
                        {
                            EmployeeSalaryDetailsId = Id,
                            RequestedBy = 1, // Replace with the actual logged-in user's ID
                            RequestedDate = DateTime.Now,
                            Status = Status.AwaitingApproval,
                            RequestType = Requestype.ToString(),
                        };

                        // Process each request individually
                        var paymentStatus = await _paymentRepository.AddPaymentRequestAsync(paymentRequest);

                        // Create a PaymentStatusDTO based on the ServiceResponse
                        PaymentStatusDTO response = new PaymentStatusDTO
                        {
                            SalaryDetailsId = Id,
                            Status = paymentStatus.Status,
                            Message = paymentStatus.Message
                        };
                        // Add the result to the list
                        paymentRequestStatus.Add(response);
                    }
                    else
                    {
                        PaymentStatusDTO response = new PaymentStatusDTO
                        {
                            SalaryDetailsId = Id,
                            Status = "Error",
                            Message = "This records doesn't exists in Database"
                        };
                        paymentRequestStatus.Add(response);
                    }
                }

                // Return the overall status along with individual results
                return paymentRequestStatus;
            }
            catch (Exception ex)
            {
                // Log exception using Common Logger
                // LogException(ex);

                // Return an error response
                return new List<PaymentStatusDTO>
                {
                    new PaymentStatusDTO
                    {
                        Status = "Error",
                        Message = "An error occurred while processing payment requests."
                    }
                };
        
            }
        }

        public async Task<ServiceResponse> AddSalaryAsync(EmployeeSalaryDetailDTO employeeSalaryDetail, DefaultHttpContext context)
        {
            try
            {
                // Ensure context is not null
                if (context == null)
                {
                    //Log the Error Trace using Common Logger
                    return new ServiceResponse { StatusCode = 400, Status = "Bad Request", Message = "Invalid HTTP context" };
                }

                // Check if it's the current month entry data
                if (employeeSalaryDetail.SalaryMonth.Month != DateTime.Now.Month || employeeSalaryDetail.SalaryMonth.Year != DateTime.Now.Year)
                {
                    // Log the error

                    // Return a response indicating that only data for the current month can be added
                    return new ServiceResponse
                    {
                        StatusCode = 400,
                        Status = "Bad Request",
                        Message = "Salary entry must be for the current month"
                    };
                }



                // Create EmployeeSalaryDetails object
                var employeeSalaryDetails = new EmployeeSalaryDetails
                {
                    Month = employeeSalaryDetail.SalaryMonth,
                    salary = employeeSalaryDetail.SalaryAmount,
                    EmployeeId = employeeSalaryDetail.EmployeeId,
                    //it would be loggedinUser.UserId
                    //As it's an existing application then there must be common implementation to fetch logged in UserDetails
                    addedBy = 1,
                    addedDate = DateTime.Now,
                    isLocked = false
                };


                // Call the repository method to add employee salary details
                var result = await _paymentRepository.AddEmployeeSalaryDetailsAsync(employeeSalaryDetails);

                if (result.StatusCode == 200)
                {
                    // Log success traces using Common Logger

                }
                else
                {
                    // Log error traces using Common Logger
                }

                // Return the result from the repository
                return result;
            }
            catch (Exception ex)
            {
                // Log exception using Common Logger

                // Return an error response
                return new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "An error occurred while adding employee salary details" };
            }
        }

        public async Task<List<EmployeeSalaryDetails>> GetEmployeeSalaryDetailsAsync(List<int>? employeeIds)
        {
            return await _paymentRepository.GetEmployeeSalaryDetailsAsync(employeeIds);
        }
    }
}
