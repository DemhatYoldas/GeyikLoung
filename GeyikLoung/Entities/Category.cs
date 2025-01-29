using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeyikLoung.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<AltKategori> AltKategoriler { get; set; } = new List<AltKategori>();  // Alt kategoriler
    }
}
