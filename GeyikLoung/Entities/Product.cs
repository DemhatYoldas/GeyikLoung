using System.ComponentModel.DataAnnotations;

namespace GeyikLoung.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Ürün adı zorunludur.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Fiyat zorunludur.")]
        [Range(0, double.MaxValue, ErrorMessage = "Fiyat pozitif bir değer olmalıdır.")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Kategori seçimi zorunludur.")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? AltKategoriId { get; set; }  // Alt kategori opsiyonel
        public virtual AltKategori AltKategori { get; set; }  // Alt kategori ilişkilendirme
    }
}
