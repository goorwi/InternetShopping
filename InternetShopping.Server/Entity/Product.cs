using LinqToDB.Mapping;

namespace ShopLibrary
{
    [Table("Product")]
    public class Product
    {
        [PrimaryKey, Identity] public int Id { get; set; }

        [Column("Title"), NotNull] public string Title { get; set; }

        [Column("CategoryId"), NotNull] public int CategoryId { get; set; }

        [Association(ThisKey = nameof(CategoryId), OtherKey = nameof(Category.Id))]
        public Category? category { get; set; }

        [Column("Producer"), NotNull] public string Producer { get; set; }

        [Column("SupplierId"), NotNull] public int SupplierId { get; set; }

        [Association(ThisKey = nameof(SupplierId), OtherKey = nameof(Supplier.Id))]
        public Supplier? supplier { get; set; }

        [Column("Content"), NotNull] public string Content { get; set; }

        [Column("Price"), NotNull] public int Price { get; set; }
    }
}
