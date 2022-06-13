/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://medium.com/firebase-developers/firebase-auth-on-xamarin-forms-171432aa3d76
 */
using Android.Gms.Extensions;
using Firebase.Auth;
using System.Threading.Tasks;
using TestApp.Droid;
using TestApp.ViewModels;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidLoginCheck))]
namespace TestApp.Droid
{
    /// <summary>
    /// This class inherts LoginWithEmailPassword and SignInWithEmailAndPasswordAsync methods from IFirebaseAuthenticator
    /// which is used to check if a user is authorised to login
    /// </summary>
    public class AndroidLoginCheck : IFirebaseAuthenticator
    {
        /// <summary>
        /// This method takes the users login and password uses FirebaseAuth to check if the user is verified
        /// and returns a token if they were logged in successfully
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// </summary>
        public async Task<string> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var user = await FirebaseAuth.Instance.
                            SignInWithEmailAndPasswordAsync(email, password);//https://github.com/xamarin/GooglePlayServicesComponents/issues/391
                var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>());
                var value = user.User.Uid;
                string newUid = value.ToString();
                //return token.Token;
                return newUid;
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "";
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return "";
            }
        }////////////////////////////////////////////////////////////////////
        /// <summary>
        /// This method allows a user to register with the application through firebase Auth
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        //https://www.py4u.net/discuss/1541967
        public async Task<string> SignupWithEmailPassword(string email, string password)
        {
            var user = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
            var value = user.User.Uid;
            string newUid = value.ToString();
            var token = await (FirebaseAuth.Instance.CurrentUser.GetIdToken(false).AsAsync<GetTokenResult>());
            //return token.Token;
            return newUid;
        }
    }
}