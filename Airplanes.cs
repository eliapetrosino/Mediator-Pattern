using System;
using System.Threading.Tasks;


namespace Mediator_Pattern 
{
   public abstract class Airplane
   {
      protected readonly IMediator Mediator;
      public Action<string> LogCamaleon;

      public Airplane(IMediator mediator, Action<string> wheretolog) {
         (Mediator, LogCamaleon) = (mediator, wheretolog);
      }


      public double LandingDistanceNeeded { get; set; }
      public int ID { get; set; }
      //TODO Add properties to the constructor


      public void RequestLanding() {
         LandingLogger.LogLandingRequest(this);
         Mediator.EnqueueLandingRequest(this); // The key of this pattern
      }

      public async Task<string> Land(Runaway whereTo) {
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