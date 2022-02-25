using System;

namespace FacultyAPR.Storage.Sql
{
    public sealed class FormStoreInternalException : Exception
    {
        public FormStoreInternalException(string message): base(message) {}
    }
}