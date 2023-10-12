using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class TreeShablonOperationOrTransit
    {
        public short Level { get; set; }
        public short? Tip { get; set; }
        public string NameChapter { get; set; }
        public Guid? id_operation { get; set; }
        public Guid? id_number_trek { get; set; }
        public string number_trek { get; set; }
        public short? pornom { get; set; }
    }
}