using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Sogltp : AbstractSkat
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public string nazsogl { get; set; }
        public short? sogl { get; set; }
        public short? num { get; set; }
        public short? gr { get; set; }
        public string logsogl { get; set; }
        public string fiosogl { get; set; }
        public DateTime? dpsogl { get; set; }
        public string signsogl { get; set; }

    }
}
