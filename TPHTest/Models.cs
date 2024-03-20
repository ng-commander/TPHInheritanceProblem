using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPHTest
{
    public class Parent
    {
        public Guid Id { get; set; }
        public IReadOnlyList<Child> Childs { get; set; } = new List<Child>();
    }

    public class Child
    {
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
    }

    public class ChildDerived : Child
    {
        public Misc Misc { get; set; }
    }

    public class Message
    {
        public Guid Id { get; set; }
    }

    public class Misc
    {
        public Message Message { get; set; }
    }
}