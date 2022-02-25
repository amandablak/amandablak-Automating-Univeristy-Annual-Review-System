using System;
using System.Threading.Tasks;
using FacultyAPR.Models.Form;
using FacultyAPR.Storage;
using System.Data.SqlClient;

namespace FacultyAPR.Storage.Sql
{
    public sealed class SqlUserStoreOptions
    {
        public static string SQLUserOptions{ get; } = "SQLUser";
        public string ConnectionString { get; set; }
    }
}