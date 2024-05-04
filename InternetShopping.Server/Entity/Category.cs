using LinqToDB.Mapping;

namespace ShopLibrary
{
    [Table("Category")]
    public class Category
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("Title"), NotNull]
        public string Title { get; set; }
    }
}