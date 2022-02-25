using System;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    
    public static class FacultyUserType
    {
        public static UserType FromString(string facultyUserType)
        {
            if (facultyUserType.Equals("Dean", StringComparison.InvariantCultureIgnoreCase))
            {
                return UserType.Dean;
            }
            else if (facultyUserType.Equals("Admin", StringComparison.InvariantCultureIgnoreCase))
            {
                return UserType.Admin;
            }
            else if (facultyUserType.Equals("Faculty", StringComparison.InvariantCultureIgnoreCase))
            {
                return UserType.Faculty;
            }
            else if (facultyUserType.Equals("FacultyChair", StringComparison.InvariantCultureIgnoreCase) 
                || UserType.Equals("Faculty Chair", StringComparison.InvariantCultureIgnoreCase))
            {
                return UserType.FacultyChair;
            
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Faculty User {facultyUserType} not currently supported");
            }
        }
    }
}