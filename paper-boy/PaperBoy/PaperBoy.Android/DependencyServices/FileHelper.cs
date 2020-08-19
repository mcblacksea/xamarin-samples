using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PaperBoy.Helpers;
using Xamarin.Forms;
using PaperBoy.Droid.DependencyServices;

[assembly:Dependency(typeof(FileHelper))]
namespace PaperBoy.Droid.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, fileName);
        }
        public List<string> GetSpecialFolders()
        {
            List<string> folders = new List<string>();
            foreach (var folder in Enum.GetValues(typeof(System.Environment.SpecialFolder)))
            {
                if (!string.IsNullOrEmpty(System.Environment.GetFolderPath((System.Environment.SpecialFolder)folder)))
                {
                    folders.Add($"{folder}={System.Environment.GetFolderPath((System.Environment.SpecialFolder)folder)}");
                }
            }
            return folders;

        }
    }
}