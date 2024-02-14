using Payment_Backend_Service.Common;
using Payment_Backend_Service.Data.Domain;
using Payment_Backend_Service.Models;

namespace Payment_Backend_Service.Services
{
    public interface IPaymentService
    {
        Task<ServiceResponse> AddSalaryAsync(EmployeeSalaryDetailDTO employeeSalaryDetail, DefaultHttpContext context);
        Task<List<EmployeeSalaryDetails>> GetEmployeeSalaryDetailsAsync(List<int>? employeeIds = null);

        Task<List<PaymentStatusDTO>> AddPaymentRequestAsync(List<int>? paymentRequestIds);

    }
}
