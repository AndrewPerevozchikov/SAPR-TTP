using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Tlp : AbstractSkat
    { 
        public string Dse_Obozn { get; set; }
        public string Vid_Work { get; set; }
        public string cex { get; set; }
        public string Avtor { get; set; }
        public string IdAvtor { get; set; }
        public DateTime? DpAvtor { get; set; }
        public string SignAvtor { get; set; }
        public string No { get; set; }
        public string Nakt { get; set; }
        public DateTime? Dakt { get; set; }
        public string IdAkti { get; set; }
        public string FioAkti { get; set; }
        public DateTime? DpAkti { get; set; }
        public string SignAkti { get; set; }
        public string Po { get; set; }
        public string IdKorp { get; set; }
        public DateTime? DpKorp { get; set; }
        public string FioKorp { get; set; }
        public string PgotTP { get; set; }
        public int? Nreg { get; set; }
        public DateTime? DatReg { get; set; }
        public DateTime? dotrarx { get; set; }
        public string prpech { get; set; }
        public short prgrtp { get; set; }
        public string cexakti { get; set; }
        public DateTime? creationdata { get; set; }
        public short stzak { get; set; }
        public Guid guid { get; set; }

    }
}
