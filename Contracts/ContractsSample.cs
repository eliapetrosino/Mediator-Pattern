namespace Mediator_Sample
{
   #region GoodForEveryProject
   public interface IMediator {
      void ProcessRequest(Colleague colleague);
      void ExecuteRequest(Colleague colleague);
   }


   public abstract class Colleague {
      protected readonly IMediator Mediator;
      public Colleague(IMediator mediator) => Mediator = mediator;
      #endregion
   }
}