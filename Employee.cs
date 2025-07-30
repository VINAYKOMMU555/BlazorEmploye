using System.ComponentModel.DataAnnotations;

namespace BlazorEmployee.Data
{
    public class Employee
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Job_title {  get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Work_phone number can not excced more than 10 Charecters")]
        public string Work_phone { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Cell_phone number can not excced more than 10 Charecters")]
        public string Cell_phone { get; set; }
        [Required]
        public string Department {  get; set; }
        [Required]
        public string Manager { get; set; }
    }
}
