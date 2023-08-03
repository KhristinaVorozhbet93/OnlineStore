using Microsoft.AspNetCore.Components;

namespace OnlineStore.Pages 
{
    public partial class ProductInfoPage
    {
        [Inject]
        public ICatalog Catalog { get; set; }
        [Parameter]
        public Guid ProductId { get; set; }

        private Product _product;
        protected override void OnInitialized()
        {
            _product = Catalog.GetProduct(ProductId);
        }
    }
}