using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class EquipmentShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public short? PORNOM { get; set; }
        public string KOD { get; set; }
        public string NST { get; set; }
        public string NN { get; set; }
        public string KPROF { get; set; }
        public short? KI { get; set; }
        public short? KOID { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid? id_operation { get; set; }

    }
}
