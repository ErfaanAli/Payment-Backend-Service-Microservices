using Payment_Backend_Service.Common;
using Payment_Backend_Service.Data.Domain;
using Payment_Backend_Service.Models;

namespace Payment_Backend_Service.Repository
{
    public interface IPaymentRepository
    {
        Task<List<EmployeeSalaryDetails>> GetEmployeeSalaryDetailsAsync(List<int>? salaryDetailsIds = null);
        Task<ServiceResponse> AddPaymentRequestAsync(SalaryRequest salaryRequestId);
        Task<ServiceResponse> AddEmployeeSalaryDetailsAsync(EmployeeSalaryDetails employeeSalaryDetails);
        Task<ServiceResponse> UpdateSalaryRequest(SalaryRequest employeeSalaryDetails);

    }
}
