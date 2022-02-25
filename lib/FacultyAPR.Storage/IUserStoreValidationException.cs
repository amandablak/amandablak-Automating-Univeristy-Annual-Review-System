using System;

namespace FacultyAPR.Storage.Sql
{
    public sealed class UserStoreValidationException : Exception
    {
        public UserStoreValidationException(string message): base(message) {}
    }
}
