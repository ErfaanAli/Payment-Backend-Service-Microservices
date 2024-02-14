using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net.NetworkInformation;
using static Payment_Backend_Service.Common.Enums;

namespace Payment_Backend_Service.Data.Domain
{
    public class SalaryRequest
    {
        [Key]
        public int Id { get; set; }

        public string RequestType { get; set; }

        public int RequestedBy { get; set; } // Assuming Accountant's EmployeeId is an integer

        public DateTime RequestedDate { get; set; }

        public Status Status { get; set; }

        public string? RejectedReason { get; set; }

        public int? FileUploadId { get; set; }

        public bool? ManagerApprovalStatus { get; set; }
        public DateTime? ManagerApprovalDate { get; set; }
        public int? ManagerUserId { get; set; }
        public string? ManagerComments { get; set; }
        public bool? AccountantApprovalStatus { get; set; }
        public DateTime? AccountantApprovalDate { get; set; }
        public int? AccountantUserId { get; set; }
        public string? AccountantComments { get; set; }

        public int EmployeeSalaryDetailsId { get; set; }
        [ForeignKey("EmployeeSalaryDetailsId")]

        public EmployeeSalaryDetails EmployeeSalaryDetails { get; set; }
    }
}
