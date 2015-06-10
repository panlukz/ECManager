using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Model;

namespace DataAccess 
{
    public class Data : IData
    {
        private readonly Context context;

        public Data()
        {
            this.context = new Context();
        }

        public ICollection<Product> GetProductsFromSupplier(int supplierId)
        {
            return context.Products.Where(p => p.SupplierId == supplierId).ToArray();
        }

        public ICollection<Product> GetAllProducts()
        {
            return context.Products.ToArray();
        }

        public ICollection<Supplier> GetAllSuppliers()
        {
            return context.Suppliers.ToArray();
        }

        public ICollection<Supply> GetAllSupplies()
        {
            return context.Supplies.ToList();
        }

        public ICollection<Category> GetAllCategories()
        {
            return context.Categories.ToList();
        }

        public ICollection<Producer> GetAllProducers()
        {
            return context.Producers.ToList();
        }

        public ICollection<Product> GetProducts(string name = "", string category = "", string producer = "", string supplier = "")
        {

            var productsQuery = context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
            {
                productsQuery = productsQuery.Where(p => p.Name.ToLower().Contains(name.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                productsQuery = productsQuery.Where(p => p.Category.Name.ToLower().Contains(category.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(producer))
            {
                productsQuery = productsQuery.Where(p => p.Producer.Name.ToLower().Contains(producer.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(supplier))
            {
                productsQuery = productsQuery.Where(p => p.Supplier.Name.ToLower().Contains(supplier.ToLower()));
            }

            return productsQuery.ToList();

        }

        public Product GetProductById(int productId)
        {
            return context.Products.FirstOrDefault(p => p.Id == productId);
        }

        public void UpdateProduct(Product newProduct)
        {
            Product oldProduct = context.Products.Single(p => p.Id == newProduct.Id);

            oldProduct.Name = newProduct.Name;
            oldProduct.Price = newProduct.Price;
            oldProduct.CategoryId = newProduct.CategoryId;
            oldProduct.Description = newProduct.Description;
            oldProduct.ProducerId = newProduct.ProducerId;
            oldProduct.SupplierId = newProduct.SupplierId;

            context.SaveChanges();

        }

        public void AddCategory(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            Category oldCategory = context.Categories.Single(c => c.Id == category.Id);

            oldCategory.Name = category.Name;

            context.SaveChanges();
        }

        public void AddProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();
        }


        public void AddSupply(Supply supply)
        {
            context.Supplies.Add(supply);
            context.SaveChanges();
        }

        public void DeleteSupply(Supply supply)
        {
            context.Supplies.Remove(supply);
            context.SaveChanges();
        }

        public void UpdateSupplier(int supplierId, Supplier newSupplier)
        {
            Supplier oldSupplier = context.Suppliers.Single(s => s.Id == supplierId);

            oldSupplier.Name = newSupplier.Name;
            oldSupplier.PhoneNo = newSupplier.PhoneNo;
            oldSupplier.Address = newSupplier.Address;
            oldSupplier.Email = newSupplier.Email;

            context.SaveChanges();
        }

        public void DeleteSupplier(Supplier supplier)
        {
            context.Suppliers.Remove(supplier);

            context.SaveChanges();
        }

        public void AddSupplier(Supplier editSupplier)
        {
            context.Suppliers.Add(editSupplier);

            context.SaveChanges();
        }
    }
}
