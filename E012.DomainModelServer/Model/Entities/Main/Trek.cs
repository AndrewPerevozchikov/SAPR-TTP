using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Trek : AbstractStore
    {
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public short? number_operation { get; set; }
        public string version { get; set; }
        public string number_trek { get; set; }
        public string information { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid? id_number_trek { get; set; }
        public Guid? id_operation { get; set; }
        public TimeSpan? work_time { get; set; }

    }
}