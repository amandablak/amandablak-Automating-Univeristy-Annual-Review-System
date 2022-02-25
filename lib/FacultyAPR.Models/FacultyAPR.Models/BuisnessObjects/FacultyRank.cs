using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public enum FacultyRank
    {
        Lecturer,
        Instructor,
        SeniorInstructor,
        AssistantProfessor,
        AssociateProfessor,
        Professor
    }
}