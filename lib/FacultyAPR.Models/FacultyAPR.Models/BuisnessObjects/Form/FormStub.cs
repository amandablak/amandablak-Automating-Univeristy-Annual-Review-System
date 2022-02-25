using System;
using System.Collections.Generic;
using System.Linq;

namespace FacultyAPR.Models.Form
{
    public class FormStub
    {
        public Guid FacultyId { get; set; }

        public Guid FormId { get; set; }

        public FormStatus State { get; set; }

        public string FormYear { get; set;}

        public FacultyRank Rank { get; set; }
    }
}