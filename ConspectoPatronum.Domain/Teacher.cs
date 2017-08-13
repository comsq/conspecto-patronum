using System.ComponentModel.DataAnnotations;

namespace ConspectoPatronum.Domain
{
    public class Teacher : Entity
    {
        [Display(Name = "ФИО")]
        public virtual string Name { get; set; }

        [Display(Name = "Категория")]
        public virtual TeacherCategory Category { get; set; }

        [Display(Name = "Фото")]
        public virtual Image Photo { get; set; }
    }
}
