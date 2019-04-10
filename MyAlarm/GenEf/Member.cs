using System;
using System.Collections.Generic;

namespace GenEf
{
    public partial class Member
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FkRole { get; set; }
        public string NumPhone { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
    }
}
