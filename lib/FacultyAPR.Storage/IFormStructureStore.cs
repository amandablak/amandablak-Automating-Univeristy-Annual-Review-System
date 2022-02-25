using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacultyAPR.Models;
using FacultyAPR.Models.Form;

namespace FacultyAPR.Storage
{
    public interface IFormStructureStore
    {
        Task<FormStructure> Get(Guid formId);

        Task<FormStructure> Get(string formYear, FacultyRank rank);

        Task<FormStructure> Update(FormStructure formPayload);

        Task<FormStructure> Create(FormStructure formPayload);

        Task<bool> Delete(Guid formId);
        
    }
}
