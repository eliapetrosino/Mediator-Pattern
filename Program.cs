using System;
using System.Linq;
using static System.Console;
using System.Collections.Generic;
using System.Threading;

//TODO: integrare un mini DB con E.Framework
namespace Mediator_Pattern
{

   public class ControlTower : IMediator {

      private readonly IEnumerable<Runaway> runaways; // set private
      public string AirportCity { get; set; } = "Helsinki";


      public ControlTower(IEnumerable<Runaway> gatesbase) {
         runaways = gatesbase;
      }

      public void EnqueueLandingRequest(Airplane p) {
         var here = runaways.FirstOrDefault(r => r.Length > p.LandingDistanceNeeded
               && r.Landing.Count < r.MaxQueueable);

         if (here != default) {
            here.Enqueue(p); //? EnqueueAsync() in base a .Priority
         }
         else 
         {
            Color.Foreground("red");
            p.LogDestination($"Airplane {p.ID} ask mayday");
         }
      }

   }

   




   class Program
   {
      static void Main(string[] args)
      {
         Random rnd = new Random();
         ControlTower command = new(new List<Runaway>(){
            new Runaway(1, 1200), new Runaway(2, 1900),
            new Runaway(3, 850), new Runaway(4, 2100)
         });

         #region pupulate
         List<Airplane> planes = new();
         for (int i = 0; i < 12; i++)
         {
            if (rnd.Next() % 2 == 0) { 
               planes.Add(new Aircraft(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(800, 1800),
                  ID = i * 24 + 1
               });
            }
            else planes.Add(new Cargo(command, (s) => { WriteLine(s); }) {
                  LandingDistanceNeeded = rnd.Next(1100, 2099),
                  ID = i * 48 + 1
               });
         }
         #endregion

         foreach (var plane in planes) {
            Thread.Sleep(2000);
            plane.RequestLanding();
         }
      }
   }
}
