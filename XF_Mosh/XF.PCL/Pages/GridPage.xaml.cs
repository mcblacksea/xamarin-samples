using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XF.PCL.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GridPage : ContentPage
    {
        public GridPage()
        {
            InitializeComponent();

            //var grid = new Grid
            //{
            //    RowSpacing = 20,
            //    ColumnSpacing = 40,
            //};

            //var label = new Label { Text = "Label Demo" };
            //grid.Children.Add(label, 0, 0);
            //Grid.SetRowSpan(label, 2);
            //Grid.SetColumnSpan(label, 2);
            //Grid.SetRow(label, 0);
            //Grid.SetColumn(label, 0);

            //grid.RowDefinitions.Add(new RowDefinition
            //{
            //    Height = new GridLength(100, GridUnitType.Absolute)
            //});

            //grid.RowDefinitions.Add(new RowDefinition
            //{
            //    Height = new GridLength(100, GridUnitType.Star)
            //});

            //grid.RowDefinitions.Add(new RowDefinition
            //{
            //    Height = new GridLength(100, GridUnitType.Auto)
            //});

            //this.Content = grid;
        }
    }
}