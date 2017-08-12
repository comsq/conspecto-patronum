using System;
using System.ComponentModel.DataAnnotations;

namespace ConspectoPatronum.Domain
{
    public class Comment : Entity
    {        
        public virtual string Author { get; set; }

        [Display(Name = "Заголоавок")]
        public virtual string Title { get; set; }

        [Display(Name = "Текст")]
        public virtual string Text { get; set; }

        public virtual DateTime PostedOn { get; set; }

        public virtual DateTime? Modified { get; set; }
    }
}
