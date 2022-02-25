﻿using System;
using System.Data;
using System.Threading.Tasks;
using FacultyAPR.Models.Form;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using MySqlConnector;
using FacultyAPR.Models;
using System.Text.Json;

namespace FacultyAPR.Storage.Sql
{
    public class SqlFormStore : IFormStructureStore, IFormContentStore
    {
        async Task<FormStructure> IFormStructureStore.Create(FormStructure formPayload)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                try
                {

                    const string createForm = "INSERT INTO Form(FormId, FacRank, FormYear) VALUES (@id, @facRank, @year)";
                    using (var command = new MySqlCommand(createForm, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formPayload.FormId;
                        command.Parameters.Add("@facRank", DbType.String).Value = Enum.GetName(typeof(FacultyRank), formPayload.Rank);
                        command.Parameters.Add("@year", DbType.String).Value = formPayload.FormYear;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    const string createGroup = "INSERT INTO FormGroup(FormId, GroupId, Title, DescriptionText, OrderIndex) VALUES(@id, @groupId, @title, @description, @orderIndex);";
                    foreach(var group in formPayload.Groups)
                    {   
                        using (var command = new MySqlCommand(createGroup, connection))
                        {
                            command.Parameters.Add("@id", DbType.Guid).Value = formPayload.FormId;
                            command.Parameters.Add("@groupId", DbType.Guid).Value = group.GroupId;
                            command.Parameters.Add("@title", DbType.String).Value = group.Title;
                            command.Parameters.Add("@orderIndex", DbType.Int32).Value = group.OrderIndex;
                            command.Parameters.Add("@description", DbType.String).Value = group.Description;
                            command.Transaction = trans;
                            var result = await command.ExecuteNonQueryAsync(); 
                        }
                        foreach (var section in group.Sections)
                        {
                            const string getSectionTypeId = "Select Id From SectionTypeTable WHERE TypeName = @typeName";
                            int id;
                            using (var command = new MySqlCommand(getSectionTypeId, connection))
                            {
                                command.Parameters.Add("@typeName", DbType.String).Value = Enum.GetName(typeof(SectionType), section.SectionType);
                                command.Transaction = trans;
                                using (var reader = await command.ExecuteReaderAsync())
                                {
                                    await reader.ReadAsync();
                                    id = reader.GetInt32(0);
                                    await reader.CloseAsync();
                                }
                            }
                            const string createSection = "INSERT INTO Section(FormID,SectionID,SectionType,SectionTitle,SectionDescription,GroupId,Options, OrderIndex) "
                            + " VALUES(@id, @sectionId, @sectionType, @title, @description, @groupId, @options, @orderIndex)";
                            using (var command = new MySqlCommand(createSection, connection))
                            {
                                command.Parameters.Add("@id", DbType.Guid).Value = formPayload.FormId;
                                command.Parameters.Add("@sectionId", DbType.Guid).Value = section.SectionId;
                                command.Parameters.Add("@sectionType", DbType.String).Value = id;
                                command.Parameters.Add("@title", DbType.String).Value = section.SectionTitle;
                                command.Parameters.Add("@description", DbType.String).Value = section.SectionDescription;
                                command.Parameters.Add("@groupId", DbType.Guid).Value = group.GroupId;
                                command.Parameters.Add("@options", DbType.String).Value = SqlUtils.OptionsArrayToString(section.Options);
                                command.Parameters.Add("@orderIndex", DbType.Int32).Value = section.OrderIndex;
                                command.Transaction = trans;
                                await command.ExecuteNonQueryAsync(); 
                            }
                        }
                    }
                    
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    // Logging?
                    throw new FormStoreValidationException(e.Message);
                }
            }
            return formPayload;
        }

