
namespace Domain.Entities;

public class Order
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public int TableNo { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pending";

    
    public decimal TotalAmount { get; set; }

    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
}
