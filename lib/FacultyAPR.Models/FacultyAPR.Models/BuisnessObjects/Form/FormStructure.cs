using System;
using System.Collections.Generic;
using System.Linq;

namespace FacultyAPR.Models.Form
{
    public class FormStructure : IEquatable<FormStructure>
    {
        public Guid FormId {get; set;}

        public IEnumerable<Group> Groups { get; set; }

        public string FormYear { get; set;}

        public FacultyRank Rank { get; set; }

        public bool Equals(FormStructure other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FormId.Equals(other.FormId) 
                   && Groups.SequenceEqual(other.Groups) 
                   && string.Equals(FormYear, other.FormYear, StringComparison.InvariantCultureIgnoreCase) 
                   && Rank == other.Rank;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormStructure) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(FormId);
            hashCode.Add(Groups);
            hashCode.Add(FormYear, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add((int) Rank);
            return hashCode.ToHashCode();
        }
    }
}