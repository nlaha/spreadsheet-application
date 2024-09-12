// <copyright file="Program.cs" company="Nathan Laha">
// 11762135
// </copyright>

namespace HW2_Project
{
    /// <summary>
    /// Main program entry class.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}