using System;

namespace FacultyAPR.Models.Form
{
    public class SectionContent : IEquatable<SectionContent>
    {
        public Guid Id { get; set; }

        public string Content { get; set; }

        public DateTimeOffset Updated { get; set; }

        public bool Equals(SectionContent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id)
                   && string.Equals(Content, other.Content, StringComparison.CurrentCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SectionContent) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Content, StringComparer.CurrentCultureIgnoreCase);
            hashCode.Add(Updated);
            return hashCode.ToHashCode();
        }
    }
}