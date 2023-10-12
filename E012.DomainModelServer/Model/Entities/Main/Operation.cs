using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Operation: AbstractStore
    {
        public Operation()
        {

        }
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public short number_operation{ get; set; }
        public string version { get; set; }
        public string NC { get; set; }
        public string kod_oper { get; set; }
        public string nmopr { get; set; }
        public string OBDOK { get; set; }
        public short? niot { get; set; }
        public string vid_pr { get; set; }
        public short? number_operationti{ get; set; }
        public short ki { get; set; }
        public string idavtor { get; set; }
        public string fioavtor { get; set; }
        public DateTime? dpavtor { get; set; }
        public short? priz { get; set; }
        public string punkt { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid id_operation{ get; set; }

    }
}
