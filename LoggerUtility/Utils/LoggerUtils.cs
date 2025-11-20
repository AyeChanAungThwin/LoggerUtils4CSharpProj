using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;

namespace LoggerUtility.Utils
{
    public class LoggerUtils
    {
        private static bool SAVE_LOG = true;
        private static readonly string currentDirectory = Directory.GetCurrentDirectory();
        public static readonly string LogFilePath = Path.Combine(currentDirectory, "OverAllLog.txt");
        public static readonly string IndividualDirectoryPath = Path.Combine(currentDirectory, "IndividualLogs");

        private static void CreateLogFile()
        {
            if (!File.Exists(LogFilePath))
            {
                File.Create(LogFilePath).Dispose();
            }
        }
        
        private static void mainLog(
            string message,
            string RoW,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!SAVE_LOG) return;
            // Get the calling class type
            Type declaringType = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
        
            string className = declaringType?.FullName ?? "UnknownClass";
            
            //Type type = Type.GetType(className);
            //string fullClassName = declaringType?.FullName ?? "UnknownClass";
            //string assemblyName = declaringType.Assembly.GetName().Name;
            //string assemblyLocation = declaringType.Assembly.Location;
            //string fullQualifiedName = declaringType.AssemblyQualifiedName;
            string fileName = Path.GetFileName(filePath);
            string directory = ClassNameResolver.GetFullClassName(fileName.Replace(".cs", ""));//Path.GetDirectoryName(filePath);
            
            var time = $"{DateTime.UtcNow:yyyy-MM-dd HH:mm:ss.fff}]\n  🕑{TimeZoneInfo.Local.DisplayName}\n  [{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}";
            
            if (new FileInfo(LogFilePath).Length > 0)
                File.AppendAllText(LogFilePath, Environment.NewLine+new String('-', 50)+"\n");
                File.AppendAllText(LogFilePath, $"{RoW} [{time}]");
                File.AppendAllText(LogFilePath, $"\n  Directory : {directory.Replace(".", "\\")}.cs");
                File.AppendAllText(LogFilePath, $"\n  Class     : {fileName}");
                File.AppendAllText(LogFilePath, $"\n  Method    : {memberName}");
                File.AppendAllText(LogFilePath, $"\n  LineNumber: {lineNumber}");
                //File.AppendAllText(LogFilePath, $"\n  Class:    {className}");
                File.AppendAllText(LogFilePath, $"\n  Message   : {message}");
            
            individualLog(RoW, time, lineNumber, memberName, message, className, directory, fileName);
        }

        private static void individualLog(string RoW, string time, int lineNumber, string memberName, string message, string className, string directory, string fileName)
        {
            if (!Directory.Exists(IndividualDirectoryPath))
            {
                Directory.CreateDirectory(IndividualDirectoryPath);
            }

            var actualFileName = Path.GetFileNameWithoutExtension(fileName.Replace(".xaml", ""));
            actualFileName = Path.GetFileNameWithoutExtension(fileName.Replace(".cs", ""));
            var individualFiles = Path.Combine(IndividualDirectoryPath, $"{actualFileName}.txt");
            if (!File.Exists(individualFiles))
            {
                File.Create(individualFiles).Dispose();
            }
            
            if (new FileInfo(individualFiles).Length > 0)
                File.AppendAllText(individualFiles, Environment.NewLine+new String('-', 50)+"\n");
                File.AppendAllText(individualFiles, $"{RoW} [{time}]");
                File.AppendAllText(individualFiles, $"\n  Directory : {directory.Replace(".", "\\")}.cs");
                File.AppendAllText(individualFiles, $"\n  Class Name: {fileName}");
                File.AppendAllText(individualFiles, $"\n  Method    : {memberName}");
                File.AppendAllText(individualFiles, $"\n  LineNumber: {lineNumber}");
                //File.AppendAllText(individualFiles, $"\n  Class:    {className}");
                File.AppendAllText(individualFiles, $"\n  Message   : {message}");
        }

        public static void log(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            CreateLogFile();
            if (!SAVE_LOG) return;
            if (String.IsNullOrEmpty(message)) return;
            mainLog(message, "✓", memberName, filePath, lineNumber);
        }
        
        public static void err(
            string message,
            [CallerMemberName] string memberName = "",
            [CallerFilePath] string filePath = "",
            [CallerLineNumber] int lineNumber = 0)
        {
            CreateLogFile();
            if (!SAVE_LOG) return;
            mainLog(message, "⛌", memberName, filePath, lineNumber);
        }

        public void Reset()
        {
            File.WriteAllText(LogFilePath, string.Empty);
        }
    }
}