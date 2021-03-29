using System;
using System.Threading.Tasks;


namespace Mediator_Pattern 
{
   public abstract class Airplane
   {
      protected readonly IMediator Mediator;
      public Action<string> LogMethod;


      public Airplane() { } // To allow fake return out Runaway.Check
      public Airplane(IMediator mediator, Action<string> wheretolog) {
         (Mediator, LogMethod) = (mediator, wheretolog);
      }


      public int Id { get; set; }
      public double LandingDistanceNeeded { get; set; }
      public LandingState LandingState { get; set; }
      //TODO Add properties to the constructor


      public void RequestLanding() {
         LandingState = LandingState.Waiting;
         LandingLogger.LogLandingRequest(this);
         Mediator.EnqueueLandingRequest(this); // The key of this pattern!
      }

      public async Task Land(Runaway whereTo) {
         this.LandingState = LandingState.Landing;
         var timeToLand = (int)whereTo.Length / 150;
         await timeToLand;
      }
   }



   public enum LandingState { Waiting = 0, Landing = 1 } //? Affinare




   #region Types

   public class Aircraft : Airplane {
      public Aircraft(IMediator mediator, Action<string> wheretolog) 
         : base(mediator, wheretolog) { }

      public Aircraft() { }
   }

   public class Cargo : Airplane {
      public Cargo(IMediator mediator, Action<string> wheretolog) 
         : base(mediator, wheretolog) { }
   }

   #endregion

}