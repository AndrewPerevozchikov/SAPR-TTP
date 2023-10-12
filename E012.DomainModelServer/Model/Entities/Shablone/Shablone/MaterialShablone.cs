using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class MaterialShablone
    {
        public int? NOM_MAS { get; set; }
        public short? number_operation { get; set; }
        public short? PORNOM { get; set; }
        public string NAIMC { get; set; }
        public string EI { get; set; }
        public int? VR { get; set; }
        public short? EN { get; set; }
        public double? POKR { get; set; }
        public string KRNAIMC { get; set; }
        public short? PROTCTP { get; set; }
        public string IDKOR { get; set; }
        public DateTime? DKOR { get; set; }
        public Guid? id_nn_material { get; set; }
        public string Concetration { get; set; }
        public string Viscosity { get; set; }
        public string Density { get; set; }
        public Guid? id_operation { get; set; }
        public string NN { get; set; }
    }
}
