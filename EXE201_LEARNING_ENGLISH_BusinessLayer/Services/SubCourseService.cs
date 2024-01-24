using AutoMapper;
using EXE201_LEARNING_ENGLISH_BusinessLayer.Helpers;
using EXE201_LEARNING_ENGLISH_BusinessLayer.IServices;
using EXE201_LEARNING_ENGLISH_DataLayer.Models;
using EXE201_LEARNING_ENGLISH_Repository.IRepository;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;

namespace EXE201_LEARNING_ENGLISH_BusinessLayer.Services
{
    public class SubCourseService : CourseService, ISubCourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IDistributedCache _cache;

        public SubCourseService(IGenericRepository<Course> repository, IMapper mapper, IDistributedCache cache)
            : base(repository, mapper)
        {
            _courseRepository = repository;
            _cache = cache;
        }

        public ICollection<string> GetListEmailOfStudentsInCourse(int courseId)
        {
            ICollection<string> lstEmail = new List<string>();
            try
            {
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
                lstEmail = _courseRepository.Find(x => x.CourseId == courseId)
                                .StudentCourses.Select(a => a.Student.Email).ToList();
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
            }
            catch(Exception ex)
            {
                return lstEmail;
            }
            finally
            {
                lock (this) ;
            }
            return lstEmail;
        }

        public bool SendListEmailOfCourseToTeacher(int courseId)
        {
            try
            {
                var email = Encoding.UTF8.GetString(_cache.Get("-email"));
                //string email = JsonConvert.DeserializeObject<string>(data);
                string lstEmail = "";

                foreach(var e in GetListEmailOfStudentsInCourse(courseId))
                {
                    lstEmail += e + "\n";
                }
                

                SupportFeature.Instance.SendEmail(email, lstEmail, "List Email");

            }catch(Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
