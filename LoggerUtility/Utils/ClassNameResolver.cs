using System;
using System.Collections.Generic;
using System.Linq;

namespace LoggerUtility.Utils
{
    public class ClassNameResolver
    {
        public static string GetFullClassName(string className)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.Name.Equals(className, StringComparison.OrdinalIgnoreCase))
                .ToList();
        
            if (types.Count == 0)
                throw new ArgumentException($"Class '{className}' not found");
        
            if (types.Count > 1)
            {
                Console.WriteLine($"Multiple classes found with name '{className}'. Using first match.");
                // You could also throw an exception or return a list
            }
        
            return types.First().FullName;
        }
    
        public static List<string> GetAllFullClassNames(string className)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => t.Name.Equals(className, StringComparison.OrdinalIgnoreCase))
                .Select(t => t.FullName)
                .ToList();
        }
    }
}