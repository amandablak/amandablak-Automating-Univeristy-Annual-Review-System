using System;

namespace FacultyAPR.Models
{
    public class FacultyUser: IUser
    {
        public Guid? Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set;}

        public FacultyRank Rank { get; set; }
        
        public CollegeDepartment Department { get; set; }

        public UserType UserType { get; set;}
    }
}