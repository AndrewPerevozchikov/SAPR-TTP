using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Login
    {
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public short? number_operation{ get; set; }
        public short? priz { get; set; }
        public string punkt { get; set; }
        public string idavtor { get; set; }
        public string fioavtor { get; set; }
        public DateTime? dpavtor { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid? id_operation { get; set; }
    }
}