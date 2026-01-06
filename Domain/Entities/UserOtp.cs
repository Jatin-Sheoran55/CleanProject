

namespace Domain.Entities;

public class UserOtp
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int Otp { get; set; } 
    public DateTime ExpiryTime { get; set; }
    public bool IsUsed { get; set; }

   
}
