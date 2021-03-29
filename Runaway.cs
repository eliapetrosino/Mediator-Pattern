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

         while (Landing.TryPeek(out var next)) {
            var current = await Check(next);
            
         //? Not storing nexts till the current's landing
            if (p == current) break; //? * See bottom page
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

      //?Logging here seems to solve color glitches
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




//? Adding waiter-else solve that but not disposing the ones cumulated since end
//? else { await ((int)this.Length / 200); continue; }