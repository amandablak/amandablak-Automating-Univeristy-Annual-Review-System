using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacultyAPR.Models.Form;

namespace FacultyAPR.Storage
{
    public interface IFormContentStore
    {
        Task<IEnumerable<FormStub>> GetAll(Guid facultyId);
        
        Task<FormContent> Get(Guid facultyId, Guid formId);

        Task<FormContent> Update(Guid facultyId, Guid formId, FormContent formContentPayload);

        Task<FormContent> Create(Guid facultyId, FormContent formContentPayload);

        Task Create (Guid facultyId, Guid formId, IEnumerable<SpotScore> spotScores);

        Task<bool> Delete(Guid facultyId, Guid formId);
    }
}
