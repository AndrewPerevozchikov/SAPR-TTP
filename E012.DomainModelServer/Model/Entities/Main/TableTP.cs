
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    public class TableTP : AbstractSkat
    {
        public string version { get; set; }
        public short? ki { get; set; }
        public Guid? id_eskiz_row { get; set; }
        public Guid? id_table { get; set; }
        public string dse { get; set; }
        public string dse_kd { get; set; }
        public Guid? id_gpf_dse { get; set; }
        public string oper { get; set; }
        public int? pornom { get; set; }
        public string column1 { get; set; }
        public string column2 { get; set; }
        public string column3 { get; set; }
        public string column4 { get; set; }
        public string column5 { get; set; }
        public string column6 { get; set; }
        public string column7 { get; set; }
        public string column8 { get; set; }
        public string column9 { get; set; }
        public string column10 { get; set; }
        public string column11 { get; set; }
        public string column12 { get; set; }
        public string column13 { get; set; }
        public string column14 { get; set; }
        public string column15 { get; set; }
        public string column16 { get; set; }
        public string column17 { get; set; }
        public string column18 { get; set; }
        public string column19 { get; set; }
        public string column20 { get; set; }
        public string column21 { get; set; }
        public string column22 { get; set; }
        public DateTime? datkor { get; set; }

    }
}