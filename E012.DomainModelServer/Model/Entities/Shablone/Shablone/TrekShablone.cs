using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class TrekShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public string number_trek { get; set; }
        public string BNMPX { get; set; }
        public DateTime? lastdata { get; set; }
        public string komm { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid? id_number_trek { get; set; }
        public Guid? id_operation { get; set; }

    }
}