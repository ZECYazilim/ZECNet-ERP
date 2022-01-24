using AbcYazilim.OgrenciTakip.Data.OgrenciTakipMigration;
using AbcYazilim.OgrenciTakip.Model.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace AbcYazilim.OgrenciTakip.Data.Contexts
{
    public class OgrenciTakipContext : BaseDbContext<OgrenciTakipContext,Configuration>
    {       
        public OgrenciTakipContext()
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public OgrenciTakipContext(string connectionString) : base(connectionString)
        {
            Configuration.LazyLoadingEnabled = false; //performans iyile�tirmesi i�in kullan�ld�.
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); //Tablolar�n sonuna s eki eklemesi engellendi.
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();//Bir e �ok ili�kili tablolarda bir veri silindi�i zaman di�er taraftan silinmesi engellendi.
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>(); //�ok a a �ok i�in ayn� i�lem yapld�.
        }
        public DbSet<Il> Il { get; set; }
        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<Okul> Okul { get; set; }
    }

}