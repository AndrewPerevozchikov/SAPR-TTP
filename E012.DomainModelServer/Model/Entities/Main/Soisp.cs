using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Soisp : AbstractSkat
    { 
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public string version { get; set; }
        public string soisp { get; set; }
        public string idsoisp { get; set; }
        public DateTime? dpsoisp { get; set; }
        public string signsoisp { get; set; }
        public string po { get; set; }
        public short GR { get; set; }
        public string nomoper { get; set; }
        public string NOMTT { get; set; }
        public string NOMTTB { get; set; }
        public string nomteb { get; set; }
        public string number_operationKOR { get; set; }
        public string edittt { get; set; }
        public string editttb { get; set; }
        public string editteb { get; set; }
        public string id_operation { get; set; }
       


    }
}
