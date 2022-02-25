using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FacultyAPR.Models;
using FacultyAPR.Models.Form;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace FacultyAPR.Storage.Sql
{
    public sealed class SqlReviewerStore : IFormReviewerStore
    {
        public async Task<IEnumerable<FormStub>> GetAll(Guid reviewerId)
        {
            var stubs = new List<FormStub>();
            var formIdFacultyIdPair = new List<(Guid formId, Guid facultyId)>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    const string getformContent = "SELECT FormId, FacultyId FROM FormRegister WHERE ReviewerId = @id;";
                    using (var command = new MySqlCommand(getformContent, connection))
                    {
                        command.Parameters.Add("@id", DbType.Guid).Value = reviewerId;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int FORM_ID_INDEX = 0;
                            const int FACULTY_ID_INDEX = 1;
                            while(await reader.ReadAsync())
                            {
                                formIdFacultyIdPair.Add((reader.GetGuid(FORM_ID_INDEX), reader.GetGuid(FACULTY_ID_INDEX)));
                            }
                        }
                    }
                }
                foreach(var id in formIdFacultyIdPair)
                {
                    using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                    {
                        var stub = new FormStub();
                        stub.FormId = id.formId;
                        stub.FacultyId = id.facultyId;
                        await connection.OpenAsync();
                        const string getFormInfo = "SELECT FacRank, FormYear FROM Form WHERE FormId = @id;";
                        using (var command = new MySqlCommand(getFormInfo, connection))
                        {
                            command.Parameters.Add("@id", DbType.Guid).Value = id.formId;
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
                            command.Parameters.Add("@id", DbType.Guid).Value = id.formId;
                            command.Parameters.Add("@facultyId", DbType.Guid).Value = id.facultyId;
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

        public SqlReviewerStore(IOptionsMonitor<SqlFormStoreOptions> options)
        {
            this._options = options ?? throw new ArgumentNullException(nameof(options));
        }

        private IOptionsMonitor<SqlFormStoreOptions> _options;

    }
}