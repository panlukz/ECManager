using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using DataAccess;
using DataAccess.Model;
using MahApps.Metro.Controls.Dialogs;

namespace ecman.ViewModels
{
    public class SupplyTabViewModel : TabViewModelBase
    {
        private IData dataContext;
        private Supplier selectedSupplier;
        private Product selectedProduct;
        private int productQuantity = 1;
        private Product selectedProductInSupply;
        private Supply selectedSupply;
        private BindableCollection<Supply> supplies;
        private Supplier editSupplier;
        private BindableCollection<Supplier> suppliers;



        public SupplyTabViewModel(IData data)
        {
            this.dataContext = data;
            this.DisplayName = "Dostawy";

            this.Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());

            if(Suppliers.Count > 0)
                EditSupplier = Suppliers[0];

            this.Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
            this.ProductsInSupply = new BindableCollection<Product>();

            this.suppliesListView = new  ListCollectionView(Supplies);
        }

       

        

        public BindableCollection<Supplier> Suppliers
        {
            get { return suppliers; }
            set
            {
                suppliers = value;
                NotifyOfPropertyChange(() => Suppliers);
            }
        }
        

        public BindableCollection<Supply> Supplies
        {
            get { return supplies; }
            private set
            {
                supplies = value;
                NotifyOfPropertyChange(() => Supplies);
                this.suppliesListView = new ListCollectionView(Supplies);
                NotifyOfPropertyChange(() => SuppliesListView);
            }
        }


        private ListCollectionView productListView;
        public ICollectionView ProductListView
        {
            get { return this.productListView; }
        }

        private string searchProductName;

        public string SearchProductName
        {
            get { return searchProductName; }
            set
            {
                searchProductName = value;
                NotifyOfPropertyChange(() => SearchProductName);
                
                if (String.IsNullOrEmpty(value))
                    ProductListView.Filter = null;
                else
                    ProductListView.Filter = new Predicate<object>(p => ((Product)p).Name.ToLower().Contains(value.ToLower()));
        
            }
        }

        private ListCollectionView suppliesListView;
        public ICollectionView SuppliesListView
        {
            get { return this.suppliesListView; }
        }


        private DateTime searchSupplyDate1 = DateTime.Today;

        public DateTime SearchSupplyDate1
        {
            get { return searchSupplyDate1; }
            set
            {
                searchSupplyDate1 = value;
                NotifyOfPropertyChange(() => SearchSupplyDate1);

                SuppliesListView.Filter = new Predicate<object>(s => ((Supply)s).DeliveryDate >= SearchSupplyDate1 && ((Supply)s).DeliveryDate <= SearchSupplyDate2);
            }
        }

        private DateTime searchSupplyDate2 = DateTime.Today;

        public DateTime SearchSupplyDate2
        {
            get { return searchSupplyDate2; }
            set
            {
                searchSupplyDate2 = value;
                NotifyOfPropertyChange(() => SearchSupplyDate2);

                SuppliesListView.Filter = new Predicate<object>(s => ((Supply)s).DeliveryDate >= SearchSupplyDate1 && ((Supply)s).DeliveryDate <= SearchSupplyDate2);

            }
        }
        

        

        public BindableCollection<Product> ProductsFromSelectedSupplier { get; private set; }

        public BindableCollection<Product> ProductsInSupply { get; private set; }

        public Supplier SelectedSupplier
        {
            get { return selectedSupplier; }
            set
            {
                selectedSupplier = value;
                this.ProductsFromSelectedSupplier = 
                    new BindableCollection<Product>(dataContext.GetProductsFromSupplier(selectedSupplier.Id));
                NotifyOfPropertyChange(() => SelectedSupplier);
                NotifyOfPropertyChange(() => ProductsFromSelectedSupplier);

                this.productListView = new ListCollectionView(ProductsFromSelectedSupplier);
                NotifyOfPropertyChange(() => ProductListView);

                if (ProductsInSupply.Count > 0)
                {
                    ProductsInSupply.Clear();
                    NotifyOfPropertyChange(() => ProductsInSupply);
                }
            }
        }

        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set
            {
                selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }

        public Product SelectedProductInSupply
        {
            get { return selectedProductInSupply; }
            set
            {
                selectedProductInSupply = value;
                NotifyOfPropertyChange(() => SelectedProductInSupply);
            }
        }


        public Supply SelectedSupply
        {
            get { return selectedSupply; }
            set
            {
                selectedSupply = value;
                NotifyOfPropertyChange(() => SelectedSupply);
            }
        }


        public Supplier EditSupplier
        {
            get { return editSupplier; }
            set
            {
                editSupplier = value;
                NotifyOfPropertyChange(() => EditSupplier);
            }
        }
        
        


        public int ProductQuantity
        {
            get { return productQuantity; }
            set
            {
                productQuantity = value;
                NotifyOfPropertyChange(() => ProductQuantity);
            }
        }


