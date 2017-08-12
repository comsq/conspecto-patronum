using System.ComponentModel.DataAnnotations;

namespace ConspectoPatronum.Domain
{
    public enum TeacherCategory
    {
        [Display(Name = "Лояльный")]
        Loyal,

        [Display(Name = "Нормальный")]
        Normal,

        [Display(Name = "Маниакально-агрессивный")]
        Maniac
    }
}
