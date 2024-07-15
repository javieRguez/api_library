using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiLibrary.Domain.Entities
{
    [Table("Books")]
    public class Book
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "El campo 'Nombre' es obligatorio.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "El campo 'Precio' es obligatorio.")]
        public required double Price { get; set; }

        [Required(ErrorMessage = "El campo 'Descripción' es obligatorio.")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "El campo 'Cantidad' es obligatorio.")]
        public required int Quantity { get; set; }

        public GenderEnum Gender { get; set; }

        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }

    public enum GenderEnum
    {
        FANTASY,
        ROMANTIC
    }
}
