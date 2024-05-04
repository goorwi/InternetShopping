using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopLibrary
{
    [Table("Supplier")]
    public class Supplier
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("Title"), NotNull]
        public string Title { get; set; }
    }
}
