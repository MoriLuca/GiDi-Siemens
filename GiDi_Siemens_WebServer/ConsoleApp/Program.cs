using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            bool a,b;

            while (true)
            {
                
                var watch = System.Diagnostics.Stopwatch.StartNew();
                a = Repo.SiemensPlc.Plc.IsAvailable;
                b = Repo.SiemensPlc.Plc.IsConnected;
                if (a && !b) Repo.SiemensPlc.Plc.Open();
                if (!a) Repo.SiemensPlc.Plc.Close();
                watch.Stop();
                Console.WriteLine($"{a}-{b}  in  {watch.ElapsedMilliseconds}");
                Thread.Sleep(300);
            }
            
        }
    }
}
