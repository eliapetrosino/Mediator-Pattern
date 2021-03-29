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
         Random rnd = new Random();
         ControlTower command = new(new List<Runaway>(){
            new Runaway(1, 1200), new Runaway(2, 1500),
            new Runaway(3, 900), new Runaway(4, 2100)
         });

         #region pupulate
         List<Airplane> planes = new();
         for (int i = 0; i < 12; i++)
         {
            if (rnd.Next() % 2 == 0) { 
               planes.Add(new Aircraft(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(700, 1800),
                  Id = i * 24 + 1
               });
            }
            else planes.Add(new Cargo(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(1150, 2099),
                  Id = i * 37 + 1
               });
         }
         #endregion

         foreach (var plane in planes) {
            Thread.Sleep(5000);
            plane.RequestLanding();
         }

         //? Finisce di smaltire le code
      }
   }
}
