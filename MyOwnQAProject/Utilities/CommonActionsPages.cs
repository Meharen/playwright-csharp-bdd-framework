using System;
using System.IO;

namespace Enterprise.Automation.Framework.Pages
{
    public class CommonActionsPage
    {
        public static string GetProjectRoot()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            DirectoryInfo? directory = new DirectoryInfo(currentDirectory);

            while (directory != null && !File.Exists(Path.Combine(directory.FullName, "Enterprise.Automation.Framework.csproj")))
            {
                directory = directory.Parent;
            }

            return directory?.FullName ?? currentDirectory;
        }
    }
}