/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://medium.com/firebase-developers/firebase-auth-on-xamarin-forms-171432aa3d76
 */
using System.Threading.Tasks;

namespace TestApp.ViewModels
{
    /// <summary>
    /// These interface methods are used to tell mainPage.xaml.cs which os is trying to login or register
    /// </summary>
    public interface IFirebaseAuthenticator
    {
        Task<string> LoginWithEmailPassword(string email, string password);
        Task<string> SignupWithEmailPassword(string email, string password);
    }
}
