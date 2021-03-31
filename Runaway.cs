using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mediator_Pattern
{
   public class Runaway {
      public readonly Queue<Airplane> Landing = new();


      public int Id { get; set; }
      public double Length { get; set; }
      public short MaxQueueable { get; }


      public Runaway(double length) {
         MaxQueueable = Convert.ToInt16((3200 / length) + 1);
         Length = length;
      }


      public async void Enqueue(Airplane p) {
         Landing.Enqueue(p);
         LandingLogger.LogEnqueuing(p, this);

         while (Landing.TryPeek(out var current)) {
            if (current.LandingState == (LandingState)0) {

               var last = await Dequeue();
               if (p == last) break; //? *
            }
         }
      }

      private async Task<Airplane> Dequeue() {
         var next = Landing.Peek();
         LandingLogger.LogLanding(next, this);

         await next.Land(this);

      //?Logging here seems to solve color glitches
         Landing.Dequeue();
         LandingLogger.LogWayFree(next, this);
         return next;
      }

      public StringBuilder CurrentQueue() {
         var queue = new StringBuilder();

         queue.Append($"Gate #{Id} Length: {Length}m ");
         queue.Append("Current queue --------------");

         foreach(var p in Landing) 
            queue.Append("[" + p.Id + "]");

         return queue;
      }
   }
}



//* Prima di far eseguire tutto al Task lanciato da ControlTower l'if da solo provocava un
//* congelamento delle RequestLanding finché un aereo qualsiasi non fosse atterrato con successo.
//? Adding waiter-else solved that but were not disposing the ones cumulated since the runtime end. 
//? else { await ((int)this.Length / 200); continue; }