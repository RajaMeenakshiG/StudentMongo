using MongoDB.Driver;
using StudentMongo.Models;

namespace StudentMongo.Services
{
    public class StudentDashboardService
    {
        private readonly IMongoCollection<Student> _studentsCollection;

        public StudentDashboardService(IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase("SchoolDB");
            _studentsCollection = database.GetCollection<Student>("Students");
        }

        public long GetTotalStudents()
        {
            return _studentsCollection.CountDocuments(student => true);
        }

        public Dictionary<string, int> GetStudentsByGrade()
        {
            var result = _studentsCollection
                .Aggregate()
                .Group(
                    student => student.Grade,
                    group => new { Grade = group.Key, Count = group.Count() }
                )
                .ToList();

            return result.ToDictionary(item => item.Grade, item => item.Count);
        }

        public List<Student> GetStudentsOlderThan(int age)
        {
            return _studentsCollection.Find(student => student.Age > age).ToList();
        }
    }
}
