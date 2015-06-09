using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        Product GetProductById(int productId);

        void UpdateProduct(Product newProduct);
        void AddProduct(Product product);

        void AddSupply(Supply supply);
        void DeleteSupply(Supply supply);

        void UpdateSupplier(int supplierId, Supplier newSupplier);
        void DeleteSupplier(Supplier supplier);
        void AddSupplier(Supplier editSupplier);
    }


}
