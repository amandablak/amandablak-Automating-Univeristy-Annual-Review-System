using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using FacultyAPR.Models.Form;

namespace FacultyAPR.Models.Form
{
    public class Group: IEquatable<Group>
    {
        public Guid GroupId { get; set; }

        public string Title { get; set; }

        public string Description { get; set;}
        
        public IEnumerable<Section> Sections { get; set; }

        public int OrderIndex { get; set; }
        

        public bool Equals(Group other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GroupId.Equals(other.GroupId) 
                   && string.Equals(Title, other.Title, StringComparison.InvariantCultureIgnoreCase) 
                   && string.Equals(Description, other.Description, StringComparison.InvariantCultureIgnoreCase) 
                   && Sections.SequenceEqual(other.Sections);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(GroupId);
            hashCode.Add(Title, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add(Description, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add(Sections);
            return hashCode.ToHashCode();
        }
    }
}