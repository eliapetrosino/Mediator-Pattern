namespace Mediator_Pattern
{
   public static class LandingLogger {
   //? Come parametro di Runaway e conseguente Mediator anche nel suo costruttore
      public static void LogLandingRequest(Airplane p) { 
         Color.Foreground("gray");
         p.LogMethod($"\n* {p.GetTypeShort()} ID: {p.Id} ask for landing *");
      }
      public static void LogEnqueuing(Airplane p, Runaway r) {
         Color.Foreground("dark");
         p.LogMethod($"Request enqueued to gate {r.Id} - Airplane ID{p.Id}\n{r.CurrentQueue()}");
      }
      public static void LogLanding(Airplane p, Runaway r) {
         Color.Foreground("blue");
         p.LogMethod($"{p.GetTypeShort()} ID: {p.Id} Landing to gate {r.Id}");
      }
      public static void LogWayFree(Airplane p, Runaway r) { 
         Color.Foreground("green");
         p.LogMethod($"Gate {r.Id} free");
      }
   }
}