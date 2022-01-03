using System;
namespace Mediator_Pattern
{
   public interface IMediator {
      // void AcceptLandingRequest(Airplane colleague);
      void EnqueueLandingRequest(Airplane colleague);
      // void RejectLandingRequest(Airplane colleague);
   }

}




