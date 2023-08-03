namespace OnlineStore.Backend.Data
{
    public class Product
    {
        public Guid Id { get; init; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
