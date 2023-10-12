using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class OperationInformation
    {
        public Operation operation { get; set; }
        public List<Equipment> equipments { get; set; }//оборудование
        public List<Material> materials { get; set; }//материал
        public List<Rigging> riggings { get; set; }//оснастка
        public List<Mode> modes { get; set; }//режимы
        public List<FreeText> attention { get; set; }//внимание
        public List<FreeText> instruction { get; set; }//указания
        public List<TrekInformation> trekInformation { get; set; }//переходы


    }
}