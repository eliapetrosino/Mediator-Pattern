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
      //TODO find a way to finish up the remainings at the end of runtime

         Landing.Enqueue(p);
         LandingLogger.LogEnqueuing(p, this);
         
         while (Landing.TryPeek(out var next)) {
            Task<Airplane> _Dequeue = Dequeue();

            if (next.LandingState == (LandingState)0) {
               var last = await _Dequeue; //? Any difference
               LandingLogger.LogWayFree(last, this);
            }

            else { 
               await ((int)this.Length / 200); 
               continue;
            }
         }
      }

      public async Task<Airplane> Dequeue() {
         var next = Landing.Peek();
         LandingLogger.LogLanding(next, this);

         await next.Land(this);
         //? Random queue empty error
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