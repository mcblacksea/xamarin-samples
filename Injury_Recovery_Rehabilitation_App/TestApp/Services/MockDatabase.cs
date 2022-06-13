/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 */
using System;
using System.Collections.Generic;
using TestApp.models;
using TestApp.Models;

namespace TestApp.Services
{
    /// <summary>
    /// This class mocks data that represents the data from the real database
    /// </summary>
    public class MockDatabase
    {
        /// <summary>
        /// This method returns a list exercisePlans
        /// </summary>
        /// <returns></returns>
        public List<ExercisePlan> GetAllMockExercises()
        {
            List<ExercisePlan> plans = new List<ExercisePlan>()
            {
                new ExercisePlan(){
                    exerciseName = "Knee pain",
                    Category = "Lower Extremity",
                    Exercise1 = "Heel and calf stretch",
                    Exercise2 = "Hamstring stretch"
                },
                new ExercisePlan(){
                    exerciseName = "Bells palsy",
                    Category = "Face",
                    Exercise1 = "Stretch Cheeks",
                    Exercise2 = "Massage face"
                },
            };
            return plans;
        }
        /// <summary>
        /// This method returns a single exercise from a list of exercises using an exercise key
        /// </summary>
        /// <param name="exerciseKey"></param>
        /// <returns></returns>
        public ExercisePlan GetMockExercise(String exerciseKey)
        {
            List<ExercisePlan> plans = GetAllMockExercises();

            foreach (ExercisePlan plan in plans)
            {
                if (plan.exerciseName == exerciseKey)
                {
                    return plan;
                }
            }
            return null;
        }
        /// <summary>
        /// This method inserts a new patient entry into the patient database using the patientUid as the key
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="name"></param>
        /// <param name="gender"></param>
        /// <param name="injuryType"></param>
        /// <param name="injuryOccurred"></param>
        /// <param name="age"></param>
        /// <param name="injurySeverity"></param>
        /// <param name="exerPlan"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool AddMockPatient(string patientUid, string name, string gender, string injuryType, string injuryOccurred, int age, int injurySeverity,string exercise1, string exercise2, string exercise3, string email)
        {
            Patient newPatient = null;
            newPatient = new Patient()
            {
                PatientName = name
            };
            if (newPatient.PatientName != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// This inserts a mock physio into a a list of physios
        /// </summary>
        /// <param name="physioUid"></param>
        /// <param name="name"></param>
        /// <param name="physioIdNumber"></param>
        /// <param name="physioEmail"></param>
        /// <param name="membership"></param>
        /// <param name="isMocked"></param>
        /// <returns></returns>
        public bool AddMockPhysio(string physioUid, string name, string physioIdNumber, string physioEmail, string membership)
        {
            List<Physiotherapist> physio = new List<Physiotherapist>()
            {
                new Physiotherapist()
                {
                    PhysioName = name,
                    IdNumber = physioIdNumber,
                    Email = physioEmail,
                    Membership = membership,
                }
            };
            return true;
        }
        /// <summary>
        /// Addeds a patient user id and a data encryption key to a list
        /// </summary>
        /// <param name="patientUid"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public bool AddMockEncryptionKeyToUserId(string patientUid, string encryptionKey)
        {
            List<Encryption> physio = new List<Encryption>()
            {
                new Encryption()
                {
                   EncryptionKey = encryptionKey,
                   PatientUid = patientUid
                }
            };
            return true;
        }
        /// <summary>
        /// This method retuns a list of Encryption keys assigned to patient data
        /// </summary>
        /// <returns></returns>
        public List<Encryption> getMockPatientEncryptionKeys()
        {
            List<Encryption> keys = new List<Encryption>()
            {
                new Encryption()
                {
                   EncryptionKey = "ad1h3vdh3udns",
                   PatientUid = "qEgm34Ybskan1sfdns"
                },
                new Encryption()
                {
                   EncryptionKey = "bd1h3vdd3udns",
                   PatientUid = "xEgm34sbskan1sfdns"
                }
            };
            return keys;
        }
        /// <summary>
        /// This method returns a encryption key for decrypting data
        /// </summary>
        /// <returns></returns>
        public string getMockKeyToData()
        {
            List<Encryption> keyList = getMockPatientEncryptionKeys();
            return keyList[0].EncryptionKey;
        }
        /// <summary>
        /// This function adds the patient unique identifier to the physios patient list
        /// </summary>
        /// <param name="PhysioUid"></param>
        /// <param name="PatientUID"></param>
        /// <returns></returns>
        public bool AddMockPatientUIDToPatientList(string PhysioUid, string PatientUID)
        {
            List<MockPhysio> physioList = new List<MockPhysio>()
            {
                new MockPhysio{PhysioName = "john Denver",Email = "johnDenver@gmail.com",physioUid = "adcd4321"}
            };

            foreach (MockPhysio physio in physioList)
            {
                if (PhysioUid == physio.physioUid)
                {
                    physio.patientList.Add(PatientUID);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// This method returns a list of encrypted names and user Ids
        /// </summary>
        /// <returns></returns>
        public List<PatientList> GetMockNamesAndPatientUids()
        {
            List<PatientList> patientList = new List<PatientList>()
            {
                new PatientList()
                {
                   PatientName = "Dagshqudbsjqa",
                   PatientUid = "qEgm34Ybskan1sfdns"
                },
                new PatientList()
                {
                   PatientName = "fagshqsaudfsjqa",
                   PatientUid = "xEgm34sbskan1sfdns"
                }
            };
            return patientList;
        }

        /// <summary>
        /// This method adds a type of user to user id
        /// </summary>
        /// <param name="userUid"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        public bool AddMockUserType(string userType)
        {
            CheckUser user = new CheckUser()
            {
                User = userType
            };
            return true;
        }
        /// <summary>
        /// This method assigns a progress plan dictionary to another dictionary to save a patients progress
        /// </summary>
        /// <param name="dates"></param>
        public bool recordMockPatientProgress(Dictionary<string, bool> dates)
        {
            Dictionary<string, bool> newDictionary = new Dictionary<string, bool>();
            newDictionary = dates;
            return true;
        }
        /// <summary>
        /// This method returns a patients progress in the form of a dictionary
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, bool> getMockPatientProgress()
        {
            Dictionary<string, bool> patientPlan = new Dictionary<string, bool>();
            patientPlan.Add("21-02-2022", false);
            patientPlan.Add("22-02-2022", false);
            patientPlan.Add("23-02-2022", true);
            return patientPlan;
        }
        /// <summary>
        /// This method returns a patient assigned to a user id
        /// </summary>
        /// <returns></returns>
        public Patient getMockpatientDetails()
        {

            Patient patient = new Patient()
            {
                Age = 22,
                PatientName = "asdshdjdjjd",
                Email = "aasdgddhhejd",
                Exer1 = "Squat",
                Exer2 = "Stretch",
                Exer3 = "Calf Stretch",
                Gender = "Male",
                maxExercise1 = 4,
                maxExercise2 = 2,
                maxExercise3 = 5,
                minExercise1 = 2,
                minExercise2 = 1,
                minExercise3 =3,
                InjuryOccurred = "Dancing",
                InjurySeverity = 4,
                InjuryType = "sprain"
            };
            return patient;
        }

        /// <summary>
        /// This method returns the type of user assigned to the user id
        /// </summary>
        /// <returns></returns>
        public CheckUser getMockTypeOfUser(string userId)
        {
            if(userId != null)
            {
                CheckUser user = new CheckUser()
                {
                    User = "patient"
                };
                return user;
            }
            else
            {
                return null;
            }
            
        }

        /// <summary>
        /// Returns a list of exercises
        /// </summary>
        /// <param name="exercise1"></param>
        /// <param name="exercise2"></param>
        /// <param name="exercise3"></param>
        /// <returns></returns>
        public List<Exercise> GetMockPatientExercises()
        {
            List<Exercise> exerciseList = new List<Exercise>()
           {
               new Exercise()
               {
                   ExerciseName = "Squat",
                   exerciseVideoCopyright = "sportsDirect",
                   VideoLink = "afsgwjskdkd"
               }
           };
           return exerciseList;
        }
        /// <summary>
        /// This method returns a video download link for a video
        /// </summary>
        /// <param name="videoName"></param>
        /// <returns></returns>
        public string GetMockVideosFromStorage(string videoName)
        {
            Dictionary<string, string> videoDownloadLink = new Dictionary<string, string>();
            videoDownloadLink.Add("key1", "http://downloadLink");
            return videoDownloadLink[videoName];
        }
        /// <summary>
        /// This method creates a new login for ios users
        /// </summary>
        /// <param name="salted"></param>
        /// <param name="saltedAndHashed"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public bool iOSMockSignupWithEmailPassword(string salted, string saltedAndHashed, string uid)
        {
            IosCredentials ios = new IosCredentials()
            {
                salt = salted,
                saltHashed = saltedAndHashed,
                userId = uid,
            };
            return true;
        }
        /// <summary>
        /// This method returns details about an Ios sign in for the login
        /// </summary>
        /// <returns></returns>
        public IosCredentials getMockIosPatientPasswordDetails()
        {
            IosCredentials ios = new IosCredentials()
            {
                salt = "asfdhw336whus",
                saltHashed = "DD44333A9D329205F56081DF154CE3A6",
                userId = "1afdu46dhds",
            };
            return ios;
        }
        /// <summary>
        /// This method returns a membership tied to a physio
        /// </summary>
        /// <returns></returns>
        public Membership checkMockPhysioMembership()
        {
            Membership membership = new Membership()
            {
                MembershipDate = "2022-05-23T00:00:00"
            };
            return membership;
        }
        /// <summary>
        /// This method sets a new membership date to a physio
        /// </summary>
        /// <param name="newMembershipDate"></param>
        /// <returns></returns>
        public bool renewMockMembership(string newMembershipDate)
        {
            Membership membership = new Membership()
            {
                MembershipDate = newMembershipDate
            };
            return true;
        }
    }
}