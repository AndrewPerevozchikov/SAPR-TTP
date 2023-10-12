using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class MaterialTechnicalArrays
    {
        public string NAIMC { get; set; }
        public string EI { get; set; }
        public int? VR { get; set; }
        public int? EN { get; set; }
        public string KRNAIMC { get; set; }
        public string komm { get; set; }
        public Guid? id_nn_material { get; set; }
        public string Concetration { get; set; }
        public string Viscosity { get; set; }
        public string Density { get; set; }

    }
}