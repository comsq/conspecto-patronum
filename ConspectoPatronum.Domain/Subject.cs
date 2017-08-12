using System.ComponentModel.DataAnnotations;

namespace ConspectoPatronum.Domain
{
    public class Subject : Entity
    {
        [Display(Name = "Наименование курса")]
        public virtual string Title { get; set; }

        [Display(Name = "Преподаватель")]
        public virtual Teacher Teacher { get; set; }

        [Display(Name = "Описание курса")]
        public virtual string Description { get; set; }

        [Display(Name = "Сложность")]
        public virtual Difficulty Difficulty { get; set; }
        
        [Display(Name = "Семестр начала")]
        public virtual Semester FromSemester { get; set; }

        [Display(Name = "Семестр конца")]
        public virtual Semester ToSemester { get; set; }
    }
}
