using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class FreeText : AbstractStore
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public short? number_operation{ get; set; }
        public string version { get; set; }
        public string freetex { get; set; }
        public string st { get; set; }
        public short? number_operationst_st { get; set; }
        public short? pornom { get; set; }
        public Guid? id_operation { get; set; }

    }
}
