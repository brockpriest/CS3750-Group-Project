using Assignment13750.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Claims;
using Topshelf;
using Assignment13750.Models;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;


namespace Assignment13750.Pages.Login
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class WelcomeModel : PageModel
    {
        private ICredentialManager credrepo;
        private IClassManager classrepo;
        private ITeacherClassManager credrepoteach;
        private IEnrollmentManager credrepoenroll;

        //public WelcomeModel(ICredentialManager credrepository,ITeacherClassManager teacherManager,IClassManager classes,IEnrollmentManager enrollRepo)
        private IAssignmentManager assignmentRepository;
        private IClassManager classRepository;
        private readonly IMemoryCache _cache;

        public WelcomeModel(ICredentialManager credrepository, ITeacherClassManager teacherManager, IClassManager classes, IAssignmentManager assignmentRepo, IEnrollmentManager enrollRepo, IClassManager classRepo, IMemoryCache memoryCache)
        {
            this.credrepo = credrepository;
            _cache = memoryCache;
            credrepoteach = teacherManager;
            classrepo = classes;
            credrepoenroll = enrollRepo;
            assignmentRepository = assignmentRepo;
            classRepository = classRepo;
        }
        public string firstname { get; set; }
        public string lastname { get; set; }
        [FromRoute]
        public int id { get; set; }
        [BindProperty]
        public Classes Classes { get; set; } = default!;
        public Assignment13750.Models.Credentials userlogin;
        public Assignment13750.Models.Classes teachClass;
        bool isFirstAccess = true;
        public int ClassCount;

        public List<TeacherClasses> TeacherClasses { get; set; }
        public List<Classes> TeachesClasses = new List<Classes> { };

        public List<Classes> studentClasses = new List<Classes> { };
        public List<Enrollment> EnrolledCourses { get; set; }
        public List<Assignment> ToDoAssignments { get; set; } = new List<Assignment>();


        public void OnGet()
        {
            Console.WriteLine("OnGet called");//checks if on get is being called


            if (isFirstAccess)
            {
                id = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
                userlogin = credrepo.GetCredById(id);
                isFirstAccess = false;
            }

            // Check if the data is already cached
            if (!_cache.TryGetValue("ClassesData", out List<Classes> cachedClasses))
            {
                // Data is not in the cache, retrieve and cache it
                if (!userlogin.IsTeacher)
                {
                    EnrolledCourses = credrepoenroll.GetClassesByStudentID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
                    ClassCount = EnrolledCourses.Count();
                    for (int i = 0; i < ClassCount; i++)
                    {
                        studentClasses.Add(classrepo.GetClassById(EnrolledCourses[i].ClassID));
                    }
                }
                if (userlogin.IsTeacher)
                {
                    TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
                    ClassCount = TeacherClasses.Count();
                    for (int i = 0; i < ClassCount; i++)
                    {
                        TeachesClasses.Add(classrepo.GetClassById(TeacherClasses[i].ClassID));
                    }
                }

                // Cache the data for future use
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(7200) // Set an expiration time for the cache, this one is 5 days 7200 min
                };

                _cache.Set("ClassesData", studentClasses, cacheEntryOptions); // Cache the data
            }
            else
            {
                // Data is in the cache, use the cached data
                studentClasses = cachedClasses;
            }

            //to-do assignments for current student
            int studentId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            ToDoAssignments = assignmentRepository.GetAssignmentsByStudentId(studentId);
            // class info for each assignment
            foreach (var assignment in ToDoAssignments)
            {
                assignment.Classes = classRepository.GetClassById(assignment.ClassID);
            }

            //if (isFirstAccess)
            //{
            //    id = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            //    userlogin = credrepo.GetCredById(id);
            //    isFirstAccess = false;
            //}

            //// Check if the data is already cached
            //if (!_cache.TryGetValue("ClassesData", out List<Classes> cachedClasses))
            //{
            //    // Data is not in the cache, retrieve and cache it
            //    if (!userlogin.IsTeacher)
            //    {
            //        EnrolledCourses = credrepoenroll.GetClassesByStudentID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            //        ClassCount = EnrolledCourses.Count();
            //        for (int i = 0; i < ClassCount; i++)
            //        {
            //            studentClasses.Add(classrepo.GetClassById(EnrolledCourses[i].ClassID));
            //        }
            //    }
            //    if (userlogin.IsTeacher)
            //    {
            //        TeacherClasses = credrepoteach.GetClassesByTeacherID(int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name)));
            //        ClassCount = TeacherClasses.Count();
            //        for (int i = 0; i < ClassCount; i++)
            //        {
            //            TeachesClasses.Add(classrepo.GetClassById(TeacherClasses[i].ClassID));
            //        }
            //    }

            //    // Cache the data for future use
            //    var cacheEntryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Set an expiration time for the cache
            //    };

            //    _cache.Set("ClassesData", studentClasses, cacheEntryOptions); // Cache the data
            //}
            //else
            //{
            //    // Data is in the cache, use the cached data
            //    studentClasses = cachedClasses;
            //}

            //// Refresh the cache when the page is accessed
            //if (!isFirstAccess)
            //{
            //    // If it's not the first access, set the same expiration time as when caching
            //    var cacheEntryOptions = new MemoryCacheEntryOptions
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30)
            //    };
            //    _cache.Set("ClassesData", studentClasses, cacheEntryOptions); // Refresh the cache
            //}

            ////to-do assignments for the current student
            //int studentId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.Name));
            //ToDoAssignments = assignmentRepository.GetAssignmentsByStudentId(studentId);
            //// class info for each assignment
            //foreach (var assignment in ToDoAssignments)
            //{
            //    assignment.Classes = classRepository.GetClassById(assignment.ClassID);
            //}

        }
    }
}