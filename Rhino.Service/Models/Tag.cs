using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rhino.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tags")]
    public partial class Tag
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Tree { get; set; }

        [StringLength(100)]
        public string Branch { get; set; }

        public string Nodes { get; set; }
    }
}
