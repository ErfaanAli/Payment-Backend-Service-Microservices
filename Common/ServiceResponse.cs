using Payment_Backend_Service.Data.Domain;

namespace Payment_Backend_Service.Common
{
    public class ServiceResponse
    {
        public int StatusCode { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public List<EmployeeSalaryDetails> Data { get; set; }

    }
}
