/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://docs.microsoft.com/en-us/xamarin/essentials/email?tabs=ios
 */
using System;
using System.Collections.Generic;
using TestApp.services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using TestApp.models;

namespace TestApp.ViewModels
{
    /// <summary>
    /// This class passes patient information to the database 
    /// Generates a unique password for a patients login
    /// Generates an email so the login information can be sent to the patient
    /// </summary>
    class RegisterPatientViewModel
    {
        private string password = "";
        private IFirebaseAuthenticator auth;
        private FirebaseMethods fireBase;
        private List<string> patientEmailList;
        private SecurityViewModel security = new SecurityViewModel();
        private string salt = "";
        private string saltedPassword = "";
        private string patientUid = "";
        private string encryptionKey = "";
        public RegisterPatientViewModel(IFirebaseAuthenticator auth)
        {
            this.auth = auth;
            fireBase = FirebaseMethods.GetInstance();
            patientEmailList = new List<string>();
        }
        public RegisterPatientViewModel()//used for testing IFirebaseAuthenticator can not be run in unit test
        {
            fireBase = FirebaseMethods.GetInstance();
            patientEmailList = new List<string>();
        }
        /// <summary>
        /// This method takes data from Register patient view and
        /// sends a generated password and the patients email to make an account for that patient
        /// then uses the user id to fill in the rest of the patients details into the patients database
        /// the user iid is then added to the physios patients list
        /// this method calls the method to send login details to the patient by email 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="exerPlan"></param> 
        /// <param name="physioUid"></param>
        public async Task<bool> setUpPatientAccount(string name, string gender, string email, string injuryType, string injuryOccurred, int age, int injurySeverity, Dictionary<string, bool> planDates, ExercisePlan exerPlan, string physioUid, string newInjuryType, string newinjuryOccurred,int min1,int min2,int min3,int max1,int max2,int max3,bool isMocked)
        {
            try
            {
                if (newInjuryType != null)
                {
                    injuryType = newInjuryType;
                }
                if (newinjuryOccurred != null)
                {
                    injuryOccurred = newinjuryOccurred;
                }
                
                patientEmailList.Add(email);
                password = security.generateSaltOrPasswordOrUid(15);
                if (isMocked)
                {
                    patientUid = "";
                }
                else
                {
                    patientUid = await auth.SignupWithEmailPassword(email, password);
                }
                if(patientUid == "true")//this checks if the iOS SignupWithEmailPassword method is called
                {
                    password = password.Remove(password.Length - 1, 1);
                    salt = security.generateSaltOrPasswordOrUid(32);
                    saltedPassword = security.md5HashAndSaltThePassword(salt, password);
                    patientUid = security.generateSaltOrPasswordOrUid(10);
                    await fireBase.iOSSignupWithEmailPassword(email, salt, saltedPassword, patientUid, isMocked);
                }

                encryptionKey = security.generateSaltOrPasswordOrUid(32);
                string hashedName = security.encryptData(name, encryptionKey);
                string hashedEmail = security.encryptData(email, encryptionKey);
                await fireBase.AddPatient(patientUid, hashedName, gender, injuryType, injuryOccurred, age, injurySeverity,exerPlan.Exercise1, exerPlan.Exercise2, exerPlan.Exercise3, hashedEmail, min1,min2,min3,max1,max2,max3, isMocked);
                await fireBase.AddPatientUIDToPatientList(physioUid, patientUid, hashedName, isMocked);
                await fireBase.recordPatientProgress(patientUid, planDates, isMocked);
                await fireBase.addUserType(patientUid,"patient", isMocked);

                await fireBase.AddEncryptionKeyToUserId(patientUid,encryptionKey, isMocked);

                string body = "Please find attached login details,\n Use this email and here is the password " + password;
                string subject = "Injury Recovery Login Details";
                await SendPatientEmail(patientEmailList, password,body,subject);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }

        }
        /// <summary>
        /// This method adds a plan to an existing patient and sends an email to the patient 
        /// telling them that the new plan is ready to be started
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="planDates"></param>
        /// <param name="exerPlan"></param>
        /// <param name="physioUid"></param>
        /// <param name="newInjuryType"></param>
        /// <param name="newinjuryOccurred"></param>
        /// <param name="min1"></param>
        /// <param name="min2"></param>
        /// <param name="min3"></param>
        /// <param name="max1"></param>
        /// <param name="max2"></param>
        /// <param name="max3"></param>
        /// <param name="patientUid"></param>
        /// <returns></returns>
        public async Task<bool> addNewPlanToExistingPatient(string name, string gender, string email, string injuryType, string injuryOccurred, int age, int injurySeverity, Dictionary<string, bool> planDates, ExercisePlan exerPlan, string physioUid, string newInjuryType, string newinjuryOccurred, int min1, int min2, int min3, int max1, int max2, int max3,string patientUid,string encryptKey)
        {
            if (newInjuryType != null)
            {
                injuryType = newInjuryType;
            }
            if (newinjuryOccurred != null)
            {
                injuryOccurred = newinjuryOccurred;
            }
            
            patientEmailList.Add(email);
            string hashedName = security.encryptData(name, encryptKey);
            string hashedEmail = security.encryptData(email, encryptKey);
            await fireBase.AddPatient(patientUid, hashedName, gender, injuryType, injuryOccurred, age, injurySeverity, exerPlan.Exercise1, exerPlan.Exercise2, exerPlan.Exercise3, hashedEmail, min1, min2, min3, max1, max2, max3, false);
            await fireBase.recordPatientProgress(patientUid, planDates, false);

            await fireBase.AddEncryptionKeyToUserId(patientUid, encryptKey, false);

            string body = "Your exercise plan has been updated please login with your old email and password";
            string subject = "Injury Recovery App";
            await SendPatientEmail(patientEmailList, password, body, subject);
            return true;
        }
        /// <summary>
        /// This method updates the number of exercises a patient will have to do in their plan
        /// </summary>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="email"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="min1"></param>
        /// <param name="min2"></param>
        /// <param name="min3"></param>
        /// <param name="max1"></param>
        /// <param name="max2"></param>
        /// <param name="max3"></param>
        /// <param name="exer1"></param>
        /// <param name="exer2"></param>
        /// <param name="exer3"></param>
        /// <param name="patientUid"></param>
        /// <returns></returns>
        public async Task<bool> updatePatientRecord(string name, string gender, string email, string injuryType, string injuryOccurred, int age, int injurySeverity,int min1, int min2, int min3, int max1, int max2, int max3,string exer1,string exer2,string exer3, string patientUid)
        {
            bool tf = await fireBase.AddPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity, exer1, exer2, exer3, email, min1, min2, min3, max1, max2, max3, false);
            return tf;
        }
        /// <summary>
        /// This method uses xamarin essentials to open an email application on the users phone and populates the body
        /// the patients email and the subject of the email it returns a bool for testing purposes.
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> SendPatientEmail(List<string> recipients, string password,string body,string subject)
        {
            try
            {
                EmailMessage message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                };
                await Email.ComposeAsync(message);
                return true;
            }
            catch (FeatureNotSupportedException ns)
            {
                var value = ns.StackTrace;
                Console.WriteLine(value);
                return false;
            }
            catch (Exception ex)
            {
                var value = ex.StackTrace;
                Console.WriteLine(value);
                return false;
            }
        }///////////////////////////////////////////////////////////////////////////////////
    }
}
