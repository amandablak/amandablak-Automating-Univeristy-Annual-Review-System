using System;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models.Form
{
    public static class SectionTypeMethods
    {
        public static SectionType FromString(string sectionType)
        {
            if (sectionType.Equals("TextBox", StringComparison.InvariantCultureIgnoreCase))
            {
                return SectionType.TextBox;
            }
            else if (sectionType.Equals("CompoundTextBox", StringComparison.InvariantCultureIgnoreCase))
            {
                return SectionType.CompundTextBox;
            }
            else if (sectionType.Equals("MultiSelect", StringComparison.InvariantCultureIgnoreCase))
            {
                return SectionType.MultiSelect;
            }
            else if (sectionType.Equals("Radio", StringComparison.InvariantCultureIgnoreCase))
            {
                return SectionType.Radio;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Section type {sectionType} not supported");
            }
        }
    }
}