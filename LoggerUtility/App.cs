using System;
using LoggerUtility.Model;

namespace LoggerUtility
{
    internal class App
    {
        public static void Main(string[] args)
        {
            GlobalExceptionHandler();
        }

        private static void GlobalExceptionHandler()
        {
            // Set up global exception handlers
            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Console.WriteLine($"Unhandled exception: {e.ExceptionObject}");
                // Log to file, etc.
                Environment.Exit(1);
            };

            // For .NET Core 3.0+ console apps
            Console.CancelKeyPress += (sender, e) =>
            {
                Console.WriteLine("Application interrupted");
                Environment.Exit(0);
            };
            
            RunApplication();
        }
        
        static void RunApplication()
        {
            try
            {
                // Main app logic
                Console.WriteLine("Application running...");
                
                // Simulate work
                new Car().drive();
                // Test exception
                new Motorcycle().drive();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Handled exception: {ex.Message}");
            }
        }
    }
}