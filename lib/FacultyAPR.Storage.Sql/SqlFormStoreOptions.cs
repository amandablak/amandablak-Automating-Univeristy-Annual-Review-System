using System;
using System.Threading.Tasks;
using FacultyAPR.Models.Form;
using FacultyAPR.Storage;
using System.Data.SqlClient;

namespace FacultyAPR.Storage.Sql
{
    public sealed class SqlFormStoreOptions
    {
        public static string SQLFormOptions{ get; } = "SQLForm";
        public string ConnectionString { get; set; }
    }
}
