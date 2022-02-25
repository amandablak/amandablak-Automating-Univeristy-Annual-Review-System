using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FacultyAPR.Models;

namespace FacultyAPR.Storage
{
    public interface IUserStore
    {
        Task<IUser> Create(IUser userPayload);

        Task<bool> Remove(IUser userPayload);

        Task<IUser> Update(IUser userPayload);

        Task<IEnumerable<IUser>> GetAll(Guid facultyId);

        Task<IEnumerable<IUser>> GetAll();

        Task<IEnumerable<IUser>> Get(string email);

        Task<FacultyUser> GetFaculty(Guid facultyId);
        Task<APRReviewer> GetReviewer(Guid reviewerId);

        Task<Guid> GetReviewerByFaculty(Guid facultyId, UserType rank);


    }
}
