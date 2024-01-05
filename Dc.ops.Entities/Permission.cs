using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dc.ops.Entities
{
    
        [Table("Permissions")]
        public class Permission
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public virtual Guid Id { get; set; }

            [Required]
            [MaxLength(50)]
            public virtual string Name { get; set; }

            public virtual string Description { get; set; }

            public virtual ICollection<UserRole> Roles { get; set; }
        }

    
}
