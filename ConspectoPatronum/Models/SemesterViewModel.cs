using ConspectoPatronum.Domain;
using System.Collections.Generic;

namespace ConspectoPatronum.Models
{
    public class SemesterViewModel
    {
        public Semester Semester { get; set; }

        public IList<Subject> Subjects { get; set; }
    }
}