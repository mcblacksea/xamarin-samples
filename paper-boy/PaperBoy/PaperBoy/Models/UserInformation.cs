using System;
using System.Collections.Generic;
using System.Text;
using PaperBoy.Common;
namespace PaperBoy.Models
{
    public class UserInformation :ObservableBase
    {
        private string _displayName;
        public string DisplayName
        {
            get => _displayName;
            set { SetProperty(ref this._displayName, value); }
        }
        private string _bioContent;
        public string BioContent
        {
            get=>_bioContent;
            set { SetProperty(ref this._bioContent, value); }
        }
        private string _profileImage;
        public string ProfileImage
        {
            get => _profileImage;
            set { SetProperty(ref this._profileImage,value); }
        }

    }
}
