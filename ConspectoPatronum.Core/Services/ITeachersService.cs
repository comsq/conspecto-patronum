using ConspectoPatronum.Domain;
using System.Collections.Generic;

namespace ConspectoPatronum.Core.Services
{
    public interface ITeachersService : IService<Teacher>
    {
        Teacher GetByName(string teacherName);
    }
}
