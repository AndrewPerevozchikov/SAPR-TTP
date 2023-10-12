using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace E012.DomainModelServer.Model.Entities.PCTEXT
{
    public class Result<T>
    {
        public List<T> Results;
        public string ErrorMessage;
        public Result(List<T> results)
        {
            Results = results;
            ErrorMessage = null;
        }
        public Result( string message)
        {
            ErrorMessage = message;
        }
    }

}