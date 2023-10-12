using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{ 
    public class MainChangeSheet : AbstractSkat
    {
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public string version { get; set; }
        public decimal? NREG { get; set; }
        public DateTime? DATREG { get; set; }
        public string rs { get; set; }
        public string AVTOR { get; set; }
        public string IDAVTOR { get; set; }
        public DateTime? DPAVTOR { get; set; }
        public string SIGNAVTOR { get; set; }
        public string NO { get; set; }
        public string PO { get; set; }
        public DateTime? creationdata { get; set; }
        public short? ready { get; set; }
        public Guid? guid_izv { get; set; }
        public int? Id { get; set; }

    }
}
