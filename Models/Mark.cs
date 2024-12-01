using MongoDB.Bson.Serialization.Attributes;

namespace StudentMongo.Models
{
    public class Mark
    {
        [BsonId]
        public string Id { get; set; } // Unique Identifier for Mark

        [BsonRequired]
        public string StudentId { get; set; } // Reference to the Student

        public List<SubjectMark> SubjectMarks { get; set; } // List of subjects and their marks

        public double TotalMarks => SubjectMarks.Sum(s => s.MarksObtained);  
        public double MaxTotalMarks => SubjectMarks.Sum(s => s.TotalMarks); 

        public double Percentage => (TotalMarks / MaxTotalMarks) * 100;
    }
}
