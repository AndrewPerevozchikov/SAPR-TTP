using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class TTP
    {
        
        public Permission permission { get; set; }
        public TotalInformation totalInformation { get; set; }
        public List<OperationInformation> operation { get; set; }
        public FreeText freeText { get; set; }
    }
}