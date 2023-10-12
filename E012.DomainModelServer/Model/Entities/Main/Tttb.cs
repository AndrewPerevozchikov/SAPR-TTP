using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Tttb : AbstractSkat
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public string version { get; set; }
        public string punkt { get; set; }
        public short? pornom { get; set; }
        public string tt { get; set; }
        public short? priz { get; set; }
        public string idavtor { get; set; }
        public string fioavtor { get; set; }
        public DateTime? dpavtor { get; set; }
        public short? number_operationst_st { get; set; }

    }
}
