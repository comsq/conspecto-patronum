using System;

namespace ConspectoPatronum.Domain
{
    public class Review : Entity
    {
        public virtual DateTime DateTime { get; set; }

        public virtual string IpAddress { get; set; }

        public virtual string UserAgent { get; set; }

        public virtual string UserName { get; set; }

        /**
         * Users review is a visit if the last visit of this
         * user was not earlier than 30 minutes ago.
         * */
        public virtual bool IsVisit { get; set; }
    }
}
