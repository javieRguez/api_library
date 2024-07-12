namespace WebApiLibrary.Domain.Entities.Generics
{
    public class Paginated<T>
    {
        public List<T> Items { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }

        public Paginated(List<T> items, int page, int totalPages) { 
        
            Items = items;
            Page = page;
            TotalPages = totalPages;
        }
    }
}
