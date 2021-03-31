using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Mediator_Pattern
{
   public class ControlTower : IMediator {

      private readonly IEnumerable<Runaway> runaways;
      private List<Task> tasks = new List<Task>();
      //? static ControlTowerDBContext db = new();


      public string AirportCity { get; set; } = "Helsinki";


      public ControlTower(IEnumerable<Runaway> gatesbase) {
         runaways = gatesbase;
      }


      public void EnqueueLandingRequest(Airplane p) {
         if (tasks.Count() > 5) {

            var deleted = ClearTaskList();
            Color.Foreground("green");
            Console.WriteLine($"Cleaned {deleted} junk tasks");
         }

         var landing = new Task(() => MatchGate(p).Enqueue(p));
         //? TODO decidere se a sto punto LandingState è ancora utile
         tasks.Add(landing); landing.Start();
      }

      private Runaway MatchGate(Airplane p) {
         var matches = runaways.Where(r => r.Length > p.LandingDistanceNeeded);
         int min = matches.Min(r => r.Landing.Count);
         return matches.First(r => r.Landing.Count == min) ?? matches.First();
      }

      public void WaitActiveLandings() { //? Not working
         foreach (var t in tasks)
            if (t.Status == TaskStatus.Running
                  || t.Status == TaskStatus.WaitingToRun)
            { 
               t.Wait();
            }
      }

      private int ClearTaskList() {
         return tasks.RemoveAll(t => t.Status == TaskStatus.RanToCompletion);
      }
   }
}