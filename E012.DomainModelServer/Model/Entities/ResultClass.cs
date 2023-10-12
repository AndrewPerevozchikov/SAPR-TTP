using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities
{
    public class ResultClass<T>
    {

        public T Result;
        public string ErrorMessage;
        public ResultClass(T result)
        {
            Result = result;
            ErrorMessage = null;
        }
        public ResultClass(string message)
        {
            ErrorMessage = message;
        }
    }
}