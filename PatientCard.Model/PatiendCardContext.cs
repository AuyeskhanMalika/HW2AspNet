using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientCard.Model
{
    public class PatiendCardContext : DbContext
    {
        public PatiendCardContext()
            :base("name=PatientDB")
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(x => x.HasColumnType("datetime2"));
        }
    }
}
