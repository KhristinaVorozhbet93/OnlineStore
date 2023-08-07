using System.Net.Http.Json;

namespace OnlineStore
{
    public class StoreClient : IDisposable, IStoreClient
    {
        private readonly string _host; 
        private readonly HttpClient? _httpClient;

        public StoreClient(string host = "http://mystore.com/",HttpClient? client = null)
        {
            if (string.IsNullOrEmpty(host)) throw new ArgumentException(nameof(host));
            if (!Uri.TryCreate(host, UriKind.Absolute, out var hostUri))
            {
                throw new ArgumentException("The host adress shoild be a valid URL", nameof(host));
            }
            _host = host; 
            _httpClient = client ?? new HttpClient();
            if (_httpClient.BaseAddress is null)
            {
                _httpClient.BaseAddress = hostUri;
            }
        }
        public void Dispose()
        {
            _httpClient.Dispose();
        }
        public async Task<List<Product>> GetProducts()
        {
            var uri = "get_products";
            var products = await _httpClient.GetFromJsonAsync<List<Product>>(uri);
            return products;
        }
        public async Task<Product> GetProduct()
        {
            var uri = "get_product";
            var products = await _httpClient.GetFromJsonAsync<Product>(uri);
            return products;
        }
        public async Task AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(product);
            var uri = "add_product";
            var products = await _httpClient.PostAsJsonAsync(uri, product);
        }
    }
}
