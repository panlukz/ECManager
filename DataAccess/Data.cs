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
