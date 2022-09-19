namespace PharmacyWebApp.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string? Comment { get; set; }
        public DateTime AddedDateTime { get; set; } = DateTime.Now;
        /*public int ProductId { get; set; }
        [ValidateNever]
        public Product Product { get; set; }
        public int UserId { get; set; }
        [ValidateNever]
        public User User { get; set; }*/
    }
}
