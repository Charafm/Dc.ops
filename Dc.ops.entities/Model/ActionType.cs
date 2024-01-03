using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.entities.Model
{
    [Table("ActionTypes")]
    public class ActionType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Title { get; set; }

        public virtual ICollection<EquipmentHistory> EquipmentHistories { get; set; }

       
    }
}
