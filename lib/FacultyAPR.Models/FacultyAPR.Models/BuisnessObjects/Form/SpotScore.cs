namespace FacultyAPR.Models.Form
{
    public class SpotScore 
    {
        public string Question { get; set; }
        
        public string Course { get; set; }
        
        public double PercentRespondents { get; set; }
        
        public double MeanValue { get; set; }

        public override bool Equals(object obj)
        {
            return obj is SpotScore score &&
                   Question == score.Question &&
                   Course == score.Course &&
                   PercentRespondents == score.PercentRespondents &&
                   MeanValue == score.MeanValue;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}