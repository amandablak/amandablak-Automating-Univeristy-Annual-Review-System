using System;
using System.Collections.Generic;
using System.Linq;

namespace FacultyAPR.Models.Form
{
    public class Section : IEquatable<Section>
    {

        public string SectionTitle { get; set; }

        public string SectionDescription { get; set; }
        
        public SectionType SectionType { get; set; }

        public Guid SectionId { get; set; }

        public Guid GroupId { get; set; }

        public IEnumerable<String> Options { get; set; }

        public int OrderIndex { get; set; }

        public bool Equals(Section other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(SectionTitle, other.SectionTitle, StringComparison.InvariantCultureIgnoreCase) 
                   && string.Equals(SectionDescription, other.SectionDescription, StringComparison.InvariantCultureIgnoreCase) 
                   && SectionType == other.SectionType 
                   && SectionId.Equals(other.SectionId) 
                   && GroupId.Equals(other.GroupId)
                   && Options.SequenceEqual(other.Options);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Section) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(SectionTitle, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add(SectionDescription, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add((int) SectionType);
            hashCode.Add(SectionId);
            hashCode.Add(GroupId);
            hashCode.Add(Options);
            return hashCode.ToHashCode();
        }
    }
}