using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class AllOperationInformation
    {
        public OperationShablone operation { get; set; }//операция
        public List<EquipmentShablone> equipments { get; set; }//оборудование
        public List<MaterialShablone> materials { get; set; }//материал
        public List<RiggingShablone> riggings { get; set; }//оснастка
        public List<ModeShablone> modes { get; set; }//режимы
        public List<FreeTextShablone> attention { get; set; }//внимание
        public List<FreeTextShablone> instruction { get; set; }//указания
    }
}