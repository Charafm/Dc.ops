using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.Entities
{
    [Table("AssignLocations")]
    public class AssignLocation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public virtual string Title { get; set; }

        [MaxLength(255)]
        public virtual string Description { get; set; }

        public virtual ICollection<AssignHistory> AssignHistories { get; set; }

        
    }
}
