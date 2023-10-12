using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Material : AbstractStore
    {
        public string name_dse { get; set; }
        public string type_work { get; set; }
        public short? number_operation { get; set; }
        public string version { get; set; }
        public short? pornom { get; set; }
        public string naimc { get; set; }
        public string ei { get; set; }
        public int? vr { get; set; }
        public short? en { get; set; }
        public double? pokr { get; set; }
        public string krnaimc { get; set; }
        public short? number_operationst_st { get; set; }
        public short? protctp { get; set; }
        public DateTime? datkor { get; set; }
        public short? ki { get; set; }
        public Guid? id_operation { get; set; }
        public Guid? id_nn_material { get; set; }
        public short? handle_input { get; set; }
        public string Concetration { get; set; }
        public string Viscosity { get; set; }
        public string Density { get; set; }
        public string NN { get; set; }

    }
}
