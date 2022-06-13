/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
 */
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class RegisterPhysioViewModelTest
    {
        RegisterPhysioViewModel physioVm = new RegisterPhysioViewModel();
        [Fact]
        public async void setUpPhysioAccountTest()
        {
            bool result = await physioVm.setUpPhysioAccount("john@gmail.com", "John Dooly", "1vbfg45ser3", "2020/02/26", "password1234", true);
            Assert.True(result);
        }
    }
}
