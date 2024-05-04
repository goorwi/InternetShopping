using LinqToDB.Mapping;

namespace ShopLibrary
{
    [Table("Customer")]
    public class Customer
    {
        [PrimaryKey, Identity] public int Id { get; set; }

        [Column("Name"), NotNull] public string Name { get; set; }

        [Column("Address"), NotNull] public string Address { get; set; }

        [Column("Phone"), NotNull] public string Phone { get; set; }

        [Column("Email"), NotNull] public string Email { get; set; }

        [Column("Password"), NotNull] public string Password { get; set; }

        [Column("IsAdmin"), NotNull] public bool IsAdmin { get; set; }
    }
}
