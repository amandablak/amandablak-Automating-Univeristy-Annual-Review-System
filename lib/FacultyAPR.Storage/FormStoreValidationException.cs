using System;

namespace FacultyAPR.Storage.Sql
{
    public sealed class FormStoreValidationException : Exception
    {
        public FormStoreValidationException(string message): base(message) {}
    }
}