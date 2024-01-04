using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.Entities

{
    [Table("EquipmentHistorys")]
    public class EquipmentHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }


        public virtual Equipment Equipment { get; set; }


        public virtual string Action { get; set; }

        [Required]
        public virtual User User { get; set; }


        public virtual DateTime Date { get; set; }

        [MaxLength(255)]
        public virtual string Notes { get; set; }
    }
}
