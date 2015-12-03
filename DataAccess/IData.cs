using System.Collections.Generic;
using DataAccess.Model;

namespace DataAccess
{
    public interface IData
    {
        ICollection<Product> GetProductsFromSupplier(int supplierId);

        ICollection<Product> GetAllProducts();

        ICollection<Supplier> GetAllSuppliers();

        ICollection<Supply> GetAllSupplies();

        ICollection<Category> GetAllCategories();

        ICollection<Product> GetProducts(string name = "", string category = "", string producer = "",
            string supplier = "");

        ICollection<Producer> GetAllProducers();

        void AddCategory(Category category);

        void DeleteCategory(Category category);

        void UpdateCategory(Category category);

        Product GetProductById(int productId);

        void UpdateProduct(Product newProduct);
        void AddProduct(Product product);
        void DeleteProduct(Product product);

        void AddSupply(Supply supply);
        void DeleteSupply(Supply supply);

        void UpdateSupplier(int supplierId, Supplier newSupplier);
        void DeleteSupplier(Supplier supplier);
        void AddSupplier(Supplier editSupplier);
    }


}
