using System;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Data;
using Caliburn.Micro;
using DataAccess;
using DataAccess.Model;

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



        public SupplyTabViewModel(IData data)
        {
            this.dataContext = data;
            this.DisplayName = "Dostawy";

            this.Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            this.Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
            this.ProductsInSupply = new BindableCollection<Product>();

            this.suppliesListView = new  ListCollectionView(Supplies);
        }

        public BindableCollection<Supplier> Suppliers { get; private set; }

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
            var newSupply = new Supply() { SupplierId = SelectedSupplier.Id, DeliveryDate = DateTime.Today};

            foreach (Product p in ProductsInSupply)
            {
                newSupply.Product.Add(p);
            }

            dataContext.AddSupply(newSupply);
            Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
            
            
        }

        public void DeleteSupply()
        {
            if (SelectedSupply != null)
            {
                dataContext.DeleteSupply(SelectedSupply);
                Supplies = new BindableCollection<Supply>(dataContext.GetAllSupplies());
            }
            
        }

        public void DeleteSupplier()
        {
            if (EditSupplier != null)
            {
                dataContext.DeleteSupplier(EditSupplier);
                Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            }
        }

        public void UpdateSupplier()
        {
            if (EditSupplier != null && EditSupplier.Id != 0)
            {
                dataContext.UpdateSupplier(EditSupplier.Id, EditSupplier);
                Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            }
            else if (EditSupplier != null && EditSupplier.Id == 0)
            {
                dataContext.AddSupplier(EditSupplier);
                Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            }
        }

        public void AddNewSupplier()
        {
            var newSupplier = new Supplier();

            Suppliers.Add(newSupplier);

            EditSupplier = newSupplier;

        }
    }
}