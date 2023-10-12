using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Coordinating : AbstractSkat
    {
        public Coordinating(string fio, DateTime? date,string typeCoordinating)
        {
            Fio = fio;
            Date = date;
            TypeCoordinating = typeCoordinating;
        }
        public string Fio { get; set; }

        public DateTime? Date { get; set; }
        public string TypeCoordinating { get; set; }
    }
}