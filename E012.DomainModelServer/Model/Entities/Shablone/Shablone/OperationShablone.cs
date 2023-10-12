using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class OperationShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public string NC { get; set; }
        public string KOD_OPER { get; set; }
        public string NMOPR { get; set; }
        public string OBDOK { get; set; }
        public short? NIOT { get; set; }
        public string VID_PR { get; set; }
        public short number_operationTI { get; set; }
        public DateTime LASTDATA { get; set; }
        public string KOMM { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid id_operation { get; set; }

    }
}
