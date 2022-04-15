using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XFDataTemplateSelectorSample
{
    public partial class MainPage : ContentPage
    {
        public List<string> SampleItems { get; set; } = new List<string>()
        {
            "Hey there, friend!",
            "Did you already subscribe?",
            "Do it now!"
        };

        public MainPage()
        {
            InitializeComponent();

            BindingContext = this;
        }
    }
}
