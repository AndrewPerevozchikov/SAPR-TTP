using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class TechnicalDetails : AbstractSkat
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public short? number_operation{ get; set; }
        public string version { get; set; }
        public short? pornom { get; set; }
        public string naim { get; set; }
        public string gost { get; set; }
        public string dsek { get; set; }
        public short? en { get; set; }
        public short? ki { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid? id_operation { get; set; }
        public Guid? id_details { get; set; }

    }
}
