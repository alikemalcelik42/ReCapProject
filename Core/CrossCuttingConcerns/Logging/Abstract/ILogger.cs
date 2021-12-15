using Core.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcerns.Logging.Abstract
{
    public interface ILogger
    {
        public IResult Log(string data);
    }
}
