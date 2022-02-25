using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacultyAPR.Models.Form;

namespace FacultyAPR.Storage
{
    public interface IFormReviewerStore
    {
        Task<IEnumerable<FormStub>> GetAll(Guid reviewerId);
    }
}
