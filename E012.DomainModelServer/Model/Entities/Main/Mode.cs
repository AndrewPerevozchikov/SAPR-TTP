using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{  
    public class Mode : AbstractStore
    {
        public string name_dse{ get; set; }
        public string type_work{ get; set; }
        public short? number_operation{ get; set; }
        public string version { get; set; }
        public string number_trek { get; set; }
        public short? pornom { get; set; }
        public string sodre { get; set; }
        public short? number_operationst_st { get; set; }
        public Guid? id_operation { get; set; }

        public string P { get; set; }//Давление

        public string t { get; set; }//температура

        public string pH { get; set; }//Водородный показатель

        public string OPA_K { get; set; }//ОПА/К

        public string D { get; set; }//D тока

        public string U { get; set; }//U (напряжение)

        public string Time { get; set; }
    }
}
