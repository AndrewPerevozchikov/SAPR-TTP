using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class ModeShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public string number_trek { get; set; }
        public short? PORNOM { get; set; }
        public string SODRE { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public string P { get; set; }
        public string t { get; set; }
        public string pH { get; set; }
        public string OPA_K { get; set; }
        public string D { get; set; }
        public string U { get; set; }
        public string Time { get; set; }
        public Guid? id_number_trek { get; set; }
        public Guid? id_operation { get; set; }



    }
}