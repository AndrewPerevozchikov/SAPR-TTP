using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.SKAT
{
    /// <summary>
    /// класс вышрузки данных
    /// </summary>
    public abstract class AbstractSkat
    {
        public TypeState typeState { get; set; }
    }
}