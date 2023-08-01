using Microsoft.AspNetCore.Components;

namespace OnlineStore.Components
{
    public partial class ProductItem
    {
        [Parameter]
        public Product Product { get; set; }
        [Inject]
        public NavigationManager Manager { get; set; }

        public void ToProductInfoPage()
        {
            Manager.NavigateTo($"products/{Product.Id}");
        }
    }
}