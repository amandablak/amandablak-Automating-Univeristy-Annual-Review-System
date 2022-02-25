using System.Text.Json.Serialization;

namespace FacultyAPR.Models.Form
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public enum FormStatus
    {
        Draft,
        Review,
        FacultyAck,
        ToBeSigned,
        Completed,
        Submitted,
    }
}