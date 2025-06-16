namespace Client.Models.DTOs
{
    public class RegisterDTO
    {
        public string AccountName { get; set; }
        public string AccountEmail { get; set; }
        public string AccountPassword { get; set; }
        public int Role { get; set; }
    }
}
