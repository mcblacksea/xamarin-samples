using Plugin.XamarinFormsSaveOpenPDFPackage;
using System;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;

namespace XFOpenPDFSample
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(Object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            var stream = await httpClient.GetStreamAsync("https://gerald.verslu.is/subscribe.pdf");

            using (var memoryStream = new MemoryStream())
            {
                await stream.CopyToAsync(memoryStream);

                await CrossXamarinFormsSaveOpenPDFPackage.Current.SaveAndView("myFile.pdf", "application/pdf", memoryStream, PDFOpenContext.InApp);
            }
        }
    }
}
