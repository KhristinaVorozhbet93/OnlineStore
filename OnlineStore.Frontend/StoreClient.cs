using System.Net.Http.Json;

namespace OnlineStore
{
    public class StoreClient : IDisposable, IStoreClient
    {
        private readonly string _host; 
        private readonly HttpClient? _httpClient;
        private bool _disposed;

        public StoreClient(string host = "http://mystore.com/", HttpClient? client = null)
        {
            if (string.IsNullOrEmpty(host)) throw new ArgumentException(nameof(host));
            if (!Uri.TryCreate(host, UriKind.Absolute, out var hostUri))
            {
                throw new ArgumentException("The host adress shoild be a valid URL", nameof(host));
            }
            _host = host;
            
            if (client is null)
            {
                _httpClient = new HttpClient();
                _disposed = true;
            }
            else
            {
                _httpClient = client;
            }

            if (_httpClient.BaseAddress is null)
            {
                _httpClient.BaseAddress = hostUri;
            }
        }
        public void Dispose()
        {
            if (_disposed)
            {
                _httpClient!.Dispose();
            }
        }
        public async Task<List<Product>> GetProducts()
        {
            var uri = "get_products";
            var products = await _httpClient!.GetFromJsonAsync<List<Product>>(uri);
            if (products is null)
            {
                throw new ArgumentNullException(nameof(products));
            }
            return products;
        }
        public async Task<Product> GetProduct()
        {
            var uri = "get_product";
            var product = await _httpClient!.GetFromJsonAsync<Product>(uri);
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            return product;
        }
        public async Task AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            var uri = "add_product";
            var response = await _httpClient!.PostAsJsonAsync(uri, product);
            response.EnsureSuccessStatusCode(); 
        }
        public async Task DeleteProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            var uri = "delete_product";
            var response = await _httpClient!.PostAsJsonAsync(uri, product);
            response.EnsureSuccessStatusCode();
        }
    }
}
