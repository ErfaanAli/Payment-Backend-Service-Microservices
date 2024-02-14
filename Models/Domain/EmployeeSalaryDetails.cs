using System.ComponentModel.DataAnnotations;

namespace Payment_Backend_Service.Data.Domain
{
    public class EmployeeSalaryDetails
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }

        public double salary { get; set; }
        public DateTime Month { get; set; }

        public int addedBy { get; set; }
        public DateTime addedDate { get; set; }

        public bool isLocked { get; set; } = false;
        public SalaryRequest SalaryRequest { get; set; }
    }
}
