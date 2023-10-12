using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public enum TypeTechnicalArray 
    {
        /// <summary>
        /// тех требования 
        /// </summary>
        MAIN_TT = 1,

        /// <summary>
        /// ттб
        /// </summary>
        MAIN_TTB = 2,

        /// <summary>
        ///ТЭБ-
        /// </summary>
        MAIN_TEB = 3,
        /// <summary>
        /// операции [А] 
        /// </summary>
        OPERATION = 4,



        /// <summary>
        /// ОПЕРАЦИИ [A] справочник операций
        /// </summary>
        OPERATION_DIRECTORY = 12,

        /// <summary>
        /// Оборудование [Б] 
        /// </summary>
        EQUIPMENT = 5,
        /// <summary>
        /// ОБОРУДОВАНИЕ [Б] выбор кодов профессий из справочника профессий
        /// </summary>
        EQUIPMENT_DIRECTORY = 13,

        /// <summary>
        /// МАТЕРИАЛЫ [М]
        /// </summary>
        MATERIAL = 6,
        /// <summary>
        /// МАТЕРИАЛЫ [М]
        /// </summary>
        MATERIAL_DIRECTORY = 14,

        /// <summary>
        /// Оснастка [Т] 
        /// </summary>
        RIGGING = 7,

        /// <summary>
        /// Переход [O] 
        /// </summary>
        TREK = 8,

        /// <summary>
        /// Режим [Р]
        /// </summary>
        MODE = 9,

        /// <summary>
        /// Внимание [B] 
        /// </summary>
        ATTENTION = 10,

        /// <summary>
        /// УКАЗАНИЕ [У]
        /// </summary>
        MAIN_FREETEXT = 11
    }
}