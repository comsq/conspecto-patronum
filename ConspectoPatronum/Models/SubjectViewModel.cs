using ConspectoPatronum.Domain;
using System;
using System.Collections.Generic;

namespace ConspectoPatronum.Models
{
    public class SubjectViewModel
    {
        public Subject Subject { get; set; }
        public IList<Image> Images { get; set; }
    }
}