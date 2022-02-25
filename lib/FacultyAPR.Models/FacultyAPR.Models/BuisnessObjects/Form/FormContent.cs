using System;
using System.Collections.Generic;
using System.Linq;

namespace FacultyAPR.Models.Form
{
    public class FormContent
    {
        public Guid FacultyId { get; set; }

        public Guid FormId { get; set; }
        
        public Guid ReviewerId { get; set; }

        public IEnumerable<SectionContent> FacultyContent { get; set;}

        public IEnumerable<FacultyComment> ReviewContent { get; set;}

        public string FormLevelComment { get; set; }

        public FormStatus State { get; set; }

        public DateTimeOffset  Modified { get; set;}

        public SpotScoreSection Scores { get; set; }

        public bool Equals(FormContent other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return FacultyId.Equals(other.FacultyId)
                   && FormId.Equals(other.FormId)
                   && FacultyContent.SequenceEqual(other.FacultyContent)
                   && ReviewContent.SequenceEqual(other.ReviewContent)
                   && string.Equals(FormLevelComment, other.FormLevelComment,
                       StringComparison.InvariantCultureIgnoreCase)
                   && State == other.State
                   && Scores.Equals(other.Scores);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormContent) obj);
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(FacultyId);
            hashCode.Add(FormId);
            hashCode.Add(ReviewerId);
            hashCode.Add(FacultyContent);
            hashCode.Add(ReviewContent);
            hashCode.Add(FormLevelComment, StringComparer.CurrentCultureIgnoreCase);
            hashCode.Add((int) State);
            hashCode.Add(Modified);
            hashCode.Add(Scores);
            return hashCode.ToHashCode();
        }
    }
}