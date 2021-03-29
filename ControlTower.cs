using System.Collections.Generic;
using System.Linq;
using System;

namespace Mediator_Pattern
{
   public class ControlTower : IMediator {

      private readonly IEnumerable<Runaway> runaways;
      public string AirportCity { get; set; } = "Helsinki";


      public ControlTower(IEnumerable<Runaway> gatesbase) {
         runaways = gatesbase;
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


      // public async void Dispose() {
      //    foreach (var r in runaways) {
      //       while(r.Landing.TryPeek(out var _)) await r.Dequeue();
      //    }
   }
}