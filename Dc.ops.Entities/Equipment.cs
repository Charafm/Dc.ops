using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.Entities
{
    [Table("Equipments")]
    public class Equipment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        public virtual string Title { get; set; }

        public virtual EquipmentType Type { get; set; }

        [Required]
        public virtual int Quantity { get; set; }

        public virtual string Description { get; set; }

        public virtual ICollection<EquipmentHistory> Histories { get; set; }

        public virtual ICollection<AssignHistory> AssignHistories { get; set; }

    }
}