        public decimal SupplyCost
        {
            get
            {
                decimal cost = ProductsInSupply.Sum(t => (t.Price * t.Quantity) );
                return cost;
            }

        }

        public DateTime SupplyDate
        {
            get { return DateTime.Today; }
        }



        public int NumberOfProductsInSupply
        {
            get { return ProductsInSupply.Count; }
        }


        public void AddProductToSupply()
        {
            if (SelectedProduct != null && !ProductsInSupply.Contains(SelectedProduct))
            {
                SelectedProduct.Quantity = ProductQuantity;
                ProductsInSupply.Add(SelectedProduct);
                ProductQuantity = 1;
                NotifyOfPropertyChange(() => ProductsInSupply);
                NotifyOfPropertyChange(() => NumberOfProductsInSupply);
                NotifyOfPropertyChange(() => SupplyCost);
            }
        }

        public void RemoveProductFromSupply()
        {
            if (SelectedProductInSupply != null)
            {
                ProductsInSupply.Remove(SelectedProductInSupply);
                NotifyOfPropertyChange(() => NumberOfProductsInSupply);
                NotifyOfPropertyChange(() => SupplyCost);

            }
        }

        public void AddSupply()
        {

            if (ProductsInSupply.Count > 0)
            {
                var newSupply = new Supply() { SupplierId = SelectedSupplier.Id, DeliveryDate = DateTime.Today };

                foreach (Product p in ProductsInSupply)
                {
                    newSupply.Product.Add(p);
                }

                dataContext.AddSupply(newSupply);
                Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
                //dirty hack to refresh supply list filtered by selected date
                SearchSupplyDate1 = SearchSupplyDate1;
                SearchSupplyDate2 = SearchSupplyDate2;

                DialogService.ShowMessage("Dodanie dostawy od odstawcy " + newSupply.Supplier.Name + " zakończono sukcesem!",
                                "Dodano dostawę", MessageDialogStyle.Affirmative);

                ProductsInSupply.Clear();
            }
            else
            {
                DialogService.ShowMessage("Nie można dodać dostawy bez produktów. Dodaj produkt do dostawy!",
                                "Ostrzeżenie", MessageDialogStyle.Affirmative);
            }

            
            

        }

        public async void DeleteSupply()
        {
            if (SelectedSupply != null)
            {

                MessageDialogResult result =
                    await
                        DialogService.ShowMessage(
                            "Czy jesteś pewny że chcesz usunąć dostawę od dostawcy " + SelectedSupply.Supplier.Name +
                            " z dnia " + SelectedSupply.DeliveryDate + "? Cofnięcie zmian nie będzie możliwe!",
                            "Usuwanie dostawy", MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {


                    dataContext.DeleteSupply(SelectedSupply);
                    Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
                    //dirty hack to refresh supply list filtered by selected date
                    SearchSupplyDate1 = SearchSupplyDate1;
                    SearchSupplyDate2 = SearchSupplyDate2;
                }


            }
        }

        public async void DeleteSupplier()
        {
            if (EditSupplier != null)
            {
                MessageDialogResult result = await DialogService.ShowMessage("Czy jesteś pewny że chcesz usunąć wybranego dostawcę: " + EditSupplier.Name + "?\nPostepuj rozważnie, gdyż cofnięcie zmian nie będzie możliwe!\n\nJeżeli wybrany dostawca ma powiązania w bazie danych jego usunięcie nie będzie możliwe.", "Usuwanie " + EditSupplier.Name, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative && EditSupplier.Product.Count < 1)
                {
                    try
                    {
                        dataContext.DeleteSupplier(EditSupplier);
                        Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
                    }
                    catch (Exception e)
                    {
                        DialogService.ShowMessage("Usunięcie dostawcy niemożliwe! Dostawca posiada wpisy w produktach",
                            "Usuwanie - błąd", MessageDialogStyle.Affirmative);
                    }
                    
                }
                else
                {
                    DialogService.ShowMessage("Usunięcie dostawcy niemożliwe! Dostawca posiada wpisy w produktach",
                            "Usuwanie - błąd", MessageDialogStyle.Affirmative);
                }
                
            }
        }

        public void UpdateSupplier()
        {
            if (EditSupplier != null && EditSupplier.Id != 0 && !String.IsNullOrWhiteSpace(EditSupplier.Name))
            {
                dataContext.UpdateSupplier(EditSupplier.Id, EditSupplier);
                Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            }
            else if (EditSupplier != null && EditSupplier.Id == 0 && !String.IsNullOrWhiteSpace(EditSupplier.Name))
            {
                dataContext.AddSupplier(EditSupplier);
                Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            }
            else
            {
                DialogService.ShowMessage("Wpisz przynajmniej nazwę dostawcy!",
                            "Błąd", MessageDialogStyle.Affirmative);
            }
        }

        public void AddNewSupplier()
        {
            EditSupplier = new Supplier();

            

        }
    }
}