using LinqToDB.Mapping;

namespace ShopLibrary
{
    [Table("Order")]
    public class Order
    {
        [PrimaryKey, Identity] public int Id { get; set; }

        [Column("CustomerId"), NotNull] public int CustomerId { get; set; }

        [Association(ThisKey = nameof(CustomerId), OtherKey = nameof(Customer.Id))]
        public Customer? customer { get; set; }

        [Column("Cost"), NotNull] public int Cost { get; set; }

        [Column("OrderStatus"), NotNull] public string OrderStatus { get; set; }

        [Column("OrderDate"), NotNull] public string OrderDate { get; set; }
    }
}
