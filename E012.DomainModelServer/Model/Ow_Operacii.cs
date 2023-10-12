using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace E012.DomainModel.DTO
{
    public class Ow_Operaciy
    {
        //naim      dse               decn_main                       decn_remark           bnop  kod_oper  nmopr
        //ВИП1	    ЦВИЯ.436631.019	  ЦВИЯ.436631.019ПМ (версия 06)	  ГТП-1198 (версия 33)	045	  9903	    Установка ЭРИ на автомате                                                                           

        [StringLength(250)]
        public string Naim { get; set; }

        [Required]
        [StringLength(35)]
        public string Dse { get; set; }

        [StringLength(32)]
        public string Decn_main { get; set; }

        [StringLength(32)]
        public string Decn_remark { get; set; }

        [StringLength(4)]
        public string Bnop { get; set; }

        [StringLength(4)]
        public string Kod_oper { get; set; }

        [StringLength(100)]
        public string Nmopr { get; set; }
        [StringLength(3)]
        public string nc { get; set; }
        [StringLength(2)]
        public string vid_work_main { get; set; }
        [StringLength(24)]
        public string dse_obozn_main { get; set; }
        [StringLength(20)]
        public string tip_oper { get; set; }

  }
}
