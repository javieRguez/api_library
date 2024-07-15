namespace WebApiLibrary.Domain.Entities
{
    public class BookLoan
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}
