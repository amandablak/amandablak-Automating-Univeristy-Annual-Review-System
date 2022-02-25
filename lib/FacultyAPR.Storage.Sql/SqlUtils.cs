using System.Collections.Generic;

namespace FacultyAPR.Storage.Sql
{
    public static class SqlUtils
    {
        private static readonly string optionsArrayDelimiter = @"~\|/";

        public static string OptionsArrayToString(IEnumerable<string> options)
        {
            if (options == default) throw new System.ArgumentNullException(nameof(options));
            return string.Join(optionsArrayDelimiter, options);
        }

        public static IEnumerable<string> StringToOptionsArray(string options)
        {
            if (options == default) throw new System.ArgumentNullException(nameof(options));
            return options.Split(optionsArrayDelimiter);
        }
    }
}