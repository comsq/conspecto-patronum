using System.ComponentModel.DataAnnotations;

namespace ConspectoPatronum.Domain
{
    public class Image : Entity
    {
        [Display(Name = "Курс")]
        public virtual Subject Subject { get; set; }

        [Display(Name = "Номер фото в конспекте")]
        public virtual int Number { get; set; }

        public virtual string FileName { get; set; }
    }
}
