using AutoMapper;
using Microservice.Shared.Dtos;
using Microservices.Services.Catalog.Dtos.CourseDtos;
using Microservices.Services.Catalog.Models;
using Microservices.Services.Catalog.Settings;
using Microsoft.AspNetCore.Razor.Hosting;
using MongoDB.Driver;

namespace Microservices.Services.Catalog.Services.CourseServices
{
    public class CourseService: ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _mapper = mapper;
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category =await _categoryCollection.Find(x=>x.Id==item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course == null)
            {
                return Response<CourseDto>.Fail("Course not found!", 404);
            }
            course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllCoursesByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find<Course>(x=>x.UserId == userId).ToListAsync();
            if (courses.Any())
            {
                foreach (var item in courses)
                {
                    item.Category = await _categoryCollection.Find(x => x.Id == item.CategoryId).FirstAsync();
                }
            }
            else
            {
                courses = new List<Course>();
            }

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>>CreateAsync(CreateCourseDto createCourseDto)
        {
            var newCourse = _mapper.Map<Course>(createCourseDto);
            newCourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newCourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newCourse), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(UpdateCourseDto updateCourseDto)
        {
            var updateCourse = _mapper.Map<Course>(updateCourseDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == updateCourseDto.Id, updateCourse);
            if (result==null)
            {
                return Response<NoContent>.Fail("Course not found!", 404);
            }
            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(x=>x.Id==id);
            if (result.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Course not found!", 404);
            }
        }
    }
}
