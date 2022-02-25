using System;

namespace FacultyAPR.Models.Form
{
    public class FacultyComment : IEquatable<FacultyComment>
    {
        public Guid GroupId { get; set; }

        public string Comments { get; set; }

        public DateTimeOffset Updated { get; set; }

        public bool Equals(FacultyComment other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return GroupId.Equals(other.GroupId)
                   && string.Equals(Comments, other.Comments, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FacultyComment) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(GroupId);
            hashCode.Add(Comments, StringComparer.InvariantCultureIgnoreCase);
            hashCode.Add(Updated);
            return hashCode.ToHashCode();
        }
    }
}