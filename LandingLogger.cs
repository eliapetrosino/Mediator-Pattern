namespace Mediator_Pattern
{
   public static class LandingLogger {
   //? Come parametro di Runaway e conseguente Mediator anche nel suo costruttore
      public static void LogLandingRequest(Airplane p) { 
         Color.Foreground("gray");
         p.LogCamaleon($"\n* {p.GetTypeShort()} ID: {p.ID} ask for landing *");
      }
      public static void LogEnqueuing(Airplane p, Runaway r) {
         Color.Foreground("dark");
         p.LogCamaleon($"Request enqueued to gate {r.ID} - Airplane ID{p.ID}\n{r.CurrentQueue()}");
      }
      public static void LogLanding(Airplane p, Runaway r) {
         Color.Foreground("blue");
         p.LogCamaleon($"{p.GetTypeShort()} ID: {p.ID} Landing to gate {r.ID}");
      }
   }
}