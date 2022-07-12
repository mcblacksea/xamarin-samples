using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XCTValidatorDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void OnSignUpClicked(object sender, EventArgs e)
        {
            EmailValidator.ForceValidate();
            PasswordValidator.ForceValidate();
            RepeatPasswordValidator.ForceValidate();
        }
    }
}
