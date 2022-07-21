using System;

namespace RecruitmentAssignmentSente
{
    class Program
    {
        static void Main(string[] args)
        {
            ForceExceptionsLanguage(language: "en-us");

            if (args == null || args.Length < 2)
            {
                PrintInfo();
                return;
            }

            var transfersFilePath = args[0];
            var structureFilePath = args[1];

            Console.Read();
        }

        public static void PrintInfo()
            => Console.WriteLine($"Usage:\n{GetCurrentProcessFilename()} TransfersFilePath StructureFilePath");

        private static string GetCurrentProcessFilename()
            => System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

        private static void ForceExceptionsLanguage(string language)
            => System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(language);
    }
}
