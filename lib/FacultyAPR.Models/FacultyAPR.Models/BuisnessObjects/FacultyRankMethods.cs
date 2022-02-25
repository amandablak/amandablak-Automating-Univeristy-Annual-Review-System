using System;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    
    public static class FacultyRankMethods
    {
        public static FacultyRank FromString(string facultyRank)
        {
            if (facultyRank.Equals("Professor", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.Professor;
            }
            else if (facultyRank.Equals("AssociateProfessor", StringComparison.InvariantCultureIgnoreCase) 
                || facultyRank.Equals("Associate Professor", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.AssociateProfessor;
            }
            else if (facultyRank.Equals("AssistantProfessor", StringComparison.InvariantCultureIgnoreCase) 
                || facultyRank.Equals("Assistant Professor", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.AssistantProfessor;
            }
            else if (facultyRank.Equals("SeniorInstructor", StringComparison.InvariantCultureIgnoreCase) 
                || facultyRank.Equals("Senior Instructor", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.SeniorInstructor;
            }
            else if (facultyRank.Equals("Instructor", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.Instructor;
            }
            else if (facultyRank.Equals("Lecturer", StringComparison.InvariantCultureIgnoreCase))
            {
                return FacultyRank.Lecturer;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Faculty Rank {facultyRank} not currently supported");
            }
        }
    }
}