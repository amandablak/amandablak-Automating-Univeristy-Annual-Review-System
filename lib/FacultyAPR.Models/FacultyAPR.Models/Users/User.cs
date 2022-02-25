
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    public class User: IUser, IEquatable<User>
    {
        public Guid? Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string EmailAddress { get; set;}
        
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserType UserType { get; set;}

        public bool Equals([AllowNull] User other)
        {
            return other != null
            && this.Id.Equals(other.Id)
            && this.FirstName.Equals(other.FirstName)
            && this.LastName.Equals(other.LastName)
            && this.EmailAddress.Equals(other.EmailAddress)
            && this.UserType == other.UserType;
        }
        
        public override bool Equals(object other)
        {
            return other is User user
                   && this.Equals(user);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
    
}