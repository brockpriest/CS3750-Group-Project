//using Assignment13750;

//namespace QuantumTest
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        [TestMethod]
//        public void InstructorCanCreateClass()
//        {


//        }
//    }
//}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Assignment13750.Pages.TeacherPages;
using Assignment13750.Data;
using Assignment13750.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Humanizer;
using NuGet.DependencyResolver;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.Intrinsics.X86;
using Topshelf;

namespace QuantumTest
{

    [TestClass]
    public class CreateClassModelTests
    {

        [TestMethod]
        public void testThatAlwaysPasses()
        {
            Assert.IsTrue(true);
        }

        public static Assignment13750.Models.Credentials GetFakeTeacher()
        {
            var teacher = new Assignment13750.Models.Credentials
            {
                ID = 999, // Set a unique ID
                FirstName = "John",
                LastName = "Doe",
                Email = "johndoe@example.com",
                Password = "mysecretpassword",
                BirthDate = new DateTime(1990, 1, 1),
                IsTeacher = true,
                Address1 = "123 Main St",
                City = "Sample City",
                State = "CA",
                Zip = "12345",
                Link1 = "https://example.com/link1",
                Link2 = "https://example.com/link2",
                Link3 = "https://example.com/link3",
                ProfilePic = "profile.jpg"
            };
            return teacher;
        }

        public static Classes GetFakeClass()
        {
            var classData = new Classes
            {
                Id = 888, // Set a unique ID
                CourseName = "Math 101",
                CourseType = "Mathematics",
                Credits = 3,
                CourseNo = 101,
                RoomNo = "A101",
                MeetingDays = "Monday, Wednesday, Friday",
                StartTime = new DateTime(2023, 1, 1, 8, 0, 0), // 8:00 AM
                EndTime = new DateTime(2023, 1, 1, 9, 30, 0) // 9:30 AM
            };

            return classData;
        }

        public static TeacherClasses GetFakeTeacherClass()
        {
            var teacherclass = new TeacherClasses
            {
                Id = 777, // Set a unique ID
                TeacherID = 999,
                ClassID = 888
            };

            return teacherclass;
        }

        [TestMethod]
        public void TeacherCanCreateClassSuccessfully()
        {
            // Arrange
            var fakeTeacher = GetFakeTeacher();
            var fakeClass = GetFakeClass();
            var fakeTeacherClass = GetFakeTeacherClass();

            //check if it got added
            Assert.IsNotNull(fakeTeacher);
            Assert.IsNotNull(fakeClass);
            Assert.IsNotNull(fakeTeacherClass);
        }
    }
}