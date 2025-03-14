namespace ecommerce.auth_ecommerce.Models
{
    public class Utenti
    {
        public int Id { get; set; }
        public string email { get; set; } = string.Empty("");
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
    }
}

