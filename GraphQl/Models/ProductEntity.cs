using System.ComponentModel;
using System.Data.SqlTypes;

namespace GraphQl.Models
{
    public class ProductEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public uint Amount { get; set; }
        public decimal Cost { get; set; }
        public virtual Category Category { get; set; }
        public virtual Storage Storage { get; set; }
    }
}
