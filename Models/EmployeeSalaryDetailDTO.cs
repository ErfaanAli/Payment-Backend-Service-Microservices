namespace Payment_Backend_Service.Models
{
    public class EmployeeSalaryDetailDTO
    {
        public int EmployeeId { set; get; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public string Department { get; set; }
        public DateTime SalaryMonth { get; set; } = DateTime.Now;
        public double SalaryAmount { get; set; }

    }
}
