using System;
using System.Collections.Generic;
using System.Text;

namespace PaperBoy.Helpers
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string fileName);
        List<string> GetSpecialFolders();
    }
}
