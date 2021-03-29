using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_Pattern
{
   public class Runaway {
      public readonly Queue<Airplane> Landing = new();


      public int Id { get; set; }
      public double Length { get; set; }
      public short MaxQueueable { get; }


      public Runaway(int Id, double length) 
      {
         MaxQueueable = Convert.ToInt16((3200 / length) + 1);
         this.Id = Id; Length = length;
      }


      public async void Enqueue(Airplane p) {
         Landing.Enqueue(p);
         LandingLogger.LogEnqueuing(p, this);

         while (Landing.TryPeek(out var next)) {
            var current = await Check(next);
            if (p == current) break;
         }
      }

      public async Task<Airplane> Check(Airplane p) {

         if (p.LandingState == (LandingState)0) {
            var last = await Dequeue();
            return last;
         }

         else return new Aircraft();
      }

      private async Task<Airplane> Dequeue() {
         var next = Landing.Peek();
         LandingLogger.LogLanding(next, this);

         await next.Land(this);
         LandingLogger.LogWayFree(next, this);
         Landing.Dequeue();

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