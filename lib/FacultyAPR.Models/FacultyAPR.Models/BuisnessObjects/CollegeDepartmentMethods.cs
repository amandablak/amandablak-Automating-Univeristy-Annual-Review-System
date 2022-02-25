using System;
using System.Text.Json.Serialization;

namespace FacultyAPR.Models
{
    
    public static class CollegeDepartmentMethods
    {
        public static CollegeDepartment FromString(string facultyDepartment)
        {
            if (facultyDepartment.Equals("Biology", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.Biology;
            }
            else if (facultyDepartment.Equals("Chemistry", StringComparison.InvariantCultureIgnoreCase))    
            {
                return CollegeDepartment.Chemistry;
            }
            else if (facultyDepartment.Equals("AssistantProfessor", StringComparison.InvariantCultureIgnoreCase) 
                || facultyDepartment.Equals("Civil and Environmental Engineering", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.CivilAndEnvironmentalEngineering;
            }
            else if (facultyDepartment.Equals("ComputerScience", StringComparison.InvariantCultureIgnoreCase) 
                || facultyDepartment.Equals("Computer Science", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.ComputerScience;
            }
            else if (facultyDepartment.Equals("ElectricalAndComputerEngineering", StringComparison.InvariantCultureIgnoreCase) 
                || facultyDepartment.Equals("Electrical and Computer Engineering", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.ElectricalAndComputerEngineering;
            }
            else if (facultyDepartment.Equals("mechanicalEngineering", StringComparison.InvariantCultureIgnoreCase) 
                || facultyDepartment.Equals("Mechanical Engineering", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.MechanicalEngineering;
            }
            else if (facultyDepartment.Equals("Mathematics", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.Mathematics;
            }
            else if (facultyDepartment.Equals("Physics", StringComparison.InvariantCultureIgnoreCase))
            {
                return CollegeDepartment.Physics;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Faculty Department {facultyDepartment} not currently supported");
            }
        }
    }
}