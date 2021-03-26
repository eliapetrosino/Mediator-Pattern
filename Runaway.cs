using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mediator_Pattern
{
   public class Runaway {
      public readonly Queue<Airplane> Landing = new();


      public int ID { get; init; }
      public double Length { get; init; }
      public short MaxQueueable { get; }


      public Runaway(int id, double length) 
      {
         MaxQueueable = Convert.ToInt16((3200 / length) + 1);
         ID = id; Length = length;
      }


      public async void Enqueue(Airplane p) {

         Landing.Enqueue(p);

         Color.Foreground("dark");
         p.LogDestination($"Request enqueued to gate {ID} - Airplane ID{p.ID}\n{CurrentQueue()}");

         if (Landing.TryPeek(out var _)) await Flush(p.LogDestination);
         else await Dequeue(p);
      }

      public async Task Dequeue(Airplane p) { 
         var confirm = await p.Land(this);
         Landing.TryDequeue(out var _);

         Color.Foreground("green"); 
         p.LogDestination(confirm);
      }

      public async Task Flush(Action<string> LogDestination)
      {
         while (Landing.TryPeek(out var next)) {
            await Dequeue(next);
         }
      }

      public StringBuilder CurrentQueue() {
         var queue = new StringBuilder();

         queue.Append($"Gate #{ID} Length: {Length}m ");
         queue.Append("Current queue --------------");

         foreach(var p in Landing) 
            queue.Append("[" + p.ID + "]");

         return queue;
      }
   }
}


