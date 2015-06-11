using System;
using System.Windows.Media;
using Caliburn.Micro;
using DataAccess;
using DataAccess.Model;
using MahApps.Metro.Controls.Dialogs;

namespace ecman.ViewModels
{
    public class ProductsTabViewModel : TabViewModelBase
    {
        private IData dataContext;
        private Category editCategory;
        private Product editProduct;
        private int editProductId;
        private BindableCollection<Category> categories;



        public ProductsTabViewModel(IData data)
        {
            this.dataContext = data;
            this.DisplayName = "Produkty";

            this.Categories = new BindableCollection<Category>(dataContext.GetAllCategories());
            this.Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
            this.Producers = new BindableCollection<Producer>(dataContext.GetAllProducers());
        }



        public BindableCollection<Category> Categories
        {
            get { return categories; }
            set
            {
                categories = value;
                NotifyOfPropertyChange(()=> Categories);
            }
        }
        

        public BindableCollection<Producer> Producers { get; set; }

        public BindableCollection<Supplier> Suppliers { get; set; }

        public BindableCollection<Product> ProductsFromQuery { get; set; }

        public string QueryProductName { get; set; }
        public string QueryProductCategory { get; set; }
        public string QueryProductProducer { get; set; }
        public string QueryProductSupplier { get; set; }

        private Product selectedProductFromQueryProduct;

        public Product SelectedProductFromQuery
        {
            get { return selectedProductFromQueryProduct; }
            set
            {
                selectedProductFromQueryProduct = value;
                NotifyOfPropertyChange(()=> SelectedProductFromQuery);
            }
        }
        

        public Category EditCategory
        {
            get { return editCategory; }
            set
            {
                editCategory = value;
                NotifyOfPropertyChange(() => EditCategory);
            }
        }


        public int EditProductId
        {
            get { return editProductId; }
            set
            {
                editProductId = value;
                NotifyOfPropertyChange( () => EditProductId);
            }
        }
        
        

        public Product EditProduct
        {
            get { return editProduct; }
            set
            {
                editProduct = value;
                NotifyOfPropertyChange( () => EditProduct);
            }
        }


        public void SearchProducts()
        {
            ProductsFromQuery = new BindableCollection<Product>(dataContext.GetProducts(QueryProductName, QueryProductCategory, QueryProductProducer, QueryProductSupplier));
            NotifyOfPropertyChange(() => ProductsFromQuery);
        }

        public void GetProductById()
        {
            if (EditProductId != 0)
            {
                EditProduct = dataContext.GetProductById(EditProductId);
            }
        }

        public void AddProduct()
        {
            EditProduct = new Product();
            
        }

        public async void DeleteProduct()
        {
            if (EditProduct != null)
            {
                MessageDialogResult result = await DialogService.ShowMessage("Czy jesteś pewny że chcesz usunąć wybrany produkt: " + EditProduct.Name + "?\nPostepuj rozważnie, gdyż cofnięcie zmian nie będzie możliwe!\n\nJeżeli wybrany produkt ma powiązania w bazie danych jego usunięcie nie będzie możliwe.", "Usuwanie " + EditProduct.Name, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    try
                    {
                        dataContext.DeleteProduct(EditProduct);
                        Suppliers = new BindableCollection<Supplier>(dataContext.GetAllSuppliers());
                        EditProduct = new Product();
                    }
                    catch (Exception e)
                    {
                        DialogService.ShowMessage("Usunięcie produktu niemożliwe! Produkt ma powiązania w bazie danych!",
                            "Usuwanie - błąd", MessageDialogStyle.Affirmative);
                    }

                }

            }
        }

        public void UpdateProduct()
        {
            if (EditProduct != null && EditProduct.Id != 0)
            {
                dataContext.UpdateProduct(EditProduct);
            }
            else if (EditProduct != null && EditProduct.Id == 0)
            {
                dataContext.AddProduct(EditProduct);
            }

            Categories = new BindableCollection<Category>(dataContext.GetAllCategories());
        }

        public void AddNewCategory()
        {
            EditCategory = new Category();
        }

        public async void DeleteCategory()
        {
            if (EditCategory != null)
            {
                MessageDialogResult result = await DialogService.ShowMessage("Czy jesteś pewny że chcesz usunąć wybraną kategorię: " + EditCategory.Name + "?\nPostepuj rozważnie, gdyż cofnięcie zmian nie będzie możliwe!\n\nJeżeli wybrana kategoria ma powiązania w bazie danych jego usunięcie nie będzie możliwe.", "Usuwanie " + EditCategory.Name, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    try
                    {
                        dataContext.DeleteCategory(EditCategory);
                        Categories = new BindableCollection<Category>(dataContext.GetAllCategories());
                        EditCategory = new Category();
                    }
                    catch (Exception e)
                    {
                        DialogService.ShowMessage("Usunięcie kategorii niemożliwe! Kategoria ma powiązania w bazie danych!",
                            "Usuwanie - błąd", MessageDialogStyle.Affirmative);
                    }

                }

            }
        }

        public void UpdateCategory()
        {
            if (EditCategory != null && EditCategory.Id != 0)
            {
                dataContext.UpdateCategory(EditCategory);
                this.Categories = new BindableCollection<Category>(dataContext.GetAllCategories());
                NotifyOfPropertyChange(() => Categories);
            }
            else if (EditCategory != null && EditCategory.Id == 0)
            {
                dataContext.AddCategory(EditCategory);
                this.Categories = new BindableCollection<Category>(dataContext.GetAllCategories());
                NotifyOfPropertyChange(() => Categories);

            }
        }
    }
}