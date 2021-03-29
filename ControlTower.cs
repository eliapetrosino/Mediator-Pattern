using System.Collections.Generic;
using System.Linq;
using System;

namespace Mediator_Pattern
{
   public class ControlTower : IMediator {

      public readonly IEnumerable<Runaway> runaways;
      //? static ControlTowerDBContext db = new();


      public string AirportCity { get; set; } = "Helsinki";


      public ControlTower(IEnumerable<Runaway> gatesbase) {
         runaways = gatesbase;

         //? Access denied to DB
         //foreach(var r in runaways) db.Runaways.Add(r); db.SaveChanges();
      }


      public async void EnqueueLandingRequest(Airplane p) {
         while (MatchGate(p) == null) await 1500;
         MatchGate(p).Enqueue(p);
      }
      private Runaway MatchGate(Airplane p) {
         var matches = runaways.Where(r => r.Length > p.LandingDistanceNeeded);
         int min = matches.Min(r => r.Landing.Count);
         return matches.First(r => r.Landing.Count == min);
      }
   }
}