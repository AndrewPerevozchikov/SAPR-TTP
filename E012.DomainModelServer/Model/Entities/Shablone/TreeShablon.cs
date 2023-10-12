using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class TreeShablon
    {
        public Int16 Level { get; set; }
        public Int16? Tip { get; set; }
        public string NameChapter { get; set; }
        public Guid? id_op { get; set; }
        public Guid? id_bnrpx { get; set; }
        public string Bnrpx { get; set; }
        public Int16? pornom { get; set; }
    }
}