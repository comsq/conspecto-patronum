using ConspectoPatronum.Domain;
using System.Collections.Generic;

namespace ConspectoPatronum.Models
{
    public class TeachersSubjectsViewModel
    {
        public Teacher Teacher { get; set; }
        public IList<Subject> Subjects { get; set; }
    }
}