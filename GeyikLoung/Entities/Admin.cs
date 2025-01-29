using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GeyikLoung.Entities
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        // Şifreyi doğrudan saklamak yerine, hashlenmiş ve tuzlanmış olarak saklayacağız.
        [Required]
        public string PasswordHash { get; set; }  // Hashlenmiş şifre
        public string PasswordSalt { get; set; }  // Tuz (salt)

        // Adminin rolünü veya diğer önemli bilgileri eklemek isterseniz:
        public string Role { get; set; }  // Örneğin, admin veya superadmin gibi roller


    }
}