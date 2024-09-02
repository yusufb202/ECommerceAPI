// Models/Order.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? OrderDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus? Status { get; set; }
        public ICollection<OrderItem>? Items { get; set; }
    }

    // Models/OrderItem.cs
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }
    }

    // Models/ShoppingCart.cs
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<ShoppingCartItem>? Items { get; set; }
    }

    // Models/ShoppingCartItem.cs
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ShoppingCartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }

    // Models/WishList.cs
    public class WishList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<WishListItem>? Items { get; set; }
    }

    // Models/WishListItem.cs
    public class WishListItem
    {
        public int Id { get; set; }
        public int WishListId { get; set; }
        public int ProductId { get; set; }
        public WishList? WishList { get; set; }
    }

    // Enums/OrderStatus.cs
    public enum OrderStatus
    {
        Pending = 1,
        Shipped = 2,
        Delivered = 3,
        Canceled = 4
    }
}