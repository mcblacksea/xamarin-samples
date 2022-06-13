/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
 */
using System.Collections.Generic;
using TestApp.Models;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class DisplayPatientExercisePlanViewModelTest
    {
        private DisplayPatientExercisePlanViewModel display = new DisplayPatientExercisePlanViewModel();
        [Fact]
        public void getPatientExercisesTest()
        {
            List<Exercise> result = display.getPatientExercises("squat", "hamstring stretch", "heel and calf stretch", true).Result;
            Assert.NotEmpty(result);
        }
        [Fact]
        public void removeTimeFromDateTest()
        {
            string result = display.removeTimeFromDate("16/01/2022 12:00:00");
            string expected = "16/01/2022";
            Assert.Equal(expected, result);
        }
        [Fact]
        public void getExerciseVideoLinkTest()
        {
            string videoLink = display.getExerciseVideoLink("key1", true).Result;
            string expected = "http://";
            Assert.Contains(expected, videoLink);
        }
        [Fact]
        public void getpatientDetailsTest()
        {
            Patient patient = display.getpatientDetails("1acdg456we",true).Result;
            Assert.NotNull(patient);
        }
        [Fact]
        public void getPatientsExercisePlanTest()
        {
            List<Exercise> result = display.getPatientsExercisePlan("1acdg456we", true).Result;
            Assert.NotEmpty(result);
        }
        [Fact]
        public void saveStateOfExercisePlanTest()
        {
            bool result = display.saveStateOfExercisePlan("5FBY78XCES", "16/01/2022 12:00:00",true).Result;
            Assert.True(result);
        }
        [Fact]
        public void checkIfExercisePlanCompleteForTodayTestPasses()
        {
            bool result = display.checkIfExercisePlanCompleteForToday("23-02-2022", "1agdsjwus", true).Result;
            Assert.True(result);
        }
        [Fact]
        public void checkIfExercisePlanCompleteForTodayTestFails()
        {
            bool result = display.checkIfExercisePlanCompleteForToday("22-02-2022", "1agdsjwus", true).Result;
            Assert.False(result);
        }
    }
}
