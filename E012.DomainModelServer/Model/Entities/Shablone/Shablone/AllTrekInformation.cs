using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class AllTrekInformation
    {
        public AllTrekInformation(){}
        public AllTrekInformation(TrekShablone trek, List<ModeShablone> modes, List<FreeTextTrekShablone> freeTextTreks, List<RiggingShablone> riggings)
        {
            this.trek = trek;
            this.modes = modes;
            this.freeTextTreks = freeTextTreks;
            this.riggings = riggings;
        }
        public TrekShablone trek { get; set; }//переходы
        public List<ModeShablone> modes { get; set; }//режимы в переходах
        public List<FreeTextTrekShablone> freeTextTreks { get; set; }//указания к переходам
        public List<RiggingShablone> riggings { get; set; }//оснастка в переходах

    }
}