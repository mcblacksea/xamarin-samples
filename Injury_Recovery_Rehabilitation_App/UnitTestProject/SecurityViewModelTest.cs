/*
 * Student Name Daniel Dinelli
 * Student Number C00242741
 * looked at https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test
 */
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using TestApp.Models;
using TestApp.ViewModels;
using Xunit;

namespace UnitTestProject
{
    public class SecurityViewModelTest
    {
        SecurityViewModel security = new SecurityViewModel();
        [Fact]
        public void generateSaltOrPasswordOrUidTest()
        {
            string result = security.generateSaltOrPasswordOrUid(20);
            int expected = 40;
            Assert.Equal(expected, result.Length);

        }
        [Fact]
        public void md5HashAndSaltThePasswordTest()
        {
            var sourceBytes = Encoding.UTF8.GetBytes("asfdhw336whus" + "GOODpASS45678");
            var md5Hash = MD5.Create();
            var hashedString = md5Hash.ComputeHash(sourceBytes);
            string expected = BitConverter.ToString(hashedString).Replace("-", "");
            string result = security.md5HashAndSaltThePassword("asfdhw336whus", "GOODpASS45678");
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void checkIfLoginIsVerifiedTestFails()
        {
            string result = await security.checkIfLoginIsVerified("john@gmail.com", "password1234", true);
            string expected = "";
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void checkIfLoginIsVerifiedTestPasses()
        {
            string result = await security.checkIfLoginIsVerified("john@gmail.com", "GOODpASS45678", true);
            string expected = "1afdu46dhds";
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void checkUserTypeTestPasses()
        {
            string result = await security.checkUserType("A1wesf2gs", true);
            string expected = "patient";
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void checkUserTypeTestFails()
        {
            string result = await security.checkUserType(null, true);
            string expected = "";
            Assert.Equal(expected, result);
        }
        [Fact]
        public async void encryptDecryptDataTest()
        {
            string encryptionKey = security.generateSaltOrPasswordOrUid(32);
            string name = "John Dooly";
            string cipherText = security.encryptData(name, encryptionKey);
            string plainTextName = security.decryptData(cipherText, encryptionKey);
            string expected = "John Dooly";
            Assert.Equal(expected, plainTextName);
        }
        [Fact]
        public async void getPatientEncryptionKeysTest()
        {
            List<Encryption> patientKeyList = await security.getPatientEncryptionKeys(true);
            int expected = 0;
            Assert.NotEqual(expected, patientKeyList.Count);
        }
        [Fact]
        public async void getPhysioMembershipTest()
        {
            DateTime membership = await security.getPhysioMembership(true,"1Asfdgwy5");
            Assert.NotEqual(membership, new DateTime(1988, 03, 30));
        }
        [Fact]
        public async void renewMembershipTest()
        {
            bool result = await security.renewMembership(true,"1acsF34se","2022/02/16");
            Assert.True(result);
        }
        [Fact]
        public async void getEncryptionKeyTest()
        {
            string result = await security.getEncryptionKey(true, "facsF34se");
            Assert.NotNull(result);
        }
        [Fact]
        public async void decyptPatientNamesTest()
        {
            List<PatientList> patientDetails = new List<PatientList>()
            {
                new PatientList()
                {
                    PatientName = "RwuO9dBCfVVZhPKCKrGGHA==",
                    PatientUid = "1asw2gshjcdk"
                }
            };
            List<Encryption> keys = new List<Encryption>()
            {
                new Encryption()
                {
                    PatientUid = "1asw2gshjcdk",
                    EncryptionKey = "D5C92D089939D48926E778EDCD2FC02057AC6411C8D219327FFF33FA7C8178CC"
                }
            };
            List<PatientList> details = await security.decyptPatientNames(patientDetails,keys);
            string expected = "John Dooly";
            Assert.Equal(expected, details[0].PatientName);
        }
    }
}
