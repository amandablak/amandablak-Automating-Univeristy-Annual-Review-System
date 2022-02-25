using System;
using System.Collections.Generic;
using FacultyAPR.Models;
using FacultyAPR.Models.Form;
namespace FacultyAPR.Testing.Utilities
{
    public static class FormDataGenerators
    {
        public static FormStructure GenerateFormStructure(Guid formId, Guid sectionId)
        {
            var structure = new FormStructure();
            structure.FormId = formId;
            var groups = new List<Group>();
            structure.FormYear = "2020";
            structure.Rank = FacultyRank.Professor;
            var group = new Group();
            groups.Add(group);
            structure.Groups = groups;
            group.GroupId = Guid.NewGuid();
            group.Title = "A Very Interesting Title";
            group.Description = "Something about the form";
            group.OrderIndex = 1;
            var sections = new List<Section>();
            var section = new Section();
            section.SectionTitle = "Section title";
            section.SectionDescription = "Description of section";
            section.SectionType = SectionType.TextBox;
            section.SectionId = sectionId;
            section.GroupId = group.GroupId;
            section.OrderIndex = 1;
            section.Options = new List<string> {""};
            sections.Add(section);
            group.Sections = sections;
            return structure;
        }

        public static FormContent GenerateFormContent(Guid formId, Guid sectionId)
        {
            var content = new FormContent();
            content.Modified = DateTimeOffset.Now;
            content.State = FormStatus.FacultyAck;
            content.FormId = formId;
            content.FacultyId = Guid.NewGuid();
            content.ReviewerId = Guid.NewGuid();
            content.FormLevelComment = "Mostly good, a few revisions needed";
            var sectionContent = new List<SectionContent>();
            var section = new SectionContent();
            section.Content = "Some content!";
            section.Id = sectionId;
            section.Updated = DateTimeOffset.UtcNow;
            sectionContent.Add(section);
            content.FacultyContent = sectionContent;
            var reviewContent = new List<FacultyComment>();
            var facComment = new FacultyComment();
            facComment.Comments = "A revision is needed";
            facComment.Updated = DateTimeOffset.UtcNow;
            facComment.GroupId = Guid.NewGuid();
            reviewContent.Add(facComment);
            content.ReviewContent = reviewContent;
            var scores = new SpotScoreSection();
            scores.Review = "asd";
            scores.FacultyComment = "asdf";
            var score = new SpotScore();
            score.MeanValue = 99.0;
            score.Course = "CSPSC 2500-01";
            score.PercentRespondents = 10;
            score.Question = "asd";
            scores.Scores = new List<SpotScore>{score};
            content.Scores = scores;
            return content;
        }
    }
}
