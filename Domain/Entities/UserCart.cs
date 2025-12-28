

namespace Domain.Entities;

public class UserCart
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<UserCartItem> CartItems { get; set; } = new List<UserCartItem>();
}
