using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Catalog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NameCatalog { get; set; }

        public int? ParentCatalogId { get; set; }

        public int Level { get; set; }

        public virtual Catalog ParentCatalog { get; set; }
        public virtual ICollection<Catalog> ChildCatalogs { get; set; }
    }
}
