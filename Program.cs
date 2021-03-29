using System;
using static System.Console;
using System.Collections.Generic;
using System.Threading;


namespace Mediator_Pattern
{
   class Program
   {
      static void Main(string[] args)
      {
         Random rnd = new();
         ControlTower command = new(new List<Runaway>(){
            new Runaway(1200), new Runaway(1500),
            new Runaway(900), new Runaway(2100)
         });


         #region pupulate
         List<Airplane> planes = new();
         for (int i = 0; i < 12; i++)
         {
            if (rnd.Next() % 2 == 0) { 
               planes.Add(new Aircraft(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(700, 1800),
                  Id = i * 1024 + 1
               });
            }
            else planes.Add(new Cargo(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(1150, 2099),
                  Id = i * 377 + 1
               });
         }
         #endregion
         
         foreach (var plane in planes) {
            Thread.Sleep(1000);
            plane.RequestLanding();
         }
      }
   }
}
