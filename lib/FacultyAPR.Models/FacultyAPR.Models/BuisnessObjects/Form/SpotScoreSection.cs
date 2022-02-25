using System.Collections.Generic;
using System.Linq;

namespace FacultyAPR.Models.Form
{
    public class SpotScoreSection
    {
        public IEnumerable<SpotScore> Scores { get; set; }
        public string FacultyComment { get; set; }
        public string Review { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SpotScoreSection section &&
                   Scores.SequenceEqual(section.Scores) &&
                   FacultyComment == section.FacultyComment &&
                   Review == section.Review;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}