using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Boddse : AbstractSkat
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public string version { get; set; }
        public string dse_oboznisp { get; set; }
    }
}
