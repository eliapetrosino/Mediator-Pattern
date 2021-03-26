using System;
using System.Threading.Tasks;


namespace Mediator_Pattern 
{
   public abstract class Airplane
   {
      protected readonly IMediator Mediator;
      public Action<string> LogDestination;
      public Airplane(IMediator mediator, Action<string> wheretolog) {
         (Mediator, LogDestination) = (mediator, wheretolog);
      }


      public double LandingDistanceNeeded { get; set; }
      public int ID { get; set; }


      public void RequestLanding() {
         Color.Foreground("grey");
         LogDestination($"\n* {this.GetTypeShort()} ID: {ID} ask for landing *");
         Mediator.EnqueueLandingRequest(this); // The key of this pattern

         
      }

      public async Task<string> Land(Runaway whereTo) {

         Color.Foreground("blue");
         LogDestination($"{this.GetTypeShort()} ID: {ID} Landing to gate {whereTo.ID}");

         var timeToLand = (int)whereTo.Length / 250;
         await timeToLand;

         return $"Gate {whereTo.ID} free\n";
      }
   } 



   #region Types
   
   public class Aircraft : Airplane {
      public Aircraft(IMediator mediator, Action<string> wheretolog) 
         : base(mediator, wheretolog) { }
   }

   public class Cargo : Airplane {
      public Cargo(IMediator mediator, Action<string> wheretolog) 
         : base(mediator, wheretolog) { }
   }

   #endregion

}