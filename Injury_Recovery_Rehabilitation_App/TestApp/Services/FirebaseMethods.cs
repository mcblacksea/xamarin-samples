/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using Firebase.Storage;
using TestApp.models;
using TestApp.Models;
using TestApp.Services;

namespace TestApp.services
{
    /// <summary>
    /// FirebaseMethods class contains methods used to read and write to the Firebase realtime database
    /// </summary>
    public class FirebaseMethods
    {
        private static FirebaseClient firebase = new FirebaseClient("https://injuryrecovery-default-rtdb.europe-west1.firebasedatabase.app/");
        private FirebaseStorage firebaseStorage = new FirebaseStorage("injuryrecovery.appspot.com");
        private static FirebaseMethods fire = new FirebaseMethods();
        private static List<ExercisePlan> plans = new List<ExercisePlan>();
        private static List<ExercisePlan> physioPlans = new List<ExercisePlan>();
        private static List<Exercise> exerciseList = new List<Exercise>();
        MockDatabase db = new MockDatabase();
        /// <summary>
        /// The constructor is private as the singleton pattern is used
        /// </summary>
        private FirebaseMethods()
        {
            
        }
        /// <summary>
        /// This method returns a single instance to all callers of the FirebaseMethods class
        /// </summary>
        /// <returns></returns>
        public static FirebaseMethods GetInstance()
        {
            return fire;
        }
        /// <summary>
        /// This method returns all exercise plans of a JSON list called exercisePlans
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        //https://xamarinmonkeys.blogspot.com/2019/01/xamarinforms-working-with-firebase.html
        public async Task<List<ExercisePlan>> GetAllExercises(bool isMocked)
        {

            try
            {
                if (isMocked == true)
                {
                    return db.GetAllMockExercises();
                }
                if (plans.Count == 0)
                {
                    plans = (await firebase
                   .Child("exercisePlans")
                   .OnceAsync<ExercisePlan>()).Select(item => new ExercisePlan
                   {
                       exerciseName = item.Object.exerciseName,
                       ImageBase64 = item.Object.ImageBase64,
                       Category = item.Object.Category,
                       Exercise1 = item.Object.Exercise1,
                       Exercise2 = item.Object.Exercise2,
                       Exercise3 = item.Object.Exercise3,
                       exerciseListKey = item.Key,
                   }).ToList();
                }
                return plans;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<ExercisePlan> GetExercise(String exerciseKey, bool isMocked)
        {
            var allExercises = await GetAllExercises(isMocked);
            if (isMocked == true)
            {
                return db.GetMockExercise(exerciseKey);
            }
            await firebase
              .Child("exercisePlans")
              .OnceAsync<ExercisePlan>();
            return allExercises.Where(a => a.exerciseName == exerciseKey).FirstOrDefault();
        }
        /// <summary>
        /// This method inserts a new patient entry into the patient database using the patient user id as the key
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <param name="email"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatient(string patientUid, string name, string gender, string injuryType, string injuryOccurred, int age, int injurySeverity, string exercise1, string exercise2, string exercise3, string email,int min1,int min2,int min3,int max1,int max2,int max3, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatient(patientUid, name, gender, injuryType, injuryOccurred, age, injurySeverity,exercise1, exercise2, exercise3, email);
            }
            try
            {
                await firebase
                .Child("patients").Child(patientUid)
                .PutAsync(new Patient()
                {
                    PatientName = name,
                    Gender = gender,
                    InjuryType = injuryType,
                    InjuryOccurred = injuryOccurred,
                    Age = age,
                    InjurySeverity = injurySeverity,
                    Exer1 = exercise1,
                    minExercise1 = min1,
                    maxExercise1 = max1,
                    Exer2 = exercise2,
                    minExercise2 = min2,
                    maxExercise2 = max2,
                    Exer3 = exercise3,
                    minExercise3 = min3,
                    maxExercise3 = max3,
                    Email = email
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method inserts a new physio into the physio database
        /// </summary>
        /// <param name="physioUid"></param>
        /// <param name="name"></param>
        /// <param name="physioIdNumber"></param>
        /// <param name="physioEmail"></param>
        /// <param name="membership"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPhysio(string physioUid, string name,string physioIdNumber, string physioEmail,string membership,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPhysio(physioUid, name, physioIdNumber, physioEmail, membership);
            }
            try
            {
                await firebase
                .Child("physio").Child(physioUid)
                .PutAsync(new Physiotherapist()
                {
                    PhysioName = name,
                    Email = physioEmail,
                    IdNumber = physioIdNumber,
                    Membership = membership
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method adds an encryption key to a patient user id
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="encryptionKey"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddEncryptionKeyToUserId(string patientUid,string encryptionKey, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockEncryptionKeyToUserId(patientUid, encryptionKey);
            }
            try
            {
                await firebase
                .Child("encryptionKeys").Child(patientUid)
                .PutAsync(new Encryption()
                {
                    EncryptionKey = encryptionKey,
                    PatientUid = patientUid
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method returns a list of patient encryption keys for encrypting data
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<Encryption>> getPatientEncryptionKeys(bool isMocked)
        {
            if (isMocked == true)
            {
                return db.getMockPatientEncryptionKeys();
            }
            try
            {
                List<Encryption> encryptionKeys = (await firebase
                   .Child("encryptionKeys")
                   .OnceAsync<Encryption>()).Select(item => new Encryption
                   {
                       EncryptionKey = item.Object.EncryptionKey,
                       PatientUid = item.Object.PatientUid

                   }).ToList();

                return encryptionKeys;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method returns the encryption key for the data assigned to the patient
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<string> getKeyToData(string PatientUID, bool isMocked)
        {

            if (isMocked == true)
            {
                return db.getMockKeyToData();
            }
            try
            {
                Encryption encryptKeyObject = await firebase.Child("encryptionKeys").Child(PatientUID).OnceSingleAsync<Encryption>();
                return encryptKeyObject.EncryptionKey;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This function adds the patient unique identifier and name to the physios patient list
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="PatientUID"></param>
        /// <param name="patientName"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> AddPatientUIDToPatientList(string PhysioUid, string PatientUID, string patientName, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockPatientUIDToPatientList(PhysioUid, PatientUID);
            }
            try
            {
                await firebase
                .Child("patientList").Child(PhysioUid).Child(PatientUID)
                .PutAsync(new PatientList()
                { PatientUid = PatientUID, PatientName = patientName });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method retruns a list containing a patints name and user id 
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<PatientList>> GetNamesAndPatientUids(string PhysioUid, bool isMocked)
        {
            try
            {
                if (isMocked == true)
                {
                    return db.GetMockNamesAndPatientUids();
                }
                else
                {
                    List<PatientList> value = (await firebase
                   .Child("patientList").Child(PhysioUid)
                   .OnceAsync<PatientList>()).Select(item => new PatientList
                   {
                       PatientName = item.Object.PatientName,
                       PatientUid = item.Object.PatientUid
                   }).ToList();

                    return value;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// This method assigns which type of user patient or physio is tied to a userid
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="userType"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> addUserType(string userUid,string userType ,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.AddMockUserType(userType);
            }
            try
            {
                await firebase
                .Child("CheckUser").Child(userUid)
                .PutAsync(new CheckUser()
                { User = userType });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method saves all the exercise plan dates for a patient
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="isMocked"></param>
        /// <param name="dates"></param>
        /// <returns></returns>
        public async Task<bool> recordPatientProgress(string PatientUID, Dictionary<string, bool> dates,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.recordMockPatientProgress(dates);
            }
            try
            {
                await firebase
                .Child("patientProgress").Child(PatientUID)
                .PutAsync(new Progress()
                { planDates = dates });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method updates the value set at a date of the patients plan 
        /// </summary>
        /// <param name="PatientUID"></param>
        /// <param name="date"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Dictionary<string, bool>> getPatientProgress(string PatientUID, bool isMocked)
        {

            if (isMocked == true)
            {
                return db.getMockPatientProgress();
            }
            try
            {
                var value = await firebase.Child("patientProgress").Child(PatientUID).OnceSingleAsync<Progress>();
                return value.planDates;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        
        /// <summary>
        /// This method uses the patient user id and returns information about the patient 
        /// including what exercises have been assigned to them for their rehabilitation
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<Patient> getpatientDetails(string patientUid, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.getMockpatientDetails();
            }
            try
            {
                return await firebase
               .Child("patients").Child(patientUid).OnceSingleAsync<Patient>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method returns what type of user is trying to access the app
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<CheckUser> getTypeOfUser(string userId, bool isMocked)
        {
            if (isMocked == true)
            {
                return db.getMockTypeOfUser(userId);
            }
            try
            {
                return await firebase
               .Child("CheckUser").Child(userId).OnceSingleAsync<CheckUser>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This function gets a list of exercises from the database then
        /// the three entered exercise strings are used to build a list that contains the exercise plan for the patient
        /// </summary>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<List<Exercise>> GetPatientExercises(string exercise1, string exercise2, string exercise3 ,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.GetMockPatientExercises();
            }
            try
            {
                List<Exercise> allExercises = (await firebase
                   .Child("exercises")
                   .OnceAsync<Exercise>()).Select(item => new Exercise
                   {
                       ExerciseName = item.Object.ExerciseName,
                       exerciseVideoCopyright = item.Object.exerciseVideoCopyright,
                       VideoLink = item.Object.VideoLink
                   }).ToList();

                foreach (Exercise exercise in allExercises)
                {
                    if (exercise1 == exercise.ExerciseName || exercise2 == exercise.ExerciseName || exercise3 == exercise.ExerciseName)
                    {
                        exerciseList.Add(exercise);
                    }
                }
                return exerciseList;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method gets an exercise video download link back from firebase storage
        /// </summary>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<string> GetVideosFromStorage(bool isMocked,string videoName)
        {
            if (isMocked == true)
            {
                return db.GetMockVideosFromStorage(videoName);
            }
            try
            {
                var video = await firebaseStorage.Child("video").Child(videoName).GetDownloadUrlAsync();
                string downloadUrl = video.ToString();
                return downloadUrl;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method works as a replacement for firebases SignupWithEmailPassword for iOS
        /// </summary>
        /// <param name="email"></param>
        /// <param name="salted"></param>
        /// <param name="saltedAndHashed"></param>
        /// <param name="uid"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public async Task<bool> iOSSignupWithEmailPassword(string email, string salted, string saltedAndHashed,string uid,bool isMocked)
        {
            if (isMocked == true)
            {
                return db.iOSMockSignupWithEmailPassword(email,salted,saltedAndHashed);
            }
            try
            {
                email = email.Replace("@", "");
                email = email.Replace(".", "");
                await firebase
                .Child("iosCredentials").Child(email)
                .PutAsync(new IosCredentials()
                {
                    userId = uid,
                    salt = salted,
                    saltHashed = saltedAndHashed
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method gets the salt the saltedAndHashed password and user id back from a valid email address
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<IosCredentials> getIosPatientPasswordDetails(bool isMocked, string email)
        {
            email = email.Replace("@", "");
            email = email.Replace(".", "");
            if (isMocked == true)
            {
                return db.getMockIosPatientPasswordDetails();
            }
            try
            {
                return await firebase.Child("iosCredentials").Child(email).OnceSingleAsync<IosCredentials>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method returns the current membership date tied to the physio 
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="physioUid"></param>
        /// <returns></returns>
        public async Task<Membership> checkPhysioMembership(bool isMocked,string physioUid)
        {

            if (isMocked == true)
            {
                return db.checkMockPhysioMembership();
            }
            try
            {
                return await firebase.Child("physio").Child(physioUid).OnceSingleAsync<Membership>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// This method adds a new membership to physio membership field
        /// </summary>
        /// <param name="isMocked"></param>
        /// <param name="physioUid"></param>
        /// <param name="newMembershipDate"></param>
        /// <returns></returns>
        public async Task<bool> renewMembership(bool isMocked, string physioUid,string newMembershipDate)
        {
            if (isMocked == true)
            {
                return db.renewMockMembership(newMembershipDate);
            }
            try
            {
                Physiotherapist physio = await firebase.Child("physio").Child(physioUid).OnceSingleAsync<Physiotherapist>();
                physio.Membership = newMembershipDate;
                await firebase
                .Child("physio").Child(physioUid)
                .PutAsync(new Physiotherapist()
                {
                  PhysioName = physio.PhysioName,
                  Email = physio.Email,
                  IdNumber = physio.IdNumber,
                  Membership = physio.Membership});
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method returns an object list of exercise names from firebase
        /// </summary>
        /// <returns></returns>
        public async Task<List<Exercise>> getAListOfExerciseNames()
        {
            List<Exercise> allExercises = (await firebase
                  .Child("exercises")
                  .OnceAsync<Exercise>()).Select(item => new Exercise
                  {
                      ExerciseName = item.Object.ExerciseName
                  }).ToList();
            return allExercises;
        }
        /// <summary>
        /// This method addes a new plan to a physio plan list
        /// </summary>
        /// <param name="physioUserId"></param>
        /// <param name="category"></param>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="execrcise3"></param>
        /// <param name="imageString"></param>
        /// <param name="exerciseName"></param>
        /// <returns></returns>
        public async Task<bool> addNewPlan(string physioUserId,string category,string exercise1,string exercise2,string execrcise3,string imageString,string exerciseName)
        {
            try
            {
                await firebase
                .Child("newPlans").Child(physioUserId).Child(exerciseName)
                .PutAsync(new Plan()
                {
                   Category = category,
                   exerciseName = exerciseName,
                   Exercise1 = exercise1,
                   Exercise2 = exercise2,
                   Exercise3 = execrcise3,
                   ImageString = imageString
                });
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method returns all exercise plans created by the physio therapist
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<ExercisePlan>> getPhysioExercisePlans(string userId)
        {
            try
            {
                physioPlans = (await firebase
                .Child("newPlans").Child(userId)
                .OnceAsync<ExercisePlan>()).Select(item => new ExercisePlan
                {
                    exerciseName = item.Object.exerciseName,
                    ImageBase64 = item.Object.ImageBase64,
                    Category = item.Object.Category,
                    Exercise1 = item.Object.Exercise1,
                    Exercise2 = item.Object.Exercise2,
                    Exercise3 = item.Object.Exercise3,
                    exerciseListKey = item.Key,
                }).ToList();
                return physioPlans;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// This method updates a physio plan
        /// </summary>
        /// <param name="physioUserid"></param>
        /// <param name="newExerciseName"></param>
        /// <param name="exer1"></param>
        /// <param name="exer2"></param>
        /// <param name="exer3"></param>
        /// <param name="image"></param>
        /// <param name="category"></param>
        /// <param name="orginalExerciseName"></param>
        /// <returns></returns>
        public async Task<bool> updatePhysioPlan(string physioUserid, string newExerciseName, string exer1, string exer2, string exer3, string image, string category, string orginalExerciseName)
        {
            try
            {
                await deletePhysioPlan(physioUserid, orginalExerciseName);
                await addNewPlan(physioUserid, category, exer1, exer2, exer3, image, newExerciseName);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// This method deletes a physio made plan
        /// </summary>
        /// <param name="physioUserid"></param>
        /// <param name="planName"></param>
        /// <returns></returns>
        public async Task<bool> deletePhysioPlan(string physioUserid,string planName)
        {
            try
            {
                await firebase.Child("newPlans").Child(physioUserid).Child(planName).DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return false;
            }
        }
    }
}