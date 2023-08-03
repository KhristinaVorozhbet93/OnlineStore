using Microsoft.AspNetCore.Components;

namespace OnlineStore.Pages
{
    public partial class CatalogPage
    {
        [Inject]
        public ICatalog Catalog { get; set; }
        private List<Product> _products;

        protected override void OnInitialized()
        {
            _products = Catalog.GetProducts();
        }
    }
}