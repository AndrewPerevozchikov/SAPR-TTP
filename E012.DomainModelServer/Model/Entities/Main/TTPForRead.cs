using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    //Level smallint null,
    //Tip smallint null,
    //NameChapter varchar(max),
    //name_dse char (24) NULL,
    //type_work char (2) NULL,
    //number_operation smallint  null,
    //number_trek char (3) null,
    //version char (2) NULL,
    //pornom smallint NULL,
    //id_operation uniqueidentifier NULL,
    //id_nn_material uniqueidentifier NULL,
    //Ordernumber int IDENTITY(1, 1))

    //класс для работы с деревом ТТП для лакокрасочных и гальвонических покрытий
    public class TTPForRead
    {
        public short Level { get; set; }
        public short? Tip { get; set; }
        public string NameChapter { get; set; }
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public short? number_operation { get; set; }
        public string number_trek { get; set; }
        public string version { get; set; }
        public short? pornom { get; set; }
        public Guid? id_operation { get; set; }
        public Guid? id_nn_material { get; set; }
        public int Ordernumber { get; set; }
    }
}
