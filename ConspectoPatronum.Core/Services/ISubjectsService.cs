
using ConspectoPatronum.Domain;
using System.Collections.Generic;

namespace ConspectoPatronum.Core.Services
{
    public interface ISubjectsService : IService<Subject>
    {
        IList<Subject> GetByTeacher(Teacher teacher);

        Subject GetByTitle(string title);

        IList<Subject> GetBySemester(Semester semester);
    }
}
