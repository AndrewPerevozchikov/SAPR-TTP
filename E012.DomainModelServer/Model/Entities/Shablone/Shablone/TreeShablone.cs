using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT.Shablone
{
    public class TreeShablone
    {
        public TreeShablone(OperationShablone operation, List<TrekShablone> treks)
        {
            this.operation = operation;
            this.treks = treks;
        }
        public OperationShablone operation { get; set; }//операция
        public List<TrekShablone> treks { get; set; }//переходы
    }
}