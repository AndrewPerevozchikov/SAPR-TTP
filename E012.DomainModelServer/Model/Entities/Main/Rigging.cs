using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Rigging : AbstractStore
    { 
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public short? number_operation { get; set; }
        public string version { get; set; }
        public string number_trek { get; set; }
        public short? pornom { get; set; }
        public string nn { get; set; }
        public string shc { get; set; }
        public string naimcto { get; set; }
        public short? ipr { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid? id_operation { get; set; }

    }
}
