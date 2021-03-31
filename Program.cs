using System;
using static System.Console;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_Pattern
{
   class Program
   {
      static void Main(string[] args)
      {
         Random rnd = new();
         ControlTower command = new(new List<Runaway>(){
            new Runaway(1200) { Id = 1 }, new Runaway(1500) { Id = 2 },
            new Runaway(900) { Id = 3 }, new Runaway(2100) { Id = 4 }
         });


         #region pupulate
         List<Airplane> planes = new();
         for (int i = 100; i < 115; i++)
         {
            if (rnd.Next() % 2 == 0) { 
               planes.Add(new Aircraft(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(700, 1800),
                  Id = i + 1
               });
            }
            else planes.Add(new Cargo(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(1150, 2099),
                  Id = i * 256
               });
         }
         #endregion
         
         var wait = new Task( () => command.WaitActiveLandings());
         wait.Start();

         foreach (var plane in planes) {
            Thread.Sleep(3000);
            plane.RequestLanding();
         }

         wait.Wait();
      }
   }
}
