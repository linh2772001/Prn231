namespace BusinessObject.DTO
{
    public class EmployeeDTO
    {
        public string EmployeeId { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? FirstName { get; set; }
        public int? DepartmentId { get; set; }
        public string? Title { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? HireDate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
    }
}
