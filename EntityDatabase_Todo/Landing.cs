using System;

namespace Mediator_Pattern
{
   public class Landing
   {
      public int RunawayId { get; set; }
      public Runaway Runaway { get; set; }

      public int Airplane { get; set; }
      public int LandingTiming { get; set; }

      public DateTime Date { get; set; }

   }
}