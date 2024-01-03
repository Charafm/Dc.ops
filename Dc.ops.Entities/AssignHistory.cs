using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dc.ops.Entities
{
    [Table("AssignHistorys")]
    public class AssignHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }


        public virtual Equipment Equipment { get; set; }

        public virtual AssignLocation AssignLocation { get; set; }


        public virtual User User { get; set; }


        public virtual DateTime Date { get; set; }
    }
}
