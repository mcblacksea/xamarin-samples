/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using UIKit;

namespace TestApp.iOS
{
    /// <summary>
    /// This class is the startup of the IOS application
    /// </summary>
    public class Application
    {
        /// <summary>
        /// This is the main entry point of the IOS application.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it heres
            UIApplication.Main(args, null, typeof(AppDelegate));
        }
    }
}
