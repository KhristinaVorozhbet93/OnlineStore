namespace OnlineStore
{
    public class Catalog : ICatalog
    {
        private object _productsSyncObj = new();

        private List<Product> _products;

        public Catalog()
        {
            _products = GenerateProducts(10);
        }

        public List<Product> GetProducts()
        {
            lock (_productsSyncObj)
            {
                return _products;
            }
            throw new ArgumentNullException(nameof(_products));
        }

        public void AddProduct(Product product)
        {
            ArgumentNullException.ThrowIfNull(nameof(product));
            lock (_productsSyncObj)
            {
                _products.Add(product);
            }
        }
        public Product GetProduct(Guid id)
        {
            for (int i = 0; i < _products.Count; i++)
            {
                if (_products[i].Id == id)
                {
                    lock (_productsSyncObj)
                    {
                        return _products[i];
                    }
                }
            }
            throw new ArgumentException($"Продукта с ID={id} не существует!");
        }


        public void DeleteProduct(Guid productId)
        {
            foreach (var product in _products)
            {
                if (product.Id == productId)
                {
                    lock (_productsSyncObj)
                    {
                        _products.Remove(product);
                    }
                }
            }
            throw new ArgumentException($"Продукта с ID={productId} не существует!");
        }

        public void UpdateProduct(Guid productId, Product newProduct)
        {
            ArgumentNullException.ThrowIfNull(nameof(newProduct));
            foreach (var product in _products)
            {
                if (product.Id == productId)
                {
                    lock (_productsSyncObj)
                    {
                        product.Title = newProduct.Title;
                        product.Price = newProduct.Price;
                        product.Author = newProduct.Author;
                        product.Description = newProduct.Description;
                    }
                }
                else
                {
                    throw new ArgumentException($"Продукта с ID={productId} не существует!");
                }
            }
        }
        public void ClearCatalog()
        {
            lock (_productsSyncObj)
            {
                _products.Clear();
            }
        }
        private static List<Product> GenerateProducts(int count)
        {
            var random = new Random();
            var products = new Product[count];

            var productTitles = new string[]
            {
            "Доктор Живаго",
            "Война и мир",
            "Поющие в терновнике",
            "Над кукушкиным гнездом",
            "Мартин Иден",
            "Большие надежды",
            "Сто лет одиночества",
            "Убить пересмешника",
            "Отцы и дети",
            "Повелитель мух"
            };

            for (int i = 0; i < count; i++)
            {
                var name = productTitles[i];
                var price = random.Next(50, 500);

                products[i] = new Product(name, price)
                {
                    Id = Guid.NewGuid(),
                    Description = "Описание " + name,
                    Author = i.ToString()
                };
            }
            return products.ToList();
        }
    }
}
