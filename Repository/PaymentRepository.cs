using Microsoft.EntityFrameworkCore;
using Payment_Backend_Service.Common;
using Payment_Backend_Service.Data;
using Payment_Backend_Service.Data.Domain;
using Payment_Backend_Service.Models;

namespace Payment_Backend_Service.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDbContext _DbContext;
        public PaymentRepository(PaymentDbContext paymentDbContext)
        {
            this._DbContext = paymentDbContext;
        }

        public async Task<ServiceResponse> AddEmployeeSalaryDetailsAsync(EmployeeSalaryDetails employeeSalaryDetails)
        {
            try
            {
                // Validate input
                if (employeeSalaryDetails == null)
                {
                    // Log the Trace using Common Logger
                    return new ServiceResponse { StatusCode = 400, Status = "Bad Request", Message = "Invalid employee salary details input" };
                }

                // Add employee salary details to the database
                await _DbContext.EmployeeSalaryDetails.AddAsync(employeeSalaryDetails);
                await _DbContext.SaveChangesAsync();

                // Log the Trace using Common Logger

                // Return success response
                return new ServiceResponse { StatusCode = 200, Status = "OK", Message = "Employee salary details added successfully" };
            }
            catch (Exception ex)
            {
                // Log the exception using Common Logger

                // Return an error response
                return new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "An error occurred while adding employee salary details" };
            }
        }

        public async Task<ServiceResponse> AddPaymentRequestAsync(SalaryRequest salaryRequest)
        {
            try
            {
                // Validate input
                if (salaryRequest == null)
                {
                    // Log the Trace using Common Logger
                    return new ServiceResponse { StatusCode = 400, Status = "Bad Request", Message = "Invalid employee salary details input" };
                }

                // Add employee salary details to the database
                await _DbContext.SalaryRequests.AddAsync(salaryRequest);
                await _DbContext.SaveChangesAsync();

                // Log the Trace using Common Logger

                // Return success response
                return new ServiceResponse { StatusCode = 200, Status = "OK", Message = "Employee salary details added successfully" };
            }
            catch (Exception ex)
            {
                // Log the exception using Common Logger

                // Return an error response
                return new ServiceResponse { StatusCode = 500, Status = "Internal Server Error", Message = "An error occurred while adding employee salary details" };
            }

        }

        public async Task<List<EmployeeSalaryDetails>> GetEmployeeSalaryDetailsAsync(List<int>? salaryDetailsIds = null)
        {
            try
            {
                // If Requested data along with Ids
                if (salaryDetailsIds != null && salaryDetailsIds.Any())
                {
                    return await _DbContext.EmployeeSalaryDetails
                        .Where(e => salaryDetailsIds.Contains(e.Id))
                        .ToListAsync();
                }
                else
                {
                    // Return all the records
                    return await _DbContext.EmployeeSalaryDetails.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                // Log the exception 
                throw ex;
            }
        }

       
        public async Task<ServiceResponse> UpdateSalaryRequest(SalaryRequest employeeSalaryDetails)
        {
            return new ServiceResponse { StatusCode = 200, Status = "OK", Message = "Employee salary details added successfully" };
        }
    }
}
