using System.Text.Json.Serialization;

namespace FacultyAPR.Models.Form
{
    [JsonConverter(typeof(JsonStringEnumConverter))] 
    public enum SectionType
    {
        TextBox,
        CompundTextBox,
        MultiSelect,
        Radio,
    }
}