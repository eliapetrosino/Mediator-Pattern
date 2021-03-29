using Microsoft.EntityFrameworkCore;

namespace Mediator_Pattern
{
   //* TODO: make sure Runaway.Enqueue receives Landing objects instead of airplane...
   //* in order to avoid ef errors & b.logic (airplanes do not belong to tower)

   public class ControlTowerDBContext : DbContext {

      public DbSet<Runaway> Runaways { get; set; }
      //? Also requests & confirms just for tracking
      

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         optionsBuilder.UseSqlServer(
            "server=DESKTOP-96H9KLC; database=MainControlTowerDB; trusted_connection=true;")
            .LogTo(System.Console.WriteLine);

         base.OnConfiguring(optionsBuilder);
      }
   }
}