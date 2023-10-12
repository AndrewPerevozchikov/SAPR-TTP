using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class TrekInformation
    {
        public Trek trek { get; set; }//переходы
        public List<Mode> modes { get; set; }//режимы в переходах
        public List<Attention> freeTextTreks { get; set; }//указания к переходам
        public List<Rigging> riggings { get; set; }//оснастка в переходах
    }
}