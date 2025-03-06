using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeyikLoung.Entities
{
    public class AltKategori
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } // Bağlı olduğu kategori

        // ✅ Yeni eklenen ilişki
        public virtual ICollection<Product> Products { get; set; } = new List<Product>(); // Alt kategoriye bağlı ürünler
    }
}
