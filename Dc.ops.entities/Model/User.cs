using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.entities.Model
{
    [Table("Users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual string Username { get; set; }

        [Required]
        // Store a hashed password, not plain text!
        public virtual string PasswordHash { get; set; }

        public virtual UserRole Role { get; set; }

        public virtual ICollection<EquipmentHistory> EquipmentHistories { get; set; }
    }
}
