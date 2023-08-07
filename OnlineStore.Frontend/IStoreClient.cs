namespace OnlineStore
{
    public interface IStoreClient
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct();
        Task AddProduct(Product product);
        Task DeleteProduct(Product product);
    }
}
