using Microsoft.Extensions.Options;
using MongoDB.Driver;
using StudentMongo.Data;
using StudentMongo.Models;

namespace StudentMongo.Services
{
    public class MarkService
    {
        private readonly IMongoCollection<Mark> _marksCollection;

        public MarkService(IMongoClient mongoClient, IOptions<StudentDatabaseSettings> settings)
        {
            var database = mongoClient.GetDatabase(settings.Value.DatabaseName);
            _marksCollection = database.GetCollection<Mark>("Marks"); 
        }

        public async Task<Mark> GetMarksByStudentIdAsync(string studentId)
        {
            return await _marksCollection.Find(mark => mark.StudentId == studentId).FirstOrDefaultAsync();
        }

        public async Task CreateMarkAsync(Mark newMark)
        {
            await _marksCollection.InsertOneAsync(newMark);
        }

        public async Task UpdateMarkAsync(string id, Mark updatedMark)
        {
            await _marksCollection.ReplaceOneAsync(mark => mark.Id == id, updatedMark);
        }

        public async Task<double> GetAveragePercentageByStudentIdAsync(string studentId)
        {
            var marks = await GetMarksByStudentIdAsync(studentId);
            if (marks == null) return 0;

            return marks.Percentage;
        }
    }
}