        async Task<FormStructure> IFormStructureStore.Update(FormStructure formPayload)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                try
                {
                    const string getFormId = "Select FormId From Form WHERE FacRank = @facRank and FormYear = @year";
                    using (var command = new MySqlCommand(getFormId, connection))
                    {
                        command.Parameters.Add("@facRank", DbType.String).Value = Enum.GetName(typeof(FacultyRank), formPayload.Rank);
                        command.Parameters.Add("@year", DbType.String).Value = formPayload.FormYear;
                        command.Transaction = trans;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            await reader.ReadAsync();
                            formPayload.FormId = reader.GetGuid(0);
                            await reader.CloseAsync();
                        }
                    }
                    const string updateGroup = "UPDATE FormGroup SET Title = @title, DescriptionText = @description WHERE FormId = @id AND GroupId = @groupId AND OrderIndex = @orderIndex";
                    foreach (var group in formPayload.Groups)
                    {
                        using (var command = new MySqlCommand(updateGroup, connection))
                        {
                            command.Parameters.Add("@title", DbType.String).Value = group.Title;
                            command.Parameters.Add("@description", DbType.String).Value = group.Description;
                            command.Parameters.Add("@id", DbType.Guid).Value = formPayload.FormId;
                            command.Parameters.Add("@groupId", DbType.Guid).Value = group.GroupId;
                            command.Parameters.Add("@orderIndex", DbType.Int32).Value = group.OrderIndex;
                            command.Transaction = trans;
                            var result = await command.ExecuteNonQueryAsync();
                        }
                        const string getSectionTypeId = "Select Id From SectionTypeTable WHERE TypeName = @typeName";
                        foreach (var section in group.Sections)
                        {
                            int id;
                            using (var command = new MySqlCommand(getSectionTypeId, connection))
                            {
                                command.Parameters.Add("@typeName", DbType.String).Value = Enum.GetName(typeof(SectionType), section.SectionType);
                                command.Transaction = trans;
                                using (var reader = await command.ExecuteReaderAsync())
                                {
                                    await reader.ReadAsync();
                                    id = reader.GetInt32(0);
                                    await reader.CloseAsync();
                                }
                            }
                            const string updateSection = "UPDATE Section SET SectionTitle = @title, SectionDescription = @description Where FormId = @id AND SectionId = @sectionId AND OrderIndex = @orderIndex";
                            using (var command = new MySqlCommand(updateSection, connection))
                            {
                                command.Parameters.Add("@title", DbType.String).Value = section.SectionTitle;
                                command.Parameters.Add("@description", DbType.String).Value = section.SectionDescription;
                                command.Parameters.Add("@id", DbType.Guid).Value = formPayload.FormId;
                                command.Parameters.Add("@sectionId", DbType.Guid).Value = id;
                                command.Parameters.Add("@orderIndex", DbType.Int32).Value = section.OrderIndex;
                                command.Transaction = trans;
                                await command.ExecuteNonQueryAsync();
                            }

                        }
                    }
                    
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    throw;

                }
            }
            return formPayload;
        }


        async Task<bool> IFormStructureStore.Delete(Guid formId)
        {
            var succeeded = true;
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                 
                try
                {
                     const string sqlStatement = "DELETE FROM Form WHERE FormId = @id;";
                     using (var command = new MySqlCommand(sqlStatement, connection))
                     {
                         command.Parameters.Add("@id", DbType.Guid).Value = formId;
                         command.Transaction = trans;
                         await command.ExecuteNonQueryAsync();
                         
                     }

                     const string deleteGroup = "DELETE FROM FormGroup WHERE FormId = @id";
                     using (var command = new MySqlCommand(deleteGroup, connection))
                     {
                         command.Parameters.Add("@id", DbType.Guid).Value = formId;
                         command.Transaction = trans;
                         await command.ExecuteNonQueryAsync();
                     }

                     const string deleteSection = "DELETE FROM Section WHERE FormId = @id";
                     using (MySqlCommand command = new MySqlCommand(deleteSection, connection))
                     {
                         command.Parameters.Add("@id", DbType.Guid).Value = formId;
                         command.Transaction = trans;
                         await command.ExecuteNonQueryAsync();
                    }
                    
                    await trans.CommitAsync();
                }
                catch (Exception)
                {   
                    await trans.RollbackAsync();
                    succeeded = false; 
                } 
                return succeeded;
            }     
        }


        async Task<FormStructure> IFormStructureStore.Get(Guid formId)
        {

            var structure = new FormStructure();
            structure.FormId = formId;
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                const string getForm = "Select FacRank,FormYear From Form WHERE FormId = @id;";
                using (var command = new MySqlCommand(getForm, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int YEAR_INDEX = 1;
                        const int RANK_INDEX = 0;
                        await reader.ReadAsync();
                        var rankAsString = reader.GetString(RANK_INDEX);
                        structure.Rank = FacultyRankMethods.FromString(rankAsString);
                        structure.FormYear = reader.GetString(YEAR_INDEX);
                        await reader.CloseAsync();
                    }
                }
                
                const string getGroups = "Select * From FormGroup WHERE FormId = @id;";
                var groups = new List<Group>();
                using (var command = new MySqlCommand(getGroups, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int GROUP_ID_INDEX = 1;
                        const int TITLE_INDEX = 2;
                        const int DESCRIPTION_INDEX = 3;
                        const int ORDERINDEX = 4;
                        while(await reader.ReadAsync())
                        {
                            var group = new Group();
                            group.GroupId = reader.GetGuid(GROUP_ID_INDEX);
                            group.Title = reader.GetString(TITLE_INDEX);
                            group.Description = reader.GetString(DESCRIPTION_INDEX);
                            group.OrderIndex = reader.GetInt32(ORDERINDEX);
                            groups.Add(group);
                        }
                    }
                }
                structure.Groups = groups;
                foreach(var group in structure.Groups)
                {
                    const string getSections = "Select * From Section WHERE FormId = @id AND GroupId = @groupId";
                    var sections = new List<Section>();
                    using (var command = new MySqlCommand(getSections, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formId;
                        command.Parameters.Add("@groupId", DbType.Guid).Value = group.GroupId;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int SECTION_ID_INDEX = 1;
                            const int SECTION_TYPE_INDEX = 2;
                            const int SECTION_TITLE_INDEX = 3;
                            const int SECTION_DESCRIPTION_INDEX = 4;
                            const int SECTION_OPTIONS_INDEX = 6;
                            const int ORDERINDEX = 7;
                            while(await reader.ReadAsync())
                            {
                                string sectionTypeName;
                                using (MySqlConnection connection2 =
                                    new MySqlConnection(_options.CurrentValue.ConnectionString))
                                {
                                    await connection2.OpenAsync();
                                    var getSectionTypename =
                                        "Select TypeName From SectionTypeTable WHERE id = @id;";
                                    
                                    using (var sectionCommand = new MySqlCommand(getSectionTypename, connection2))
                                    {
                                        sectionCommand.Parameters.Add("@id", DbType.Guid).Value = reader.GetInt32(SECTION_TYPE_INDEX);
                                        using (var sectionReader = await sectionCommand.ExecuteReaderAsync())
                                        {
                                            await sectionReader.ReadAsync();
                                            sectionTypeName = sectionReader.GetString(0);
                                            await sectionReader.CloseAsync();
                                        }
                                    }
                                }
                                var section = new Section();
                                section.GroupId = group.GroupId;
                                section.SectionDescription = reader.GetString(SECTION_DESCRIPTION_INDEX);
                                section.SectionId = reader.GetGuid(SECTION_ID_INDEX);
                                section.SectionTitle = reader.GetString(SECTION_TITLE_INDEX);
                                section.SectionType = SectionTypeMethods.FromString(sectionTypeName);
                                section.Options = SqlUtils.StringToOptionsArray(reader.GetString(SECTION_OPTIONS_INDEX));
                                section.OrderIndex = reader.GetInt32(ORDERINDEX);
                                sections.Add(section);
                            }
                        }
                        group.Sections = sections;
                    }
                }
            }
            return structure;
        }
        
        async Task<FormStructure> IFormStructureStore.Get(string formYear, FacultyRank rank)
        {
            Guid formId;
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                const string getForm = "Select FormId From Form WHERE FacRank = @facRank AND FormYear= @year;";
                using (var command = new MySqlCommand(getForm, connection))
                {
                    command.Parameters.Add("@facRank", DbType.String).Value = Enum.GetName(typeof(FacultyRank), rank);
                    command.Parameters.Add("@year", DbType.String).Value = formYear;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        formId = reader.GetGuid(0);
                        await reader.CloseAsync();
                    }
                }
            }
            return await ((IFormStructureStore)this).Get(formId);
        }

        async Task<FormContent> IFormContentStore.Get(Guid facultyId, Guid formId)
        {
            var formContent = new FormContent();
            formContent.FormId = formId;
            formContent.FacultyId = facultyId;
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                const string getformContent = "SELECT TopLevelReview, FormState, Modified FROM FormReviewContent WHERE FormId = @id AND UserId = @userId";
                using (var command = new MySqlCommand(getformContent, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int FORM_LEVEL_COMMENT_INDEX = 0;
                        const int STATE_INDEX = 1;
                        const int MODIFIED_INDEX = 2;
                        await reader.ReadAsync();
                        formContent.FormLevelComment = reader.GetString(FORM_LEVEL_COMMENT_INDEX);
                        
                        var stateAsString = reader.GetString(STATE_INDEX);
                        var success = Enum.TryParse<FormStatus>(stateAsString, out var result);
                        if(!success) throw new FormStoreInternalException($"Failed to decode FormState {stateAsString}");
                        formContent.State = result;
                        formContent.Modified = DateTimeOffset.Parse(reader.GetString(MODIFIED_INDEX));
                    }
                }
                const string getSectionContent = "SELECT SectionId, Content, Modified FROM SectionContent WHERE FormId= @id AND UserId= @userId";
                var sections = new List<SectionContent>();
                using (var command = new MySqlCommand(getSectionContent, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int SECTION_ID_INDEX = 0;
                        const int CONTENT_INDEX = 1;
                        const int MODIFIED_INDEX = 2;
                        
                        while(await reader.ReadAsync())
                        {
                            var section = new SectionContent();
                            section.Id = reader.GetGuid(SECTION_ID_INDEX);
                            section.Content = reader.GetString(CONTENT_INDEX);
                            section.Updated = DateTimeOffset.Parse(reader.GetString(MODIFIED_INDEX));
                            sections.Add(section);   
                        }
                    }
                }
                formContent.FacultyContent = sections;
                const string getReviewContent = "SELECT GroupId, Review, Modified FROM GroupReviewContent WHERE FormId= @id AND UserId= @userId";
                var reviews = new List<FacultyComment>();
                using (var command = new MySqlCommand(getReviewContent, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int GROUP_ID_INDEX = 0;
                        const int REVIEW_INDEX = 1;
                        const int MODIFIED_INDEX = 2;
                        
                        while(await reader.ReadAsync())
                        {
                            var review = new FacultyComment();
                            review.GroupId = reader.GetGuid(GROUP_ID_INDEX);
                            review.Comments = reader.GetString(REVIEW_INDEX);
                            review.Updated = DateTimeOffset.Parse(reader.GetString(MODIFIED_INDEX));
                            reviews.Add(review);   
                        }
                    }
                }
                formContent.ReviewContent = reviews;
                var spotScoreSection = new SpotScoreSection();
                const string getSpotScoreContent = "SELECT Question, Course, PercentResponded, Mean FROM SpotScoreContent WHERE FormId= @id AND UserId= @userId";
                var scores = new List<SpotScore>();
                using (var command = new MySqlCommand(getSpotScoreContent, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int QUESTION_INDEX = 0;
                        const int COURSE_INDEX = 1;
                        const int PERCENT_RESPONDED_INDEX = 2;
                        const int MEAN_INDEX = 3;
                        
                        while(await reader.ReadAsync())
                        {
                            var score = new SpotScore();
                            score.Question = reader.GetString(QUESTION_INDEX);
                            score.Course = reader.GetString(COURSE_INDEX);
                            score.PercentRespondents = reader.GetDouble(PERCENT_RESPONDED_INDEX);
                            score.MeanValue = reader.GetDouble(MEAN_INDEX);
                            scores.Add(score);  
                        }
                    }
                }
                spotScoreSection.Scores = scores;
                
                const string getSpotScoreMetadata = "SELECT FacultyComment, Review FROM SpotScoreSectionMetadata WHERE FormId= @id AND UserId= @userId";
                using (var command = new MySqlCommand(getSpotScoreMetadata, connection))
                {
                    command.Parameters.Add("@id", DbType.Guid).Value = formId;
                    command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int FACULTY_COMMENT_INDEX = 0;
                        const int REVIEW_INDEX = 1;
                        
                        await reader.ReadAsync();
                        spotScoreSection.FacultyComment = reader.GetString(FACULTY_COMMENT_INDEX);
                        spotScoreSection.Review = reader.GetString(REVIEW_INDEX); 
                    }
                }
                formContent.Scores = spotScoreSection;

            }
            return formContent;
        }
        

        async Task<FormContent> IFormContentStore.Create(Guid facultyId, FormContent formContentPayload)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                var modified = DateTimeOffset.UtcNow;
                try
                {
                    formContentPayload.Modified = modified;
                    const string createFormRegister = "INSERT INTO FormRegister(FacultyId,FormId,ReviewerId) " 
                        + "VALUES(@facultyId, @formId, @reviewerId)";
                    using (var command = new MySqlCommand(createFormRegister, connection))
                    {
                        command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = formContentPayload.FacultyId;
                        command.Parameters.Add("@reviewerId", DbType.Guid).Value = formContentPayload.ReviewerId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    const string createFormContent = "INSERT INTO FormReviewContent(FormId,UserId, TopLevelReview, Modified, FormState) "
                        + "VALUES(@formId, @facultyId, @topLevelReview, @modified, @state)";
                    using (var command = new MySqlCommand(createFormContent, connection))
                    {
                        command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                        command.Parameters.Add("@topLevelReview", DbType.String).Value = formContentPayload.FormLevelComment;
                        command.Parameters.Add("@modified", DbType.String).Value = modified;
                        command.Parameters.Add("@state", DbType.String).Value = Enum.GetName(typeof(FormStatus), formContentPayload.State);
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    const string createSectionContent = "INSERT INTO SectionContent(FormId, SectionId, UserId, Content, Modified)"
                            + " VALUES (@formId, @sectionId, @facultyId, @content, @modified)";
                    foreach (var section in formContentPayload.FacultyContent)
                    {
                        section.Updated = modified;
                        using (var command = new MySqlCommand(createSectionContent, connection))
                        {
                            command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            command.Parameters.Add("@sectionId", DbType.Guid).Value = section.Id;
                            command.Parameters.Add("@modified", DbType.String).Value = modified;
                            command.Parameters.Add("@content", DbType.String).Value = section.Content;
                            command.Transaction = trans;
                            await command.ExecuteNonQueryAsync(); 
                        }
                    }
                    
                    var createReviewContent = "INSERT INTO GroupReviewContent(FormId, GroupId, UserId, Review, Modified) "
                            + "VALUES (@formId, @groupId, @facultyId, @comments, @modified)";
                    foreach (var review in formContentPayload.ReviewContent)
                    {
                        review.Updated = modified;
                        using (var command = new MySqlCommand(createReviewContent, connection))
                        {
                            command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            command.Parameters.Add("@groupId", DbType.Guid).Value = review.GroupId;
                            command.Parameters.Add("@modified", DbType.String).Value = modified;
                            command.Parameters.Add("@comments", DbType.String).Value = review.Comments;
                            command.Transaction = trans;
                            await command.ExecuteNonQueryAsync(); 
                        }
                    }
                    var scoreSection = formContentPayload.Scores;
                    
                    const string insertSpotScoreMetadata = "INSERT INTO SpotScoreSectionMetadata(FormId, UserId, FacultyComment, Review)"
                        + "VALUES (@formId, @facultyId, @facultyComment, @review)";
                    using (var command = new MySqlCommand(insertSpotScoreMetadata, connection))
                    {
                        command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                        command.Parameters.Add("@facultyComment", DbType.String).Value = scoreSection.FacultyComment;
                        command.Parameters.Add("@review", DbType.String).Value = scoreSection.Review;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    } 
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    // Logging?
                    throw new FormStoreValidationException(e.Message);
                }
                return formContentPayload;
            }
            
        }

        async Task<FormContent> IFormContentStore.Update(Guid facultyId, Guid formId, FormContent formContentPayload)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                var modified = DateTimeOffset.UtcNow;
                try
                {
                    formContentPayload.Modified = modified;
                    const string updateFormContent = "UPDATE FormReviewContent SET TopLevelReview = @topLevelReview, Modified = @modified, FormState = @state WHERE UserId = @facultyId And FormID = @formId";
                    using (var command = new MySqlCommand(updateFormContent, connection))
                    {
                        // First you write the query skeleton, and then you replace all the spots with real variables below
                        command.Parameters.Add("@topLevelReview", DbType.String).Value = formContentPayload.FormLevelComment;
                        command.Parameters.Add("@modified", DbType.String).Value = modified;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                        command.Parameters.Add("@formId", DbType.Guid).Value = formId;
                        command.Parameters.Add("@state", DbType.String).Value = Enum.GetName(typeof(FormStatus), formContentPayload.State);
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync();
                    }
                    const string updateSectionContent = "UPDATE SectionContent SET Content = @content, Modified = @modified WHERE FormId = @formId AND SectionId = @sectionId AND UserId = @userId";
                    foreach (var section in formContentPayload.FacultyContent)
                    {
                        section.Updated = modified;
                        using (var command = new MySqlCommand(updateSectionContent, connection))
                        {
                            command.Parameters.Add("@content", DbType.String).Value = section.Content;
                            command.Parameters.Add("@modified", DbType.String).Value = modified;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            command.Parameters.Add("@formId", DbType.Guid).Value = formId;
                            command.Parameters.Add("@sectionId", DbType.Guid).Value = section.Id;
                            command.Parameters.Add("@userId", DbType.Guid).Value = formContentPayload.FacultyId;
                            command.Transaction = trans;
                            await command.ExecuteNonQueryAsync(); 
                        }
                    }
                    const string updateReviewContent = "UPDATE GroupReviewContent SET Review = @review, Modified = @modified WHERE FormId = @formId AND GroupID = @groupId AND UserId = @facultyId";
                    foreach (var review in formContentPayload.ReviewContent)
                    {
                        review.Updated = modified;
                        using (var command = new MySqlCommand(updateReviewContent, connection))
                        {
                            command.Parameters.Add("@review", DbType.String).Value = review.Comments;
                            command.Parameters.Add("@modified", DbType.String).Value = modified;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            command.Parameters.Add("@formId", DbType.Guid).Value = formId;
                            command.Parameters.Add("@groupId", DbType.Guid).Value = review.GroupId;
                            command.Transaction = trans;
                            await command.ExecuteNonQueryAsync(); 
                        }
                    }
                    var scoreSection = formContentPayload.Scores;
                    
                    const string deleteSpotScoreMetadata = "DELETE FROM SpotScoreSectionMetadata WHERE FormId = @formId";
                    using (var command = new MySqlCommand(deleteSpotScoreMetadata, connection))
                    {
                        command.Parameters.Add("@formId", DbType.Guid).Value = formId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    
                    const string insertSpotScoreMetadata = "INSERT INTO SpotScoreSectionMetadata(FormId, UserId, FacultyComment, Review)"
                        + "VALUES (@formId, @facultyId, @facultyComment, @review)";
                    using (var command = new MySqlCommand(insertSpotScoreMetadata, connection))
                    {
                        command.Parameters.Add("@formId", DbType.Guid).Value = formContentPayload.FormId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                        command.Parameters.Add("@facultyComment", DbType.String).Value = scoreSection.FacultyComment;
                        command.Parameters.Add("@review", DbType.String).Value = scoreSection.Review;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    await trans.CommitAsync();
                }
                catch (Exception e)
                {
                    await trans.RollbackAsync();
                    // Logging?
                    throw;

                }
                return formContentPayload;
            }
        }

        async Task<bool> IFormContentStore.Delete(Guid facultyId, Guid formId)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();

                try
                {
                    const string deleteFormContent = "DELETE FROM FormReviewContent WHERE UserId = @facultyId, FormId = @id";
                    using (var command = new MySqlCommand(deleteFormContent, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = formId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }

                    const string deleteReviewContent = "DELETE FROM GroupReviewContent WHERE UserId = @facultyId, FormId = @id";
                    using (var command = new MySqlCommand(deleteReviewContent, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formId;
                        command.Parameters.Add("@facultyId", DbType.Guid).Value = formId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    
                    const string deleteSpotScoreContent = "DELETE FROM SpotScoreContent WHERE FormId = @id";
                    using (var command = new MySqlCommand(deleteSpotScoreContent, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }

                    const string deleteSpotScoreMetadata = "DELETE FROM SpotScoreSectionMetadata WHERE FormId = @id";
                    using (var command = new MySqlCommand(deleteSpotScoreMetadata, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = formId;
                        command.Transaction = trans;
                        await command.ExecuteNonQueryAsync(); 
                    }
                    await trans.CommitAsync();
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    // Logging?
                    throw;
                }
                return true;
            }
            
        }
        
        async Task<IEnumerable<FormStub>> IFormContentStore.GetAll(Guid facultyId)
        {
            var stubs = new List<FormStub>();
            var ids = new List<Guid>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    const string getformContent = "SELECT FormId FROM FormRegister WHERE FacultyId = @id;";
                    using (var command = new MySqlCommand(getformContent, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = facultyId;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int FORM_ID_INDEX = 0;
                            while(await reader.ReadAsync())
                            {
                                ids.Add(reader.GetGuid(FORM_ID_INDEX));
                            }
                        }
                    }
                }
                foreach(var id in ids)
                {
                    using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                    {
                        var stub = new FormStub();
                        stub.FacultyId = facultyId;
                        stub.FormId = id;
                        await connection.OpenAsync();
                        const string getFormInfo = "SELECT FacRank, FormYear FROM Form WHERE FormId = @id;";
                        using (var command = new MySqlCommand(getFormInfo, connection))
                        {
                            command.Parameters.Add("@id", DbType.Guid).Value = id;
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                const int RANK_INDEX = 0;
                                const int YEAR_INDEX = 1;
                                await reader.ReadAsync();
                                if(Enum.TryParse<FacultyRank>(reader.GetString(RANK_INDEX), true, out var result))
                                {
                                    stub.Rank = result;
                                }
                                else
                                {
                                    throw new FormStoreInternalException($"No suitable match for rank {reader.GetString(RANK_INDEX)}"); 
                                }
                                stub.FormYear = reader.GetString(YEAR_INDEX);
                            }
                        }
                        const string getState = "SELECT FormState FROM FormReviewContent WHERE FormId = @id AND UserId = @facultyId;";
                        using (var command = new MySqlCommand(getState, connection))
                        {
                            command.Parameters.Add("@id", DbType.Guid).Value = id;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                const int STATE_INDEX = 0;
                                
                                await reader.ReadAsync();
                                if(Enum.TryParse<FormStatus>(reader.GetString(STATE_INDEX), true, out var result))
                                {
                                    stub.State = result;
                                }
                                else
                                {
                                    throw new FormStoreInternalException(
                                        $"No suitable match for state {reader.GetString(STATE_INDEX)}");
                                }
                            }
                        }
                        stubs.Add(stub);
                    }    
                }
            }
            catch(Exception e)
            {
                throw new FormStoreValidationException(e.Message);
            }
            return stubs;
        }

        public async Task Create(Guid facultyId, Guid formId, IEnumerable<SpotScore> spotScores)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var trans = await connection.BeginTransactionAsync();
                var modified = DateTimeOffset.UtcNow;
                try
                {
                    var insertSpotScore = "INSERT INTO SpotScoreContent(FormId, UserId, Question, Course, PercentResponded, Mean)"
                            + "VALUES (@formId, @facultyId, @question, @course, @percentResponded, @mean)";
                    foreach(var score in spotScores)
                    {
                        using (var command = new MySqlCommand(insertSpotScore, connection))
                        {
                            command.Parameters.Add("@formId", DbType.Guid).Value = formId;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = facultyId;
                            command.Parameters.Add("@question", DbType.String).Value = score.Question;
                            command.Parameters.Add("@course", DbType.String).Value = score.Course;
                            command.Parameters.Add("@percentResponded", DbType.String).Value = score.PercentRespondents;
                            command.Parameters.Add("@mean", DbType.Decimal).Value = score.MeanValue;
                            command.Transaction = trans;
                            await command.ExecuteNonQueryAsync(); 
                        } 
                    }
                    await trans.CommitAsync();
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    // Logging?
                    throw;
                }
            
            }
        }

        public SqlFormStore(IOptionsMonitor<SqlFormStoreOptions> options)
        {
            this._options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private IOptionsMonitor<SqlFormStoreOptions> _options;
    }
}
