using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class RiggingShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public string number_trek { get; set; }
        public short? PORNOM { get; set; }
        public string NN { get; set; }
        public string SHC { get; set; }
        public string NAIMCTO { get; set; }
        public short? IPR { get; set; }
        public string pr_name { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid? id_number_trek { get; set; }
        public Guid? id_operation { get; set; }

    }
}
