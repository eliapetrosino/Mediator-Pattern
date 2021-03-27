using Microsoft.EntityFrameworkCore;

namespace Mediator_Pattern
{
   public class ControlTowerDBContext : DbContext {

      public DbSet<Runaway> Runaways { get; set; }
      public DbSet<Airplane> Airplanes { get; set; }
      //? Also requests, confirms & landings just for tracking
      

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      { 
         optionsBuilder.UseSqlServer(
            "server=DESKTOP-96H9KLC; database=ControlTowerDB; trusted_connection=true;");
      }
   }
}