namespace BusinessObject.DTO
{
    public class CustomerDTO
    {
        public string CustomerId { get; set; } = null!;
        public DateTime? CreateDate { get; set; }
        public string? Phone { get; set; }
        public string? Name { get; set; }
        public string? District { get; set; }
        public string? Province { get; set; }
        public string? Wards { get; set; }
        public string? Address { get; set; }
    }
}
