using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Mediator_Pattern 
{
   public static class IntegerExthensions {
      public static TaskAwaiter GetAwaiter(this int seconds) {
         return Task.Delay(TimeSpan.FromSeconds(seconds)).GetAwaiter();
      }
   }
   public static class ObjExtension {
      public static string GetTypeShort(this object obj) {
         var pieces = obj.GetType().ToString().Split('.');
         return pieces[pieces.Length - 1];
      }
   }
   public static class Color
   {
      public static void Foreground(string color) {
         Console.ForegroundColor = color switch
         {
            "gray" => ConsoleColor.Gray,
            "green" => ConsoleColor.DarkGreen,
            "blue" => ConsoleColor.DarkBlue,
            "yellow" => ConsoleColor.DarkYellow,
            "red" => ConsoleColor.DarkRed,
            "dark" => ConsoleColor.DarkGray,
            "watergreen" => ConsoleColor.DarkCyan,
            "purple" => ConsoleColor.DarkMagenta,
            _ => ConsoleColor.White
         };
      }
   }
}