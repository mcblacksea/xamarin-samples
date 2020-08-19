using PaperBoy.Helpers;
using PaperBoy.UWP.DependencyServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Xamarin.Forms;

[assembly:Dependency(typeof(FileHelper))]
namespace PaperBoy.UWP.DependencyServices
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string fileName)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, fileName);
        }
        public List<string> GetSpecialFolders()
        {
            return new List<string>();
        }
    }
}
