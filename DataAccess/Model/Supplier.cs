using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNo { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public virtual ICollection<Product> Product { get; private set; }
    }
}
