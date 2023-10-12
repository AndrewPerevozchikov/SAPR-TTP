using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    /// <summary>
    /// состояние изменения раздела
    /// </summary>
    public enum TypeState
    {
        /// <summary>
        /// не изменен-последнее состояние из БД
        /// </summary>
        STATE_NOT_CHANGED = 0,
        /// <summary>
        /// создан ,еще не сохранен в БД
        /// </summary>
        STATE_NEW = 1,
        /// <summary>
        /// изменен ,еще не сохранен в БД
        /// </summary>
        STATE_CHANGED = 2,
        /// <summary>
        /// удален ,еще не сохранен в БД
        /// </summary>
        STATE_DELETED = 3
    }

}