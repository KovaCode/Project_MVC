using Model.Common;
using System;

namespace Model
{
    public class Make : IMake
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }

}
