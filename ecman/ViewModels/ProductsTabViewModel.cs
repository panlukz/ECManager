using DataAccess;

namespace ecman.ViewModels
{
    public class ProductsTabViewModel : TabViewModelBase
    {
        private IData dataContext;

        public ProductsTabViewModel(IData data)
        {
            this.dataContext = data;
            this.DisplayName = "Produkty";
        }
    }
}