namespace WebApiLibrary.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<BookLoan> BookLoans { get; set; } = new List<BookLoan>();
    }
}
