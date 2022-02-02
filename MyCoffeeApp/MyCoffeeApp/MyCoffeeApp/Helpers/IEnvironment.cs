using System.Drawing;

namespace MyCoffeeApp.Helpers
{
    public interface IEnvironment
    {
        void SetStatusBarColor(Color color, bool darkStatusBarTint);
    }
}
