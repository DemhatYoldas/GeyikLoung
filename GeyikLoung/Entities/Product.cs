using System.ComponentModel.DataAnnotations;

namespace GeyikLoung.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? AltKategoriId { get; set; }  // Alt kategori opsiyonel
        public virtual AltKategori AltKategori { get; set; }  // Alt kategori ilişkilendirme
    }
}
