using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLibrary.Domain.Entities
{
    [Table("Books")]
    public class Book
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required double Price { get; set; }
        public required string Description { get; set; }
        public int Quantity { get; set; }
        public GenderEnum Gender { get; set; }
    }

    public enum GenderEnum
    {
        FANTASY,
        ROMANTIC
    }
}
