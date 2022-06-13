/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
 */
using TestApp.ViewModels;
using Xunit;
/// <summary>
/// This test class test the methods of DisplayExercisesViewModel
/// </summary>
namespace UnitTestProject
{
    public class DisplayExercisePlansViewModelTest
    {
        DisplayExercisePlansViewModel model = new DisplayExercisePlansViewModel();
        [Fact]
        public void getExerciseListTest()
        {
            Assert.NotNull(model.getExerciseList(true));

        }
        [Fact]
        public void sendExerciseKeyTest1()
        {
            Assert.NotNull(model.sendExerciseKey("Knee pain",true));
        }
        [Fact]
        public void sendExerciseKeyTest2()
        {
            var value = model.sendExerciseKey("", true).Result;
            Assert.Null(value);
        }
    }
}
