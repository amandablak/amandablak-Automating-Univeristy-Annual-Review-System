using System;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    public interface IUser
    {
        Guid? Id { get; set; }

        string FirstName { get; set; }
        
        string LastName { get; set; }

        string EmailAddress { get; set;}

        [JsonConverter(typeof(JsonStringEnumConverter))]
        UserType UserType { get; set;}
    }
}