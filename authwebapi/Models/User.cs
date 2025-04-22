namespace authwebapi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Name { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool isActive { get; set; } = true;
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
