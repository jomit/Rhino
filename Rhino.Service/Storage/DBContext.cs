namespace Rhino.Service.Storage
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Rhino.Service.Models;

    public partial class ISDB : DbContext
    {
        public ISDB()
            : base("name=isdb")
        {
            // Turn off the Migrations, (NOT a code first Db)
            Database.SetInitializer<ISDB>(null);
        }

        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
