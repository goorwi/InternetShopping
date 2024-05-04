using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    [Table("Stockroom")]
    public class Stockroom
    {
        [PrimaryKey, Identity] public int Id { get; set; }

        [Column("ProductId"), NotNull] public int ProductId { get; set; }

        [Association(ThisKey = nameof(ProductId), OtherKey = nameof(Product.Id))]
        public Product? product { get; set; }

        [Column("Address"), NotNull] public string Address { get; set; }

        [Column("Amount"), NotNull] public int Amount { get; set; }
    }
}
