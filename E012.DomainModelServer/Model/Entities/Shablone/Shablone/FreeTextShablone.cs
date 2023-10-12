using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class FreeTextShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public string FREETEX { get; set; }
        public short? PORNOM { get; set; }
        public string ST { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid? id_operation { get; set; }

    }
}
