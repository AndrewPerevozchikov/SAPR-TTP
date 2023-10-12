using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    /// <summary>
    /// Типы разделов
    /// </summary>
    public enum TypeChapter:short
    {
        /// <summary>
        /// Общие сведения - главный раздел - обязательно содержит разделы типа  Общие данные(DATA), подписи(SIGNS), исполнения(PERFOMANCES)
        /// </summary>
        MAIN_DATA = 0,

        /// <summary>
        /// тех требования - главный раздел - не содержит подразделы
        /// </summary>
        MAIN_TT = 1,

        /// <summary>
        /// ттб- главный раздел - не содержит подразделы
        /// </summary>
        MAIN_TTB = 2,

        /// <summary>
        ///ТЭБ- главный раздел - не содержит подразделы
        /// </summary>
        MAIN_TEB = 3,

        /// <summary>
        /// Свободный текст ко всему ТП- главный раздел - не содержит подразделы
        /// </summary>
        MAIN_FREETEXT = 4,

        /// <summary>
        /// операции [А] (главный раздел дерева) - содержит перечень операций
        /// </summary>
        OPERATIONS = 5,

        /// <summary>
        /// ЭСКИзы (главный раздел дерева) - содержит перечень эскизов
        /// </summary>
        IMAGES = 6,

        /// <summary>
        /// Общие данные  - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        DATA = 7,

        /// <summary>
        /// подписи  - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        SIGNS = 8,

        /// <summary>
        /// исполнения - подраздел дерева - содержится в главном разделе дерева ТТП MAIN_DATA
        /// </summary>
        PERFOMANCES = 9,

        /// <summary>
        /// операция [А] -конкретная операция - содержится в главном разделе дерева ТТП MAIN_OPERATIONS
        /// </summary>
        OPERATION = 10,

        /// <summary>
        /// эскиз  -конкретный эскиз - содержится в главном разделе дерева ТТП MAIN_IMAGES, в конкретной операции OPERATION или в переходе TREK или в MAIN_TABLES
        /// </summary>
        IMAGE = 11,

        /// <summary>
        /// Оборудования [Б]  - подраздел дерева - содержится в конкретной операции OPERATION, содержит в себе EQUIPMENT
        /// </summary>
        EQUIPMENTS = 12,

        /// <summary>
        /// Оборудование [Б] -конкретное оборудование - содержится в разделе оборудований EQUIPMENTS
        /// </summary>
        EQUIPMENT = 13,

        /// <summary>
        /// Тех детали [К]- подраздел дерева - содержится в конкретной операции OPERATION, содержит в себе TECHNICAL_DETAIL
        /// </summary>
        TECHNICAL_DETAILS = 14,

        /// <summary>
        /// Тех деталь [К]- конкретная тех деталь - содержится в разделе TECHNICAL_DETAILS
        /// </summary>
        TECHNICAL_DETAIL = 15,

        /// <summary>
        /// Материалы [М]- подраздел дерева - содержится в конкретной операции OPERATION, содержит в себе MATERIAL
        /// </summary>
        MATERIALS = 16,


        /// <summary>
        /// Материал [М]- конкретный материал - содержится в разделе MATERIALS
        /// </summary>
        MATERIAL = 17,

        /// <summary>
        /// Оснастка [Т]- подраздел дерева - содержится в конкретной операции OPERATION или в переходе TREK, содержит в себе RIGGING
        /// </summary>
        RIGGINGS = 18,


        /// <summary>
        /// Оснастка [Т]- конкретная оснастка - содержится в разделе RIGGINGS 
        /// </summary>
        RIGGING = 19,

        /// <summary>
        /// Режимы  [Р]- подраздел дерева - содержится в конкретной операции OPERATION или в переходе TREK, содержит в себе MODE
        /// </summary>
        MODES = 20,

        /// <summary>
        /// Режим [Р]- конкретный режим - содержится в разделе MODES
        /// </summary>
        MODE = 21,

        /// <summary>
        /// Внимание [B] (ранее свободный текст перед операцией)  - подраздел дерева - содержится в конкретной операции OPERATION
        /// </summary>
        ATTENTION = 22,

        /// <summary>
        /// Переходы [O] -  подраздел дерева - содержится в конкретной операции OPERATION , содержит в себе TREK
        /// </summary>
        TREKS = 23,

        /// <summary>
        /// Переход [O] -  конкретный переход - содержится в разделе TREKS
        /// </summary>
        TREK = 24,

        /// <summary>
        /// Указание [У] -  подраздел дерева - содержится в конкретной операции OPERATION (ранее свободный текст после операции) и в переходе TREK  (ранее свободный текст после перехода)
        /// </summary>
        DESIGNATION = 25,

        /// <summary>
        /// Внимание [B] (конкретная запись Внимания)  - содержится в разделе ATTENTION
        /// </summary>
        ATTENTION_RECORD = 26,

        /// <summary>
        /// Указание [У] (конкретная запись Указания)  - содержится в разделе DESIGNATION
        /// </summary>
        DESIGNATION_RECORD = 27,

        /// <summary>
        /// Лист изменений (только для корректировки) - главный раздел  - не содержит подразделы
        /// </summary>
        MAIN_CHANGE_SHEET = 28,

        /// <summary>
        /// Главный раздел ТАБЛИЦЫ КТП (произвольная таблица)  - содержит таблицы и эскизы 
        /// </summary>
        MAIN_TABLES_KTP = 29,

        /// <summary>
        /// таблица КТП  (произвольная таблица) , содержится в MAIN_TABLES_KTP
        /// </summary>
        TABLE_KTP = 30,


        /// <summary>
        /// Главный раздел ТАБЛИЦЫ ВТП (обязательно наличие в содержании таблицы обозначения ДСЕ)- содержит таблицы и эскизы
        /// </summary>
        MAIN_TABLES_VTP = 31,

        /// <summary>
        /// таблица ВТП (обязательно наличие в содержании таблицы обозначения ДСЕ), содержится в MAIN_TABLES_VTP
        /// </summary>
        TABLE_VTP = 32


    }
}
