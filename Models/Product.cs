namespace OnlineStore
{
    public class Product
    {
        public Product(string title, decimal price)
        {
            if (string.IsNullOrEmpty(title)) throw new ArgumentException(nameof(title));
            if (price <= 0) throw new ArgumentException($"Цена задана некорректно! {nameof(price)}");

            Title = title;
            Price = price;
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
