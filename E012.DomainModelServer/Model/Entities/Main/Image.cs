using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Image : AbstractSkat
    { 
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public short? number_operation{ get; set; }
        public string version { get; set; }
        public byte[] ESKIZ { get; set; }
        public short? ki { get; set; }
        public short? number_operationst_st { get; set; }
        public string name_eskiz { get; set; }
        public short pornom { get; set; }
        public string formates { get; set; }
        public byte[] origin_eskiz { get; set; }
        public string origin_name { get; set; }
        public Guid? id_eskiz { get; set; }
        public Guid? id_operation { get; set; }
        public string number_trek { get; set; }

    }
}
