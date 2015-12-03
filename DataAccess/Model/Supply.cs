using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;

namespace DataAccess.Model
{
    public class Supply
    {

        public Supply()
        {
            Product = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<Product> Product { get; set; }

    }
}