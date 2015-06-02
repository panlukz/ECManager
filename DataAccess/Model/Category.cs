using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Category
    {
        public Category()
        {

        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Product> Product { get; private set; }

    }
}
