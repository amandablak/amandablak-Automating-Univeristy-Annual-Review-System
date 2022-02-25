using System;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using MySqlConnector;
using FacultyAPR.Models;
using System.IO;
namespace FacultyAPR.Storage.Sql
{
    public class SqlUserStore : IUserStore
    {

        public async Task<IEnumerable<IUser>> Get(string email)
        {
            if (string.IsNullOrEmpty(email)) { throw new ArgumentNullException(nameof(email)); }
            var users = new List<IUser>();
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                const string getUserByEmail = "SELECT UserId, FirstName, LastName, UserType FROM UserInfo WHERE EmailAddress = @email";
                using (var command = new MySqlCommand(getUserByEmail, connection))
                {
                    command.Parameters.Add("@email", DbType.String).Value = email;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        const int UserIdIndex = 0;
                        const int FirstNameIndex = 1;
                        const int LastNameIndex = 2;
                        const int UserTypeIndex = 3;
                        while (await reader.ReadAsync())
                        {
                            var user = new User
                            {
                                Id = reader.GetGuid(UserIdIndex),
                                FirstName = reader.GetString(FirstNameIndex),
                                LastName = reader.GetString(LastNameIndex),
                                EmailAddress = email,
                                UserType = (UserType)Enum.Parse(typeof(UserType), reader.GetString(UserTypeIndex))
                            };
                            users.Add(user);
                        }
                        await reader.CloseAsync();
                    }
                }
            }
            return users;
        }

        public async Task<IUser> Create(IUser userPayload)
        {

            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var transaction = await connection.BeginTransactionAsync();
                try
                {
                    var createUser = $"INSERT INTO UserInfo(UserId, FirstName, LastName, EmailAddress, UserType) VALUES (@id, @firstName, @lastName, @email, @userType)";
                    using (var command = new MySqlCommand(createUser, connection))
                    {
                        command.Transaction = transaction;
                        command.Parameters.Add("@id", DbType.Guid).Value = userPayload.Id;
                        command.Parameters.Add("@firstName", DbType.String).Value = userPayload.FirstName;
                        command.Parameters.Add("@lastName", DbType.String).Value = userPayload.LastName;
                        command.Parameters.Add("@email", DbType.String).Value = userPayload.EmailAddress;
                        command.Parameters.Add("@userType", DbType.String).Value = Enum.GetName(typeof(UserType), userPayload.UserType);
                        await command.ExecuteNonQueryAsync();
                    }
                    await transaction.CommitAsync();
                }

                catch (Exception error)
                {
                    await transaction.RollbackAsync();
                    throw new UserStoreValidationException(error.Message);
                }

            }
            return userPayload;

        }

        public async Task<bool> Remove(IUser userPayload)
        {

            var deleted = true;
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var transaction = connection.BeginTransaction();
                try
                {
                    const string deleteSqlStatement = "DELETE FROM UserInfo WHERE UserID = @userId";
                    using (var command = new MySqlCommand(deleteSqlStatement, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = userPayload.Id;
                        command.ExecuteNonQuery();
                        command.Transaction = transaction;
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception error)
                {
                    await transaction.RollbackAsync();
                    deleted = false;
                    throw error;

                }
            }
            return await Task.FromResult(deleted);
        }

        public async Task<IUser> Update(IUser userPayload)
        {
            using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
            {
                await connection.OpenAsync();
                var transaction = await connection.BeginTransactionAsync();
                try
                {
                    const string updateUser = "UPDATE UserInfo SET  FirstName = @firstName, LastName = @lastName, EmailAddress = @email, UserType = @userType WHERE UserId = @userId ";
                    using (var command = new MySqlCommand(updateUser, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = userPayload.Id;
                        command.Parameters.Add("@firstName", DbType.String).Value = userPayload.FirstName;
                        command.Parameters.Add("@lastName", DbType.String).Value = userPayload.LastName;
                        command.Parameters.Add("@email", DbType.String).Value = userPayload.EmailAddress;
                        command.Parameters.Add("@userType", DbType.String).Value = Enum.GetName(typeof(UserType), userPayload.UserType);
                        command.Transaction = transaction;
                        await command.ExecuteNonQueryAsync();
                    }
                    await transaction.CommitAsync();

                }
                catch (Exception error)
                {
                    await transaction.RollbackAsync();
                    throw error;
                }
            }
            return userPayload;
        }

        public async Task<IEnumerable<IUser>> GetAll(Guid facultyId)
        {
            var users = new List<IUser>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    var getUser = $"SELECT * FROM UserInfo WHERE Id = @userId";
                    using (var command = new MySqlCommand(getUser, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int ID_INDEX = 0;
                            const int FIRST_NAME_INDEX = 1;
                            const int LAST_NAME_INDEX = 2;
                            const int EMAIL_ADDRESS_INDEX = 3;
                            const int USER_TYPE_INDEX = 4;
                            while (await reader.ReadAsync())
                            {
                                var user = new User();
                                user.Id = reader.GetGuid(ID_INDEX);
                                user.FirstName = reader.GetString(FIRST_NAME_INDEX);
                                user.LastName = reader.GetString(LAST_NAME_INDEX);
                                user.EmailAddress = reader.GetString(EMAIL_ADDRESS_INDEX);
                                var userTypeAsString = reader.GetString(USER_TYPE_INDEX);
                                user.UserType = FacultyUserType.FromString(userTypeAsString);
                                users.Add(user);
                            }

                        }
                    }
                }
            }
            catch (Exception error)
            {
                throw error;
            }
            return users;
        }

        public async Task<IEnumerable<IUser>> GetAll()
        {
            var users = new List<IUser>();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    var getUsers = $"SELECT * FROM UserInfo;";
                    using (var command = new MySqlCommand(getUsers, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int ID_INDEX = 0;
                            const int FIRST_NAME_INDEX = 1;
                            const int LAST_NAME_INDEX = 2;
                            const int EMAIL_ADDRESS_INDEX = 3;
                            const int USER_TYPE_INDEX = 4;

                            while (await reader.ReadAsync())
                            {
                                var user = new User();
                                user.Id = reader.GetGuid(ID_INDEX);
                                user.FirstName = reader.GetString(FIRST_NAME_INDEX);
                                user.LastName = reader.GetString(LAST_NAME_INDEX);
                                user.EmailAddress = reader.GetString(EMAIL_ADDRESS_INDEX);
                                var userTypeAsString = reader.GetString(USER_TYPE_INDEX);
                                user.UserType = FacultyUserType.FromString(userTypeAsString);
                                users.Add(user);

                            }
                        }
                    }
                }
            }

            catch (Exception error)
            {
                throw new FormStoreValidationException(error.Message);
            }

            return users;
        }

        public async Task<FacultyUser> GetFaculty(Guid facultyId)
        {
            var faculty = new FacultyUser();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    var getFaculty = $"SELECT * FROM Faculty WHERE Id = @userId";
                    using (var command = new MySqlCommand(getFaculty, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int ID_INDEX = 0;
                            const int FIRST_NAME_INDEX = 1;
                            const int LAST_NAME_INDEX = 2;
                            const int EMAIL_ADDRESS_INDEX = 3;
                            const int RANK_INDEX = 4;
                            const int DEPARTMENT_INDEX = 5;
                            const int USER_TYPE_INDEX = 6;
                            while (await reader.ReadAsync())
                            {

                                faculty.Id = reader.GetGuid(ID_INDEX);
                                faculty.FirstName = reader.GetString(FIRST_NAME_INDEX);
                                faculty.LastName = reader.GetString(LAST_NAME_INDEX);
                                faculty.EmailAddress = reader.GetString(EMAIL_ADDRESS_INDEX);
                                var facultyRankAsString = reader.GetString(RANK_INDEX);
                                faculty.Rank = FacultyRankMethods.FromString(facultyRankAsString);
                                var facultyDepAsString = reader.GetString(DEPARTMENT_INDEX);
                                faculty.Department = CollegeDepartmentMethods.FromString(facultyDepAsString);
                                var userTypeAsString = reader.GetString(USER_TYPE_INDEX);
                                faculty.UserType = FacultyUserType.FromString(userTypeAsString);

                            }

                        }
                    }

                }


            }

            catch (Exception error)
            {
                throw error;
            }
            return faculty;
        }

        public async Task<APRReviewer> GetReviewer(Guid reviewerId)
        {

            var reviewer = new APRReviewer();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    var GetReviewer = $"SELECT * FROM Reviewer WHERE Id = @userId;";
                    using (var command = new MySqlCommand(GetReviewer, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = reviewerId;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int ID_INDEX = 0;
                            const int FIRST_NAME_INDEX = 1;
                            const int LAST_NAME_INDEX = 2;
                            const int EMAIL_ADDRESS_INDEX = 3;
                            const int DEPARTMENT_INDEX = 4;
                            const int USER_TYPE_INDEX = 5;
                            while (await reader.ReadAsync())
                            {

                                reviewer.Id = reader.GetGuid(ID_INDEX);
                                reviewer.FirstName = reader.GetString(FIRST_NAME_INDEX);
                                reviewer.LastName = reader.GetString(LAST_NAME_INDEX);
                                reviewer.EmailAddress = reader.GetString(EMAIL_ADDRESS_INDEX);
                                var reviewerDepAsString = reader.GetString(DEPARTMENT_INDEX);
                                reviewer.Department = CollegeDepartmentMethods.FromString(reviewerDepAsString);
                                var userTypeAsString = reader.GetString(USER_TYPE_INDEX);
                                reviewer.UserType = FacultyUserType.FromString(userTypeAsString);

                            }

                        }
                    }

                }

            }

            catch (Exception error)
            {
                throw error;
            }
            return reviewer;
        }

        public async Task<Guid> GetReviewerByFaculty(Guid facultyId, UserType rank)
        {
            var reviewer = new Guid();
            try
            {
                var reviewerDepAsString = "";
                using (MySqlConnection connection = new MySqlConnection(_options.CurrentValue.ConnectionString))
                {
                    await connection.OpenAsync();
                    if (rank == UserType.FacultyChair) {
                        var GetDeanOfCollege = $"SELECT UserId From UserInfo WHERE UserType = \"Dean\";";
                        using (var command = new MySqlCommand(GetDeanOfCollege, connection))
                        {
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                const int ID_INDEX = 0;
                                while (await reader.ReadAsync())
                                {
                                    return reader.GetGuid(ID_INDEX);
                                }
                            }
                        }
                    }

                    var GetDepartmentOfFaculty = $"SELECT DepartmentName FROM Faculty LEFT JOIN Department ON Faculty.DepartmentId = Department.DepartmentId WHERE UserId = @userId;";
                    using (var command = new MySqlCommand(GetDepartmentOfFaculty, connection))
                    {
                        command.Parameters.Add("@userId", DbType.Guid).Value = facultyId;
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            const int DEPARTMENT_NAME_INDEX = 0;
                            while (await reader.ReadAsync())
                            {
                                reviewerDepAsString = reader.GetString(DEPARTMENT_NAME_INDEX);
                                
                            }

                        }
                    }

                    var GetReviewer = $"SELECT Faculty.UserId FROM Faculty JOIN Department ON Faculty.DepartmentId = Department.DepartmentId JOIN UserInfo ON UserInfo.UserId = Faculty.UserId WHERE DepartmentName = @departmentName AND UserInfo.UserType = \"FacultyChair\";";
                    using (var command2 = new MySqlCommand(GetReviewer, connection))
                    {
                        command2.Parameters.Add("@departmentName", DbType.String).Value = reviewerDepAsString;
                        using (var reader = await command2.ExecuteReaderAsync())
                        {
                            const int ID_INDEX = 0;
                            while (await reader.ReadAsync())
                            {
                                
                                reviewer = reader.GetGuid(ID_INDEX);
                                
                            }

                        }
                    }

                }

            }
            catch (Exception error)
            {
                throw error;
            }
            return reviewer;
        }

    public SqlUserStore(IOptionsMonitor<SqlUserStoreOptions> options)
        {
            this._options = options ?? throw new ArgumentNullException(nameof(options));
        }
        private IOptionsMonitor<SqlUserStoreOptions> _options;
    }       
    
}


