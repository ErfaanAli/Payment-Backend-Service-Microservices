using System.ComponentModel.DataAnnotations;

namespace Payment_Backend_Service.Data.Domain
{

    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

    }
}
