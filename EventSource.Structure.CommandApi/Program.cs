using Microsoft.Owin.Hosting;
using System;

namespace EventSource.Structure.CommandApi
{
    class Program
    {
        static void Main(string[] args)
        {
            const string baseAddress = "http://localhost:9000/";

            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"Host Is Ready On {baseAddress}");

                Console.ReadLine();
            }
        }
    }
}
