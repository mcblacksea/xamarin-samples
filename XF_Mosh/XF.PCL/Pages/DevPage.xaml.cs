using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.PCL.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DevPage : ContentPage
    {
        public DevPage()
        {
            InitializeComponent();
            slider.Value = 0.5;

            // Device Platform determination
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    Padding = new Thickness(0, 30, 0, 0);
                    break;
                case Device.Android:
                    Padding = new Thickness(10, 20, 0, 0);
                    break;
                case Device.UWP:
                    Padding = new Thickness(30, 20, 0, 0);
                    break;
                default:
                    break;
            }

            /* 
             * Content in code: 
             *   Content = new Label()
             *   {
             *       HorizontalOptions = LayoutOptions.Center,
             *       VerticalOptions = LayoutOptions.Center,
             *       Text = "Hello"
             *   };
            */
        }
    }
}