using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rhino.Service.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public string Text { get; set; }

        public string Tags { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        [StringLength(500)]
        public string FileLocation { get; set; }

        public string FileContentType { get; set; }

        public decimal FileSize { get; set; }

        [StringLength(500)]
        public string ThumbnailName { get; set; }

        [StringLength(500)]
        public string ThumbnailLocation { get; set; }

        public string ThumbnailContentType { get; set; }

        public decimal ThumbnailSize { get; set; }

        public bool IsApproved { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserName { get; set; }
    }
}