using Microsoft.AspNetCore.Components;

namespace OnlineStore.Pages
{
    public partial class CatalogPage
    {
        [Inject]
        public IStoreClient StoreClient { get; set; }

        private List<Product> _products;

        protected override async Task OnInitializedAsync()
        {
            _products = await StoreClient.GetProducts();
        }
    }
}