using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PaperBoy.Helpers
{
    public static class StorageHelper
    {
        public static List<string> GetSpecialFolders()
        {
            return DependencyService.Get<IFileHelper>().GetSpecialFolders();
        }

        public static string GetLocalFilePath(string dbName)
        {
           return DependencyService.Get<IFileHelper>().GetLocalFilePath(dbName);
        }
    }
}
