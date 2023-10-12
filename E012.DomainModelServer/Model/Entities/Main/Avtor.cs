
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class Avtor : AbstractSkat
    {
        public Avtor(string fio, DateTime? date)
        {
            Fio = fio;
            Date = date;
        }
        public string Fio { get; set; }

        public DateTime? Date { get; set; }
    }
}