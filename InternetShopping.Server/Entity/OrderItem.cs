using LinqToDB.Mapping;

namespace ShopLibrary
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [PrimaryKey, Identity] public int Id { get; set; }

        [Column("ProductId"), NotNull] public int ProductId { get; set; }

        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product? product { get; set; }

        [Column("Amount"), NotNull] public int Amount { get; set; }

        [Column("Price"), NotNull] public int Price { get; set; }

        [Column("OrderId"), NotNull] public int OrderId { get; set; }

        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Order.Id))]
        public Order? order { get; set; }

        [Column("StockroomId"), NotNull] public int StockroomId { get; set; }

        [Association(ThisKey = nameof(StockroomId), OtherKey = nameof(Stockroom.Id))]
        public Stockroom? stockroom { get; set; }
    }
}
